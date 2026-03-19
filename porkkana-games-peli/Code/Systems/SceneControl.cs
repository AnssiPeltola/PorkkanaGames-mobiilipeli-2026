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
	public static SceneControl Current { get; set; }

	public override void _Ready()
	{
		Current = this;
	}

	public void OnComplete(int CurrentLevelNumber)
	{
		int nextLevel = LevelOrder.GetNextLevel(CurrentLevelNumber);
		string nextPath = LevelOrder.GetLevelPath(nextLevel);
		GetTree().ChangeSceneToFile(nextPath);
	}
}
