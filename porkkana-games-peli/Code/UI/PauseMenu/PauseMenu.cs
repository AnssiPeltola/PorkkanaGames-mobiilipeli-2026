using Godot;
using System;

// https://www.youtube.com/watch?v=e9-WQg1yMCY
public partial class PauseMenu : Control
{
	private BaseButton _resumeButton;
	private BaseButton _quitButton;
	private AnimationPlayer _animationPlayer;

	public override void _Ready()
	{
		_resumeButton = GetNode<TextureButton>("PanelContainer/VBoxContainer/Button");
		_quitButton = GetNode<TextureButton>("PanelContainer/VBoxContainer/Button2");
		_animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");

		_resumeButton.Pressed += Resume;
		_quitButton.Pressed += Quit;
		_animationPlayer.AnimationFinished += OnAnimationFinished;

		ClosePause();

	}
	public void Resume()
	{
		GetTree().Paused = false;
		_animationPlayer.PlayBackwards("blur");
	}

	public void Pause()
	{
		OpenPause();
		GetTree().Paused = true;
		_animationPlayer.Play("blur");
	}

	public void Quit()
	{
		GetTree().Paused = false;
		GameManager.Instance.ResetLevelProgress();
		GameManager.Instance.ResetScore();
		ClosePause();
		FadeTransition.ChangeSceneWithFade("res://Scenes/Menus/MainMenu.tscn");

	}

	public void OpenPause()
	{
		Show();
	}

	public void ClosePause()
	{
		Hide();
	}

	// Hide pause UI only after the blur-out animation ends on resume
	private void OnAnimationFinished(StringName animationName)
	{
		if (animationName == "blur" && !GetTree().Paused)
		{
			ClosePause();
		}
	}
}
