using Godot;
using System;

public partial class LevelTwoIngredient : BaseIngridient
{
	// https://www.w3schools.com/cs/cs_enums.php - An enum is a special "class" that represents a group of constants.
	public enum IngredientState
	{
		Raw,
		Chopped,
		Cooked
	}
		
	// PackedScene = Godots own .tscn type
	// https://docs.godotengine.org/en/stable/classes/class_packedscene.html
	// We can now assign .tscn scenes to _CuttingMiniGameScene
	[Export] private PackedScene _cuttingMiniGameScene;
	private CuttingMiniGame _activeMiniGame = null;
	
	// When public we can set this as true or false in other code where this object is used
	public bool IsInDropZone { get; set; } = false;
	public bool IsInFryingPan { get; set; } = false;
	public bool OpenMiniGame { get; set; } = false;
	
	private Texture2D _tomatoTexture;
	private Texture2D _onionTexture;
	private Texture2D _carrotTexture;
	private Texture2D _choppedOnionTexture;
	private Texture2D _pastaTexture;

	private Texture2D _choppedCarrotTexture;
	private Texture2D _choppedTomatoTexture;
	private CollisionShape2D _tomatoCollision;
	private CollisionShape2D _onionCollision;
	private CollisionShape2D _carrotCollision;
	private CollisionShape2D _pastaCollision;

	// Init IngredientState as RAW.
	public IngredientState State { get; set; } = IngredientState.Raw;
	
	public override void _Ready()
	{
		// GetNode Sprite2D into variable _sprite
		_sprite = GetNode<Sprite2D>("Sprite2D");

		// Load and enable CollisionShape and Sprite2D Textures by group. Collisions in editor is set "Disabled".
		if (this.IsInGroup("Tomato"))
		{
			_tomatoTexture = GD.Load<Texture2D>("res://Art/Assets/Ingridients/Tomato/tomato-v1.png");
			_tomatoCollision = GetNode<CollisionShape2D>("TomatoCollision");
			_choppedTomatoTexture = GD.Load<Texture2D>("res://Art/Assets/Ingridients/Tomato/tomato-chopped-v1.png");
			ChangeSprite(_tomatoTexture);
			_tomatoCollision.Disabled = false;
		}

		if (this.IsInGroup("Onion"))
		{
			_onionTexture = GD.Load<Texture2D>("res://Art/Assets/Ingridients/Onion/onion-v1.png");
			_onionCollision = GetNode<CollisionShape2D>("OnionCollision");
			_choppedOnionTexture = GD.Load<Texture2D>("res://Art/Assets/Ingridients/Onion/onion-chopped-v1.png");
			ChangeSprite(_onionTexture);
			_onionCollision.Disabled = false;
		}

		if (this.IsInGroup("Carrot"))
		{
			_carrotTexture = GD.Load<Texture2D>("res://Art/Assets/Ingridients/Carrot/carrot-v1.png");
			_carrotCollision = GetNode<CollisionShape2D>("CarrotCollision");
			_choppedCarrotTexture = GD.Load<Texture2D>("res://Art/Assets/Ingridients/Carrot/carrot-chopped-v1.png");
			ChangeSprite(_carrotTexture);
			_carrotCollision.Disabled = false;
		}

		if (this.IsInGroup("Pasta"))
		{
			_pastaTexture = GD.Load<Texture2D>("res://Art/Assets/Ingridients/Pasta/spaghetti-v1.png");
			_pastaCollision = GetNode<CollisionShape2D>("PastaCollision");
			ChangeSprite(_pastaTexture);
			_pastaCollision.Disabled = false;
		}
	}

	public override void _Input(InputEvent e)
	{
		// base = run the inherited _Input() AND any additions bellow
		base._Input(e);

		// touchtap.Pressed prevents releasing touch to register as click.
		if (e is InputEventScreenTouch touchtap && touchtap.Pressed)
		{
			if ((touchtap.Position - GlobalPosition).Length() < base._clickRadius && IsInDropZone && State == IngredientState.Raw && !OpenMiniGame)
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
		// Make new instance of the cutting minigame (instance of CuttingMiniGame class)
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
		OpenMiniGame = false;

		Chop();

		/* TODO:
			Currently: Only changing the sprite
			Consider: Swap the entire MiniGameIngridient node -> CuttedIngridient<name>
		*/
		// _sprite.Texture = ChoppedSprite;
		_sprite.Show();


		/*TODO:
			Inform "GameManager" that ingridient has been cut etc.
		*/
	}

	public void Chop()
	{
		State = IngredientState.Chopped;
		RemoveFromGroup("Raw");
		AddToGroup("Chopped");

		if (IsInGroup("Onion"))
		{
			ChangeSprite(_choppedOnionTexture);
		}

		if (IsInGroup("Carrot"))
		{
			ChangeSprite(_choppedCarrotTexture);
		}

		if (IsInGroup("Tomato"))
		{
			ChangeSprite(_choppedTomatoTexture);
		}
	}

	public void changeStateCooked()
	{
		State = IngredientState.Cooked;
		RemoveFromGroup("Chopped");
		AddToGroup("Cooked");
	}
}
