using Godot;
using System;
// using System.Numerics;

public partial class FryingIngredient : CharacterBody2D
{
	// https://www.w3schools.com/cs/cs_enums.php - An enum is a special "class" that represents a group of constants.
	public enum IngredientState
	{
		Raw,
		Chopped,
		Cooked
	}

	private bool _dragging = false;
	[Export] private int _clickRadius = 32;
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
	private Sprite2D _sprite;

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

	// This function is called for every input event (mouse, keyboard, touch, etc.)
   public override void _Input(InputEvent e)
    {
		// Only react to screen touch events (mobile / mouse click)
        if (e is InputEventScreenTouch touch)
        {
			// Check if the touch is close enough to this object to start dragging and set _dragging true
			if ((touch.Position - GlobalPosition).Length() < _clickRadius)
			{
            	_dragging = touch.Pressed;
			}
        }

		// touchtap.Pressed prevents releasing touch to register as click.
		if (e is InputEventScreenTouch touchtap && touchtap.Pressed)
		{
			if ((touchtap.Position - GlobalPosition).Length() < _clickRadius && IsInDropZone)
			{
				OpenMiniGame = true;
				GD.Print("Open minigame!");
				_sprite.Hide();
			}
		}
    }

	// if dragging false does nothing
	public override void _PhysicsProcess(double delta)
    {
        if (!_dragging)
		{
			return;
		}

		// Get the current position of finger/mouse
        Vector2 target = GetGlobalMousePosition();
        Vector2 direction = target - GlobalPosition;

        Velocity = direction / (float)delta;
		// Makes the move using Godot physics
        MoveAndSlide();
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

	// Function that changes this scenes Sprite2D texture to new
	public void ChangeSprite(Texture2D newTexture)
	{
		_sprite.Texture = newTexture;
	}
}
