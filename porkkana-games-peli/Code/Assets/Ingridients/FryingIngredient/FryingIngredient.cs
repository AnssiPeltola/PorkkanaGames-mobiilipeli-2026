using Godot;
using System;
// using System.Numerics;

public partial class FryingIngredient : BaseIngridient
{
	// https://www.w3schools.com/cs/cs_enums.php - An enum is a special "class" that represents a group of constants.
	public enum IngredientState
	{
		Raw,
		Chopped,
		Cooked
	}
	private Texture2D _tomatoTexture;
	private Texture2D _onionTexture;
	private Texture2D _carrotTexture;
	private CollisionShape2D _tomatoCollision;
	private CollisionShape2D _onionCollision;
	private CollisionShape2D _carrotCollision;

	// Init IngredientState. For testing its now Chopped
	public IngredientState State { get; set; } = IngredientState.Chopped;

	// When public we can set this as true or false in other code where this object is used
	public bool IsInDropZone { get; set; } = false;
	public bool IsInFryingPan { get; set; } = false;
	public bool OpenMiniGame { get; set; } = false;

	public override void _Ready()
	{
		// Load Nodes and textures
		_sprite = GetNode<Sprite2D>("Sprite2D");

		// Load Textures for ingredients
		_onionTexture = GD.Load<Texture2D>("res://Art/Assets/Ingridients/Onion/onion.png");
		_carrotTexture = GD.Load<Texture2D>("res://Art/Assets/Ingridients/Carrot/carrot.png");
		_tomatoTexture = GD.Load<Texture2D>("res://Art/Assets/Ingridients/Tomato/tomato.png");

		// Load Collisions for ingredients
		_tomatoCollision = GetNode<CollisionShape2D>("TomatoCollision");
		_onionCollision = GetNode<CollisionShape2D>("OnionCollision");
		_carrotCollision = GetNode<CollisionShape2D>("CarrotCollision");

		// Change Sprite2D Texture and Enable CollisionShape2D by group. Collisions in editor is set "Disabled".
		if (this.IsInGroup("Tomato"))
		{
			ChangeSprite(_tomatoTexture);
			GD.Print(_tomatoTexture.GetSize());
			Vector2 textreSize = _sprite.Texture.GetSize();
			Vector2 newSize = new Vector2(50, 50);
			_tomatoCollision.Disabled = false;
			_sprite.Scale = newSize / textreSize;
		}

		if (this.IsInGroup("Onion"))
		{
			ChangeSprite(_onionTexture);
			_onionCollision.Disabled = false;
		}

		if (this.IsInGroup("Carrot"))
		{
			ChangeSprite(_carrotTexture);
			_carrotCollision.Disabled = false;
		}
	}

	public override void _Input(InputEvent e)
	{
		base._Input(e);

		// touchtap.Pressed prevents releasing touch to register as click.
		if (e is InputEventScreenTouch touchtap && touchtap.Pressed)
		{
			if ((touchtap.Position - GlobalPosition).Length() < base._clickRadius && IsInDropZone)
			{
				OpenMiniGame = true;
				GD.Print("Open minigame!");
				_sprite.Hide();
			}
		}
	}

	// Use after minigame is completed?
	public void Chop()
	{
		State = IngredientState.Chopped;

		RemoveFromGroup("Raw");
		AddToGroup("Chopped");
	}

	public void changeStateCooked()
	{
		State = IngredientState.Cooked;
		RemoveFromGroup("Chopped");
		AddToGroup("Cooked");
	}
}
