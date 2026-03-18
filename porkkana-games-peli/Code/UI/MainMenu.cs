using Godot;

public partial class MainMenu : Control
{
	public override void _Ready()
	{
		GetNode<Button>("CanvasLayer/ButtonContainer/StartButton").Pressed += OnStartPressed;
		GetNode<Button>("CanvasLayer/ButtonContainer/SettingsButton").Pressed += OnSettingsPressed;
		GetNode<Button>("CanvasLayer/ButtonContainer/ExitButton").Pressed += OnExitPressed;
	}

	private void OnStartPressed()
	{
		// Load Recipe Selection menu directly
		GetTree().ChangeSceneToFile("res://Scenes/Menus/RecipeSelection.tscn");
	}

	private void OnSettingsPressed()
	{
		// Load Settings menu directly
		GetTree().ChangeSceneToFile("res://Scenes/Menus/Settings.tscn");
	}

	private void OnExitPressed()
	{
		GetTree().Quit();
	}
}
