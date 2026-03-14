using Godot;
using System;

/*
 * Purpose:
 *  SceneControl will handle all the scene changes
 * Why?:
 *  - Easier to scale and edit
 *  - Can also add Score screens and pausing etc.
 * 
 * How it works:
 *  Simply listen to signals and handle scene changes based on levelComplete Signals!
 *      HOW?:
 *          This is how Godot handles changing scenes:
 *          GetTree().ChangeSceneToFile("res://path/to/scene.tscn");
 *          https://docs.godotengine.org/en/stable/tutorials/scripting/change_scenes_manually.html
 *
 */

public partial class SceneControl : Node
{
    

}

public void onLevelComplete(int CurrentLevelNumber) {

// Check what is the next from LevelOrder
string nextLevelPath = "";

}
