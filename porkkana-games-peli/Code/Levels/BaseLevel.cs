using Godot;
using System;


/* Base class for all levels to inherit from.
 *
 */


public partial class BaseLevel : Node
{
	public int LevelNumber { get; set; }

	public override void _Ready()
	{
		// from python f strings
		GD.Print($"Level {LevelNumber} Loaded");
	}

	// Virtual means it can be overriden
	public virtual void OnLevelComplete() 
	{
		// Test default
		GD.Print("Level completed!"); 
	}

	public virtual void SignalCompletion()
	{
		// Contact SceneControl's
		// Instance "current"
		// Give it the current LevelNumber
		SceneControl.Current.OnComplete(LevelNumber);
	}

}
