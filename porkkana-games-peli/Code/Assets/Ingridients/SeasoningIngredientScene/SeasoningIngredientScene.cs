using Godot;
using System;

public partial class SeasoningIngredientScene : CharacterBody2D
{
	public enum SeasoningKind
	{
		None,
		Pepper,
		Salt,
		Basil
	}
	private bool _dragging = false;
	[Export] private int _clickRadius = 32;
	[Export] public SeasoningKind SeasonKind = SeasoningKind.None;
	private Sprite2D _sprite;
	private Texture2D _pepperTexture;
	private Texture2D _saltTexture;
	private Texture2D _basilTexture;
	private CollisionShape2D _shakerCollision;
	private CollisionShape2D _basilCollision;

    public override void _Ready()
    {
		// We get this scenes Sprite2D node in variable _sprite
        _sprite = GetNode<Sprite2D>("Sprite2D");

		// Load texture and collision by what SeasoningKind is set from Editor
		if (SeasonKind == SeasoningKind.Pepper)
		{
			_pepperTexture = GD.Load<Texture2D>("res://Art/Assets/Ingridients/SaltPepper/pepper-shaker-v1.png");
			_shakerCollision = GetNode<CollisionShape2D>("ShakerCollision");
			ChangeSprite(_pepperTexture);
			_shakerCollision.Disabled = false;
		}

		if (SeasonKind == SeasoningKind.Salt)
		{
			_saltTexture = GD.Load<Texture2D>("res://Art/Assets/Ingridients/SaltPepper/salt-shaker-v1.png");
			_shakerCollision = GetNode<CollisionShape2D>("ShakerCollision");
			ChangeSprite(_saltTexture);
			_shakerCollision.Disabled = false;
		}

		if (SeasonKind == SeasoningKind.Basil)
		{
			_basilTexture = GD.Load<Texture2D>("res://Art/Assets/Ingridients/Basil/basil-leaf-v2.png");
			_basilCollision = GetNode<CollisionShape2D>("BasilCollision");
			ChangeSprite(_basilTexture);
			_basilCollision.Disabled = false;
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

	// Function that changes this scenes Sprite2D texture to new
	public void ChangeSprite(Texture2D newTexture)
	{
		_sprite.Texture = newTexture;
	}
}