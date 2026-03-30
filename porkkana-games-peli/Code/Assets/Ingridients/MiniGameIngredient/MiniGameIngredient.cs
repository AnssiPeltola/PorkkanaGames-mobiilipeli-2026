using Godot;
using System;

// https://www.youtube.com/watch?v=K_mZeYLYpgg - SubViewport tutorial - Make minigame scene with subViewport?

public partial class MiniGameIngredient : BaseIngridient
{
	[Export] public Texture2D ChoppedSprite;


	// PackedScene = Godots own .tscn type
	// https://docs.godotengine.org/en/stable/classes/class_packedscene.html
	// We can now assign .tscn scenes to _CuttingMiniGameScene
	[Export] private PackedScene _cuttingMiniGameScene;


	// When public we can set this as true or false in other code where this object is used
	public bool IsInDropZone { get; set; } = false;
	public bool OpenMiniGame { get; set; } = false;

	private CuttingMiniGame _activeMiniGame = null;

	private Sprite2D _sprite;

	public override void _Ready()
	{
		// We get this scenes Sprite2D node in variable _sprite
		_sprite = GetNode<Sprite2D>("Sprite2D");
	}

	public override void _Input(InputEvent e)
	{
		base._Input(e);

		// touchtap.Pressed prevents releasing touch to register as click.
		if (e is InputEventScreenTouch touchtap && touchtap.Pressed)
		{
			if (IsInDropZone)
			{
				OpenMiniGame = true;
				GD.Print("Open minigame!");
				_sprite.Hide();
				StartCuttingMiniGame();
			}
		}
	}

	private void StartCuttingMiniGame()
	{
		// https://docs.godotengine.org/en/stable/tutorials/scripting/nodes_and_scene_instances.html
		// Make new instance of the cutting minigame (object of CuttingMiniGame class)
		// call that instance _cuttingMiniGameScene
		// NOTE: _activeMiniGame = godots datatype "PackedScene" = takes (.tscn)
		_activeMiniGame = _cuttingMiniGameScene.Instantiate<CuttingMiniGame>();

		// Read the current texture and hand it to the new instance ("_activeMinGame")
		_activeMiniGame.IngridientTexture = _sprite.Texture;

		// Initiate the actual opening of the minigame
		// https://docs.godotengine.org/en/stable/tutorials/scripting/change_scenes_manually.html
		GetTree().CurrentScene.AddChild(_activeMiniGame);

		// Subscribe to signal CuttingCompleteEventHandler()
		//      From CuttingMiniGame.cs (Class)
		// When signal is received run OnCuttingComplete();
		_activeMiniGame.CuttingComplete += OnCuttingComplete;

		GD.Print("CuttingMiniGame started.");
	}

	private void OnCuttingComplete()
	{
		GD.Print("Ingredient chopped!");
		// Close the running minigame
		// Also unsubscribes the signals!
		_activeMiniGame.QueueFree();

		// Reset _activeMiniGame back to null
		_activeMiniGame = null;


		/* TODO:
			Currently: Only changing the sprite
			Consider: Swap the entire MiniGameIngridient node -> CuttedIngridient<name>
		*/

		_sprite.Texture = ChoppedSprite;
		_sprite.Show();


		/*TODO:
			Inform "GameManager" that ingridient has been cut etc.
		*/
	}
}
