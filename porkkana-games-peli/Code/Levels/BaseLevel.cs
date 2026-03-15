using Godot;
using System;


/* Base class for all levels to inherit from.
 *
 */


public partial class BaseLevel : Node
{
	public int CurrentLevel { get; set; }

	public override void _Ready()
	{
		// CurrentComplete();
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
		// Give it the current CurrentLevel
		SceneControl.Current.OnComplete(CurrentLevel);
	}

	public virtual void CurrentComplete()
	{
		GD.Print("Level ", CurrentLevel, " Complete!");
	}

}
