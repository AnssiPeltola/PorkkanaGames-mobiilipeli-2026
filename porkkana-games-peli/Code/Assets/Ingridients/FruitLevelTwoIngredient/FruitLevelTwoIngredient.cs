using Godot;
using System;

public partial class FruitLevelTwoIngredient : BaseIngridient
{
	// https://www.w3schools.com/cs/cs_enums.php - An enum is a special "class" that represents a group of constants.
	public enum IngredientState
	{
		Raw,
		Peeled,
		Chopped
	}

	// PackedScene = Godots own .tscn type
	// https://docs.godotengine.org/en/stable/classes/class_packedscene.html
	// We can now assign .tscn scenes to _CuttingMiniGameScene
	[Export] private PackedScene _cuttingMiniGameScene;
	[Export] private PackedScene _peelingMiniGameScene;
	private CuttingMiniGame _activeMiniGame = null;

	// When public we can set this as true or false in other code where this object is used
	public bool IsInDropZone { get; set; } = false;
	public bool IsInPeelDropZone { get; set; } = false;
	public bool OpenMiniGame { get; set; } = false;
	public bool OpenPeelMiniGame { get; set; } = false;

	// TODO: Apple, kiwi, banana, grapes, lime. Need As it is, peeled, and chopped
	// TODO: for all of those own collision / touch areas

	// Whole fruit texture
	private Texture2D _appleTexture;
	private Texture2D _kiwiTexture;
	private Texture2D _bananaTexture;
	private Texture2D _grapesTexture;
	private Texture2D _limeTexture;
	// Peeled fruit texture
	private Texture2D _peeledAppleTexture;
	private Texture2D _peeledKiwiTexture;
	private Texture2D _peeledBananaTexture;
	private Texture2D _peeledGrapesTexture;
	// Chopped fruit texture
	private Texture2D _choppedAppleTexture;
	private Texture2D _choppedKiwiTexture;
	private Texture2D _choppedBananaTexture;
	private Texture2D _choppedGrapesTexture;
	private Texture2D _choppedLimeTexture;
	// Fruit collision
	private CollisionShape2D _appleCollision;
	private CollisionShape2D _kiwiCollision;
	private CollisionShape2D _bananaCollision;
	private CollisionShape2D _grapesCollision;
	private CollisionShape2D _limeCollision;
	// Touch collisions
	private CollisionShape2D _circleTouch;
	private CollisionShape2D _bananaTouch;

	// Init IngredientState as RAW.
	public IngredientState State { get; set; } = IngredientState.Raw;

	public override void _Ready()
	{
		// GetNode Sprite2D into variable _sprite
		_sprite = GetNode<Sprite2D>("Sprite2D");

		// Load and enable CollisionShape and Sprite2D Textures by group. Collisions in editor is set "Disabled".
		if (this.IsInGroup("Apple"))
		{
			_appleTexture = GD.Load<Texture2D>("res://Art/Assets/Ingridients/Apple/apple-v1.png");
			_appleCollision = GetNode<CollisionShape2D>("AppleCollision");
			_circleTouch = GetNode<CollisionShape2D>("TouchArea/CircleTouch");
			// TODO: SWITCH PEELED / CHOPPED APPLE PATHS
			_peeledAppleTexture = GD.Load<Texture2D>("res://Art/Assets/Ingridients/Apple/rotten-apple-v1.png");
			_choppedAppleTexture = GD.Load<Texture2D>("res://Art/Assets/Ingridients/Tomato/tomatosauce.png");
			ChangeSprite(_appleTexture);
			_appleCollision.Disabled = false;
			_circleTouch.Disabled = false;
		}

		if (this.IsInGroup("Kiwi"))
		{
			_kiwiTexture = GD.Load<Texture2D>("res://Art/Assets/Ingridients/Kiwi/kiwi-v1.png");
			_kiwiCollision = GetNode<CollisionShape2D>("KiwiCollision");
			_circleTouch = GetNode<CollisionShape2D>("TouchArea/CircleTouch");
			_peeledKiwiTexture = GD.Load<Texture2D>("res://Art/Assets/Ingridients/Kiwi/kiwi-peeled-v1.png");
			_choppedKiwiTexture = GD.Load<Texture2D>("res://Art/Assets/Ingridients/Kiwi/kiwi-chopped-v1.png");
			ChangeSprite(_kiwiTexture);
			_kiwiCollision.Disabled = false;
			_circleTouch.Disabled = false;
		}

		if (this.IsInGroup("Banana"))
		{
			_bananaTexture = GD.Load<Texture2D>("res://Art/Assets/Ingridients/Banana/banana-v2.png");
			_bananaCollision = GetNode<CollisionShape2D>("BananaCollision");
			_bananaTouch = GetNode<CollisionShape2D>("TouchArea/BananaTouch");
			// TODO: SWITCH PEELED AND CHOPPED BANANA PATHS
			_peeledBananaTexture = GD.Load<Texture2D>("res://Art/Assets/Ingridients/Banana/banana-v1.png");
			_choppedBananaTexture = GD.Load<Texture2D>("res://Art/Assets/Ingridients/Banana/rotten-banana-v1.png");
			ChangeSprite(_bananaTexture);
			_bananaCollision.Disabled = false;
			_bananaTouch.Disabled = false;
		}

		if (this.IsInGroup("Grapes"))
		{
			_grapesTexture = GD.Load<Texture2D>("res://Art/Assets/Ingridients/Grape/grapes-v1.png");
			_grapesCollision = GetNode<CollisionShape2D>("GrapesCollision");
			_circleTouch = GetNode<CollisionShape2D>("TouchArea/CircleTouch");
			_peeledGrapesTexture = GD.Load<Texture2D>("res://Art/Assets/Ingridients/Grape/grapes-peeled-v1.png");
			_choppedGrapesTexture = GD.Load<Texture2D>("res://Art/Assets/Ingridients/Grape/grapes-chopped-v1.png");
			ChangeSprite(_grapesTexture);
			_grapesCollision.Disabled = false;
			_circleTouch.Disabled = false;
		}

		if (this.IsInGroup("Lime"))
		{
			_limeTexture = GD.Load<Texture2D>("res://Art/Assets/Ingridients/Lime/lime-v1.png");
			_limeCollision = GetNode<CollisionShape2D>("LimeCollision");
			_circleTouch = GetNode<CollisionShape2D>("TouchArea/CircleTouch");
			// TODO: SWITCH CHOPPED LIME PATH
			_choppedLimeTexture = GD.Load<Texture2D>("res://Art/Assets/Ingridients/Lime/lime-chopped-v1.png");
			ChangeSprite(_limeTexture);
			_limeCollision.Disabled = false;
			_circleTouch.Disabled = false;
			State = IngredientState.Peeled;
		}

		// Do also ready from BaseIngredient (Load TouchArea)
		base._Ready();
	}

	public override void _Input(InputEvent e)
	{
		// base = run the inherited _Input() AND any additions bellow
		base._Input(e);

		// touchtap.Pressed prevents releasing touch to register as click
		// Cutting minigame
		if (e is InputEventScreenTouch touchtap && touchtap.Pressed)
		{
			if (IsInDropZone && State == IngredientState.Peeled && !OpenMiniGame)
			{
				OpenMiniGame = true;
				GD.Print("Open minigame!");
				_sprite.Hide();
				StartCuttingMiniGame();
			}
		}

		// Peeling minigame
		if (e is InputEventScreenTouch touch && touch.Pressed)
		{
			if (IsInPeelDropZone && State == IngredientState.Raw && !OpenPeelMiniGame && !this.IsInGroup("Lime"))
			{
				OpenPeelMiniGame = true;
				GD.Print("Open peeling minigame!");
				_sprite.Hide();
				StartPeelingMiniGame();
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

	private void StartPeelingMiniGame()
	{
		// https://docs.godotengine.org/en/stable/tutorials/scripting/nodes_and_scene_instances.html
		// Make new instance of the cutting minigame (instance of CuttingMiniGame class)
		// call that instance _cuttingMiniGameScene
		// NOTE: _activeMiniGame = godots datatype "PackedScene" = takes (.tscn)
		// 		TODO: PeelingMiniGame
		_activeMiniGame = _peelingMiniGameScene.Instantiate<CuttingMiniGame>();

		// Read the current texture and hand it to the new instance ("_activeMinGame")
		_activeMiniGame.IngridientTexture = _sprite.Texture;

		// Initiate the actual opening of the minigame
		// https://docs.godotengine.org/en/stable/tutorials/scripting/change_scenes_manually.html
		GetTree().CurrentScene.AddChild(_activeMiniGame);

		// Subscribe to signal CuttingCompleteEventHandler()
		//      From CuttingMiniGame.cs (Class)
		// When signal is received run OnCuttingComplete();
		_activeMiniGame.CuttingComplete += OnPeelingComplete;

		GD.Print("Peeling MiniGame started.");
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

	private void OnPeelingComplete()
	{
		GD.Print("Ingredient Peeled!");
		// Close the running minigame
		// Also unsubscribes the signals!
		_activeMiniGame.QueueFree();

		// Reset _activeMiniGame back to null
		_activeMiniGame = null;
		OpenPeelMiniGame = false;

		// Peel fruit
		Peel();

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

	// TODO: Make new samekind of thing for peeling as chopping
	// TODO: Change these for apple, kiwi, banana, grapes, lime(?)
	public void Chop()
	{
		State = IngredientState.Chopped;
		RemoveFromGroup("Peeled");
		AddToGroup("Chopped");

		if (IsInGroup("Apple"))
		{
			ChangeSprite(_choppedAppleTexture);
		}

		if (IsInGroup("Kiwi"))
		{
			ChangeSprite(_choppedKiwiTexture);
		}

		if (IsInGroup("Banana"))
		{
			ChangeSprite(_choppedBananaTexture);
		}
		if (IsInGroup("Grapes"))
		{
			ChangeSprite(_choppedGrapesTexture);
		}
		if (IsInGroup("Lime"))
		{
			ChangeSprite(_choppedLimeTexture);
		}
	}

	public void Peel()
	{
		State = IngredientState.Peeled;
		RemoveFromGroup("Raw");
		AddToGroup("Peeled");

		if (IsInGroup("Apple"))
		{
			ChangeSprite(_peeledAppleTexture);
		}

		if (IsInGroup("Kiwi"))
		{
			ChangeSprite(_peeledKiwiTexture);
		}

		if (IsInGroup("Banana"))
		{
			ChangeSprite(_peeledBananaTexture);
		}
		if (IsInGroup("Grapes"))
		{
			ChangeSprite(_peeledGrapesTexture);
		}
	}

	public void changeStateChopped()
	{
		State = IngredientState.Chopped;
		RemoveFromGroup("Peeled");
		AddToGroup("Chopped");
	}
}
