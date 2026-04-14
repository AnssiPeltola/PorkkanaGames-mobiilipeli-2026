using Godot;
using System;

public partial class MainMenu : Control
{
	public override void _Ready()
	{
		GetNode<TextureButton>("CanvasLayer/ButtonContainer/StartButton").Pressed += OnStartPressed;
		GetNode<TextureButton>("CanvasLayer/ButtonContainer/SettingsButton").Pressed += OnSettingsPressed;
		GetNode<TextureButton>("CanvasLayer/ButtonContainer/ExitButton").Pressed += OnExitPressed;
	}

	private void OnStartPressed()
	{
		// Go to recipe selection
		GetTree().ChangeSceneToFile("res://Scenes/Menus/RecipeSelections.tscn");
	}

	private void OnSettingsPressed()
	{
		// Go to settings menu
		GetTree().ChangeSceneToFile("res://Scenes/Menus/Settings.tscn");
	}

	private void OnExitPressed()
	{
		GetTree().Quit();
	}
}
