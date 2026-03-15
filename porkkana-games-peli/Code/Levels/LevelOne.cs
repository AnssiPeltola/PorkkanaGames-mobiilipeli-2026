using Godot;
using System;

// All levels inherit from BaseLevel and BaseLevel inherits Node
public partial class LevelOne : BaseLevel
{
	public override void _Ready()
	{
		LevelNumber = 1;

	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("ui_select"))
		{
			// $print = fstring from Python
			GD.Print($"Level {LevelNumber} Complete!"); 
			// Baselevel Method
			SignalCompletion();
		}
	}
}
