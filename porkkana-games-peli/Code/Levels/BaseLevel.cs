using Godot;
using System;


/* Base class for all levels to inherit from.
 *
 */


public class BaseLevel : Node
{

    // Reading allowed, Writing allower
    public int LevelNumber { get; set; }

    public override void _Ready()
    {
        Startup();
    }

    // Virtual means it can be overriden
    // Or called with base.Startup() to run the original
    // And added functinalyt
    public virtual void Startup()
    {
        GD.Print("Level", LevelNumber," Loaded");
    }

    public virtual void OnLevelComplete() 
    {
        // Test default
        GD.Print("Level completed!"); 

        // Every level sends Im done message and give current
        // level number
        // to SceneControl.cs
        public virtual void SignalCompletion()
        {
            // Test print
            GD.Print("Level Completed");
            // Tell SceneController current LevelNumber
            // and that we are done.

            //TODO:
            // Tell SceneControl we are done and see SceneControl
            // how to handle changing scenes.
        }
    }

}

