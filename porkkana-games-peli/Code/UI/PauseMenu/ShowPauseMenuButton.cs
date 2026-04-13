using Godot;
using System;

public partial class ShowPauseMenuButton : Button
{
	private PauseMenu _pauseMenu;

	public override void _Ready()
	{
		// PauseMenu is autoloaded Scene
		_pauseMenu = GetNode<PauseMenu>("/root/PauseMenu");

		Pressed += OnPressed;
	}

	private void OnPressed()
	{
		_pauseMenu.Pause();
	}
}