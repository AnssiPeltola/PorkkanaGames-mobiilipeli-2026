using Godot;
using System;

/*
 * Purpose:
 * - SceneControl will handle all the scene changes
 * Why?:
 * - Easier to scale and edit
 * - Debugging
 *
 * This is how Godot handles changing scenes:
 *  - GetTree().ChangeSceneToFile("res://path/to/scene.tscn");
 *      Source: https://docs.godotengine.org/en/stable/tutorials/scripting/change_scenes_manually.html
 *
 * NOTE:
 * SceneControl cannot be a static class since we HAVE TO inherit : Node
 * For the sake of using GetTree() and methods belonging to Node
 * Therefore when game starts autoloader creates a new instance of SceneControl,
 * give it name "Current" and reference to it when calling.
 */

public partial class SceneControl : Node
{
   // Create new Variable Current with read write permissions
   public static SceneControl Current { get; set; }

   public override void _Ready()
   {
	   // When Godot autoloader creates this object, "this" is that object
	   // Store a reference to it in "Current"
	   // Now talk to "this" instance by talking to "Current"
	   // NOTE:
	   // Do not use "new" here or we will pump out infinite instances.
	   // This is why we just refer "this" to Current
	   Current = this;
   }

   // Run the Scene changing sequence
   public void OnComplete(int CurrentLevelNumber)
   {
	   // Get Next level (int) from LevelOrder.cs
	   int nextLevel = LevelOrder.GetNextLevel(CurrentLevelNumber);

	   // Get Next level PATH from LevelOrder.cs
	   string nextPath = LevelOrder.GetLevelPath(nextLevel);

	   // Change scene
	   GetTree().ChangeSceneToFile(nextPath);
   }
}
