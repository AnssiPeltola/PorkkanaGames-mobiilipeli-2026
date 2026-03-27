using Godot;
using System;

public partial class CutArea : Area2D
{

	// Introduce a new signal for the parent (Minigame) to register cuts
	[Signal] public delegate void CutRegisteredEventHandler();

	public override void _Ready()
	{
		// Godot automatically checks if inputs are within collision shape
		InputPickable = true;

		// InputEvent is Area2D's own Signal
		// It is emmitted when 
		InputEvent += OnInputEvent;
	}

	// Parameters:
		// (Node viewport) = is a reference to the game window/screen. (If multiple viewports like splitscreen)
		// InputEvent e = actual input data. (Godot tells us what input just happened)
		// int shapeIdx = can specify which collision shape was touched if you have multiple
	//
	// Explanation:
	// An alarm was set:
		// viewpor = in which house was the alarm tirggered
		// e = whta kind of alarm (fire, motion sensor, window, door?)
		// which sensor sent the alarm
	private void OnInputEvent (Node viewport, InputEvent @event, long shapeIdx)
	{
		if (@event is InputEventScreenTouch touch && touch.Pressed)
		{
			GD.Print("Cut detected inside Area2D");
			// Emmit a signal that a proper input was received in the area
			EmitSignal(SignalName.CutRegistered);
		}
	}
}
