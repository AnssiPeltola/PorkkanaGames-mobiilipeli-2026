using Godot;
using System;

public partial class LevelTwo : BaseLevel
{
	public override void _Ready()
	{
		CurrentLevel = 2;
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionJustPressed("ui_select"))
		{
            // Test print
			CurrentComplete();
			// BaseLevel method
			SignalComplete();
		}
	}
}
