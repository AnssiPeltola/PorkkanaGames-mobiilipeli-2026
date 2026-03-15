using Godot;
using System;

// All levels inherit from BaseLevel and BaseLevel inherits Node
public partial class LevelOne : BaseLevel
{
	public override void _Ready()
	{
		CurrentLevel = 1;
	}

	public override void _Process(double delta)
	{
		// GODOT Spacbar is default assigned to ui_select & ui_accept
		// So to test press spacebar to run scene changing sequence
		if (Input.IsActionJustPressed("ui_select"))
		{
			// Test Print from Baselevel
			CurrentComplete();

			// Start the changeScene sequence
			// @Baselevel 
			//
			// SignalComplete() => SceneControl.Current.OnComplete(CurrentLevel)
			SignalComplete();
		}
	}
}
