using Godot;
using System;

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
