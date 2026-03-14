using Godot;
using System;


/* Base class for all levels to inherit from.
 *
 */


public class BaseLevel : Node
{

    private int LevelNumber { get; set; }

    // Virtual => base implementation, can be overwritten if need.
    public virtual void OnLevelComplete() 
    {
        // Test default
        GD.Print("Level completed!"); 

        // Call ScenControl
        // Ask for currenLevelNumber
        SceneController.OnLevelComplete(LevelNumber);
    }

}
