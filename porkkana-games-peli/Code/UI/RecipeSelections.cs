using Godot;
using System;

public partial class RecipeSelections : Control
{
	public override void _Ready()
	{
		Button backButton = GetNode<Button>("CanvasLayer/BackButton");
		Button startPastaButton = GetNode<Button>("CanvasLayer/StartPastaButton");

		if (backButton == null)
		{
			GD.PrintErr("BackButton not found! Check node path.");
			return;
		}

		backButton.Pressed += OnBackPressed;
		startPastaButton.Pressed += StartPastaLevel;
	}

	private void OnBackPressed()
	{
		GD.Print("Back button pressed"); // debug

		GetTree().ChangeSceneToFile("res://Scenes/Menus/MainMenu.tscn");
	}

	private void StartPastaLevel()
	{
		GD.Print("Starting pasta level!");
		FadeTransition.ChangeSceneWithFade("res://Scenes/Levels/LevelOne/LevelOne.tscn");
	}
}
