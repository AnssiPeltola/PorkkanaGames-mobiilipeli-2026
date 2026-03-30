// https://docs.godotengine.org/en/stable/classes/class_inputevent.html#class-inputevent
// NOTE:
// 1. Add vector start and end
// 2. Start and End tracking vectors at input and drag
// 3. Calculate the distance = ( (A,B) - (A,B) )
		// float distance = _vectorStart.DistanceTo(_vectorEnd);

using Godot;
using System;

public partial class MovementTest : Node2D
{
	// Declaration field
	private Sprite2D _spriteMovementTest;

	// 1. 
	private Vector2 _vectorStart = Vector2.Zero;
	private Vector2 _vectorEnd = Vector2.Zero;

	public override void _Ready()
	{
		// Initialize the value
		_spriteMovementTest = GetNode<Sprite2D>("Jep");
	}

	// trach any type of input
	public override void _Input(InputEvent e)
	{
		// Touch input tracking
		if (e is InputEventScreenTouch touch)
		{
			// Touch input start
			// If toch then ->>
			if (touch.Pressed)
			{
				GD.Print("Touch @: " + touch.Position);
				_spriteMovementTest.Position = touch.Position;

				// 2.
				_vectorStart = touch.Position;

				// Change color
				_spriteMovementTest.Modulate = new Color(1, 0, 0); // Red when touching
			}
			// Touch input ended (or not happened)
			else
			{
				// 2.
				// Vector end position
				_vectorEnd = touch.Position;
				GD.Print("Touch END @: " + touch.Position);

				// Change color
				_spriteMovementTest.Modulate = new Color(1, 1, 1); // White when released
				 
				// Print _vectorStart location constantly
				GD.Print($"New Vector End: {_vectorEnd}");

				// 3.
				// float distance = _vectorStart.DistanceTo(_vectorEnd);
				float _vectorDistance = _vectorStart.DistanceTo(_vectorEnd);
				GD.Print($"Distance for vectors was {_vectorDistance}");
			}
		}
		// Drag input tracking
		// If drag then ->>
		if (e is InputEventScreenDrag drag)
		{
			GD.Print("Dragging to: " + drag.Position);
			_spriteMovementTest.Position = drag.Position;
		}
	}	
}
