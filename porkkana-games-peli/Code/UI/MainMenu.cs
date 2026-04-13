using Godot;
using System;

public partial class MainMenu : Control
{
	public override void _Ready()
	{
		GetNode<Button>("CanvasLayer/ButtonContainer/StartButton").Pressed += OnStartPressed;
		GetNode<Button>("CanvasLayer/ButtonContainer/SettingsButton").Pressed += OnSettingsPressed;
		GetNode<Button>("CanvasLayer/ButtonContainer/ExitButton").Pressed += OnExitPressed;
		GetNode<Button>("CanvasLayer/LangFi").Pressed += OnLangFiPressed;
		GetNode<Button>("CanvasLayer/LangEng").Pressed += OnLangEngPressed;
}

	private void OnLangEngPressed()
	{
		TranslationServer.SetLocale("en");
		GD.Print("Current Locale: " + TranslationServer.GetLocale());
	}

	private void OnLangFiPressed()
	{
		TranslationServer.SetLocale("fi");
		GD.Print("Current Locale: " + TranslationServer.GetLocale());
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
