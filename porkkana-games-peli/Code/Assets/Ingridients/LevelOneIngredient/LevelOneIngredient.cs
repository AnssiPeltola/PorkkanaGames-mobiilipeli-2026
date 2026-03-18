using Godot;
using System;

public partial class LevelOneIngredient : CharacterBody2D
{
	private bool _dragging = false;
	[Export] private int _clickRadius = 40;
	[Export] public Texture2D IngredientTexture;
	private Sprite2D _sprite;

	public override void _Ready()
	{
		// We get this scenes Sprite2D node in variable _sprite
		_sprite = GetNode<Sprite2D>("Sprite2D");
		if (_sprite != null && IngredientTexture != null)
        {
            _sprite.Texture = IngredientTexture;
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
