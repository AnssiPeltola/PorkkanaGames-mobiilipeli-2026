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
		SceneManager.Instance.LoadScene("res://Scenes/Menus/RecipeSelection.tscn");
	}

	private void OnSettingsPressed()
	{
		SceneManager.Instance.LoadScene("res://Scenes/Menus/Settings.tscn");
	}

	private void OnExitPressed()
	{
		GetTree().Quit();
	}
}
