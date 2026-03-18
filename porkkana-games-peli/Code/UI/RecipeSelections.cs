using Godot;
using System;

public partial class RecipeSelections : Control
{
	public override void _Ready()
	{
		Button backButton = GetNode<Button>("CanvasLayer/BackButton");

		if (backButton == null)
		{
			GD.PrintErr("BackButton not found! Check node path.");
			return;
		}

		backButton.Pressed += OnBackPressed;
	}

	private void OnBackPressed()
	{
		GD.Print("Back button pressed"); // debug

		GetTree().ChangeSceneToFile("res://Scenes/Menus/MainMenu.tscn");
	}
}
