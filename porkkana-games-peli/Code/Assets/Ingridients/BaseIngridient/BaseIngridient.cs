using Godot;
using System;

/*
 * Purpose:
 *      Let all ingridients inherit from this BaseIngridient
 */


public partial class BaseIngridient : CharacterBody2D
{
	private bool _dragging = false;
	[Export] protected int _clickRadius = 50;
	
	// protected for class inheritance (class inheriting CAN access this but nobody else)
	protected Sprite2D _sprite;

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
