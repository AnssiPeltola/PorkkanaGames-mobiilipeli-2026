using Godot;
using System;

public partial class RecipeSelections : Control
{
	public override void _Ready()
	{
		Button backButton = GetNode<Button>("CanvasLayer/BackButton");
		Button startPastaButton = GetNode<Button>("CanvasLayer/StartPastaButton");
		Button startFruitButton = GetNode<Button>("CanvasLayer/StartFruitButton");

		if (backButton == null)
		{
			GD.PrintErr("BackButton not found! Check node path.");
			return;
		}

		backButton.Pressed += OnBackPressed;
		startPastaButton.Pressed += StartPastaLevel;
		startFruitButton.Pressed += StartFruitLevel;
	}

	private void OnBackPressed()
	{
		GD.Print("Back button pressed"); // debug

		GetTree().ChangeSceneToFile("res://Scenes/Menus/MainMenu.tscn");
	}

	private void StartPastaLevel()
	{
		GD.Print("Starting pasta level!");
		GameManager.Instance.ResetScore();
		GameManager.Instance.currentLevel = 1;
		GameManager.Instance.levelOneRequired = 7;
		GameManager.Instance.levelTwoRequired = 4;
    	GameManager.Instance.levelThreeRequired = 5;
		FadeTransition.ChangeSceneWithFade(LevelOrder.GetLevelPath(GameManager.Instance.currentLevel));
	}

	// Set Fruit level score requirements here and start level 1 in fruit recipe
	private void StartFruitLevel()
	{
		GD.Print("Starting pasta level!");
		GameManager.Instance.ResetScore();
		GameManager.Instance.currentLevel = 4;
		GameManager.Instance.levelOneRequired = 8;
		GameManager.Instance.levelTwoRequired = 5;
    	GameManager.Instance.levelThreeRequired = 5;
		FadeTransition.ChangeSceneWithFade(LevelOrder.GetLevelPath(GameManager.Instance.currentLevel));
	}
}
