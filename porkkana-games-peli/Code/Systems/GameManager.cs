using Godot;
using System;

public partial class GameManager : Node
{
	// ---------------------------------------------------------
	// Singleton
	// ---------------------------------------------------------
	public static GameManager Instance { get; private set; }

	public override void _Ready()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else if (Instance != this)
		{
			QueueFree();
			return;
		}
	}

	// ---------------------------------------------------------
	// Level Score System
	// ---------------------------------------------------------
	private int _levelOneScore = 0;

	[Export] public int RequiredGoodItems { get; set; } = 2;
	[Export] public int RequiredBadItems { get; set; } = 1;

	public int LevelOneScore
	{
		get => _levelOneScore;
		set
		{
			_levelOneScore = Mathf.Clamp(value, 0, Int32.MaxValue);
			GD.Print($"Points now: {LevelOneScore} Needed points: {RequiredGoodItems + RequiredBadItems}");
		}
	}

	public void GoodItemEntered()
	{
		LevelOneScore += 1;
		CheckLevelOneComplete();
	}

	public void GoodItemExited()
	{
		LevelOneScore -= 1;
	}

	public void BadItemEntered()
	{
		LevelOneScore += 1;
		CheckLevelOneComplete();
	}

	private void CheckLevelOneComplete()
	{
		if (LevelOneScore >= (RequiredGoodItems + RequiredBadItems))
		{
			GD.Print("All items in right positions! Level completed!");
			SceneControl.Current?.OnComplete(1);
		}
	}

	// ---------------------------------------------------------
	// Recipe Selection
	// ---------------------------------------------------------
	public RecipeData SelectedRecipe { get; set; }
}
