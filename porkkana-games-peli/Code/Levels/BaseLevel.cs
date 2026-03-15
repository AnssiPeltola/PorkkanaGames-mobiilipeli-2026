using Godot;
using System;


/* Base class for all levels to inherit from.
 *
 * Could change this into Abstract class to force intialisation to include CurrentLevel number
 */


public partial class BaseLevel : Node
{
	public int CurrentLevel { get; set; }

	public virtual void SignalComplete()
	{
		// Contact SceneControl's
		// Instance "current"
		// Give it the current CurrentLevel
		SceneControl.Current.OnComplete(CurrentLevel);
	}

	public virtual void CurrentComplete()
	{
		GD.Print("From level ", CurrentLevel, " We are Complete!");
	}

}
