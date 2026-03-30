// TODO:
// 1. Add vector start and end
// 2. Start and End tracking vectors at input and drag
// 3. New vector to track hoow many pixel each drag is



using Godot;
using System;

public partial class MovementTest : Node2D
{
	// Declaration field
	private Sprite2D _sprite;

	// 1. 
	private Vector2 _vectorStart = Vector2.Zero;
	private Vector2 _vectorEnd = Vector2.Zero;

	// 3.
	private Vector2 _dragDistance;

	public override void _Ready()
	{
		// Initialize the value
		_sprite = GetNode<Sprite2D>("Jep");
	}

	// trach any type of input
	public override void _Input(InputEvent e)
	{
		// if input is touch screen
		if (e is InputEventScreenTouch touch)
		{
			// Touch input start
			if (touch.Pressed)
			{
				GD.Print("Touch @: " + touch.Position);
				_sprite.Position = touch.Position;
				// 2.
				_vectorStart = touch.Position;
				// Change color
				_sprite.Modulate = new Color(1, 0, 0); // Red when touching
			}
			// Touch input ended (or not happened)
			else
			{
				// 2.
				// Vector end position
				_vectorEnd = touch.Position;
				GD.Print("Touch END @: " + touch.Position);
				// Change color
				_sprite.Modulate = new Color(1, 1, 1); // White when released

				// 3. Calculate drag distance
				float distance = _vectorStart.DistanceTo(touch.Position);
			}
		}
		// Drag tracking
		// If drag then ->>
		if (e is InputEventScreenDrag drag)
		{
			GD.Print("Dragging to: " + drag.Position);
			 // TEST:
			// Add a delay / drag for the Sprite2D
			// _sprite.Position = drag.Position.Lerp(drag.Position, 0.3f);
			_sprite.Position = drag.Position;
			// 2.
			// _vectorEnd = drag.Position;
		}

		 // Print _vectorStart location constantly
		GD.Print($"Vector Start: {_vectorStart}");
		
		// Print _vectorStart location constantly
		GD.Print($"Vector End: {_vectorEnd}");
	}
}
