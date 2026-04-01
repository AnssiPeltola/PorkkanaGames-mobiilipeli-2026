using Godot;
using System;

public partial class FadeTransition : CanvasLayer
{
	// https://www.youtube.com/watch?v=QC_mn2tX6n0
	// https://www.youtube.com/watch?v=Shj_QVwrefY
	// https://www.youtube.com/watch?v=_4_DVbZwmYc
	private static AnimationPlayer _animationPlayer;
	private static ColorRect _colorRect;
	private static string _targetScenePath;

	public override void _Ready()
	{
		_animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
		_colorRect = GetNode<ColorRect>("ColorRect");

		_animationPlayer.AnimationFinished += OnAnimationFinished;
		// This scene is on autoload so it has to be set false, else we got blackscreen always
		_colorRect.Visible = false;
	}

	// Static so other scripts can call FadeTransition.ChangeSceneWithFade function (GameManager.cs)
	public static void ChangeSceneWithFade(string scenePath)
	{
		// Save target scene path for the fade-out finished callback
		_targetScenePath = scenePath;
		// Show fullscreen overlay
		_colorRect.Visible = true;
		// Start fade_to_black animation
		_animationPlayer.Play("fade_to_black");
	}

	private void OnAnimationFinished(StringName animName)
	{
		if (animName == "fade_to_black")
		{
			// Swap scene only after the screen is fully black
			// Switch scene - https://docs.godotengine.org/en/latest/tutorials/scripting/change_scenes_manually.html
			GetTree().ChangeSceneToFile(_targetScenePath);
			// Fade back from black to gameplay
			_animationPlayer.Play("fade_to_normal");
		}
		else if (animName == "fade_to_normal")
		{
			// Hide overlay when transition is finished
			_colorRect.Visible = false;
		}
	}
}
