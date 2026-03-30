// https://docs.godotengine.org/en/stable/classes/class_line2d.html
// https://docs.godotengine.org/en/stable/classes/class_inputevent.html#class-inputevent

// NOTE:
// Create lines from dragging inputs
// Put the lines in "DrawingContainer"
// Touch = Create new Line2D()
// Drag = Add points to the line
// EndTouch = Stop adding points save the Line2D and prepare for new "cut"

// TODO:
// Add a keybind to clear all lines
// => (Kill / destroy all childs of DrawingContainer)

// NOTE *9.
// Modulo and arrays
// Modulo that is = to array.Lenght makes a perfect looping array.
// example step 1.
// Index = (0 + 1) - (0 * 5) = 1
// Index = (1 + 1) - (0 * 5) = 2
// Index = (2 + 1) - (0 * 5) = 3
// Index = (3 + 1) - (0 * 5) = 4
// Index = (4 + 1) - (0 * 5) = 5 --> End of array next loop % 5 = 1
// Index = (5 + 1) - (1 * 5) = 6 - 5
//                           = 1 again!!
// SO:
// To create infinite looping array simply set arrayIndex = (arrayIndex + 1 ) % array.Length;

using Godot;
using System;

public partial class Drawing : Node2D
{
 
 
	// Declaration
	private Node2D _drawingContainer;

	//----------
	// lines
	//----------
	// Since the cut lines are temporary
	// Create new node from code (Line2D)
	// for now Declare it as null;
	private Line2D _currentLine;
	// Is the player CURRENTLY drawing
	private bool _isDrawing = false;

	private Color[] _colors = new Color[]
	{
		Colors.Red, Colors.Black, Colors.Blue,
		Colors.Yellow, Colors.Green, Colors.Purple,
		Colors.White
	};
	private int _colorIndex = 0;


	public override void _Ready()
	{
		// Give reference 
		_drawingContainer = GetNode<Node2D>("DrawingContainer");
	}

	// Track all inputs
	public override void _Input(InputEvent e)
	{
		// If input is touch -->
		if (e is InputEventScreenTouch touch)
		{
			// Touch start
			if (touch.Pressed)
			{
				GD.Print("Touch @: " + touch.Position);
				// Create new Line2D()
				// Add it as a child for the DrawingContainer node
				_currentLine = new Line2D();
				_drawingContainer.AddChild(_currentLine);
 
				// Properties
				_currentLine.Width = 5f;
				// _currentLine.DefaultColor = new Color(0, 0, 0);
				_currentLine.DefaultColor = _colors[_colorIndex];
				_colorIndex = (_colorIndex + 1) % _colors.Length; // *9.

				_currentLine.AddPoint(touch.Position);

				_isDrawing = true;
			}
			// Touch end
			else
			{
				GD.Print("Touch END @: " + touch.Position);
				// Line always needs 2 points this is not needed if drag happens.
				_currentLine.AddPoint(touch.Position);
				_isDrawing = false;
			}
		}
		// If input is drag -->
		// AND if _isDrawing = true
		else if (e is InputEventScreenDrag drag && _isDrawing)
		{
			// Create end points for the line every frame == drawing works
			_currentLine.AddPoint(drag.Position);
		}

		// Run KillChildren using space
		else if (e is InputEventKey key && key.Pressed && key.Keycode == Key.Space)
		{
			KillChildren();
		}
	}

	// Destroy all drawings on the screen
	private void KillChildren()
	{
		 foreach (Node child in _drawingContainer.GetChildren())
		 {
			 child.QueueFree();
		 }
		 GD.Print("Cleared all drawings!");
	}

	/*
	 * Kill all children of the node using custom bind "K"
	// Added custom bind K to run Killchildren
	public override void _Process(double delta)
	{
		if (Input.IsActionPressed("KillChildren()"))
		{
			KillChildren();
		}
	}
	*/
}
