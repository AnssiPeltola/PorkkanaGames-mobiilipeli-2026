using Godot;
using System;
using System.Collections.Generic;

/*
 * Purpose:
 *      Let all ingridients inherit from this BaseIngridient
 */

public partial class BaseIngridient : CharacterBody2D
{
	private bool _dragging = false;
	// Index of the finger currently controlling this ingredient (-1 means none).
	private int _activeTouchId = -1;
	// Latest world position of the controlling finger.
	private Vector2 _activeTouchPosition = Vector2.Zero;
	// Dictionary that stores key value pairs (int and Vector2)
	private Dictionary<int, Vector2> ingredientTouchDictionary = new Dictionary<int, Vector2>();
	[Export] protected int _clickRadius = 50;
	// protected for class inheritance (class inheriting CAN access this but nobody else)
	protected Sprite2D _sprite;

	// This function is called for every input event (mouse, keyboard, touch, etc.)
	public override void _Input(InputEvent e)
	{
		// Touch down/up events: claim or release one touch owner for this ingredient.
		// InputEvenScreenTouch counts touch index in the case of a multi-touch event. One index = one finger.
		if (e is InputEventScreenTouch touch)
		{
			// When touch is down
			if (touch.Pressed)
			{
				// Claim this touch only if we are not already claimed and the tap is close enough.
				if (_activeTouchId == -1 && (touch.Position - GlobalPosition).Length() < _clickRadius)
				{
					_activeTouchId = touch.Index;
					// GD.Print(_activeTouchId); // Test
					_activeTouchPosition = touch.Position;
					// GD.Print(_activeTouchPosition); // Test
					_dragging = true;
				}
			}
			// When touch is up
			else
			{
				ingredientTouchDictionary.Remove(touch.Index);

				// Stop dragging only if the released touch is the one we claimed.
				if (touch.Index == _activeTouchId)
				{
					_activeTouchId = -1;
					_dragging = false;
					Velocity = Vector2.Zero;
				}
			}
			return;
		}

		// Drag event fires while any finger moves on screen.
		if (e is InputEventScreenDrag drag)
		{
			// Keep latest position for this finger index (useful for multi-touch tracking/debug).
			ingredientTouchDictionary[drag.Index] = drag.Position;

			// Move this ingredient only when the moving finger is the owner of this ingredient.
			if (drag.Index == _activeTouchId)
			{
				// Physics step reads this target position and moves toward it.
				_activeTouchPosition = drag.Position;
			}
			return;
		}
	}

	// if dragging false does nothing
	public override void _PhysicsProcess(double delta)
	{
		if (!_dragging)
		{
			return;
		}

		// Follow the currently claimed touch/finger.
		Vector2 target = _activeTouchPosition;
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
