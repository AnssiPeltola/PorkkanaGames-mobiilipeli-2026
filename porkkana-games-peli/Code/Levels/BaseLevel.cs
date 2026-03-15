using Godot;
using System;


/* Base class for all levels to inherit from.
 *
 * Consider:
 *  Change this into Abstract class to force intialisation to include CurrentLevel number
 */


public partial class BaseLevel : Node
{
	public int CurrentLevel { get; set; }

	public virtual void SignalComplete()
	{
		// Contact SceneControl's
		// Instance: "Current"
		// Give it the CurrentLevel (int)
		SceneControl.Current.OnComplete(CurrentLevel);
	}

	// Just a test print method to inherit
	public virtual void CurrentComplete()
	{
		GD.Print("From level ", CurrentLevel, " We are Complete!");
	}

}
