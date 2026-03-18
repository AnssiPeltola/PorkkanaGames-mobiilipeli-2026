using Godot;

public partial class Settings : Control
{
	private HSlider _volumeSlider;
	private CheckBox _accessibilityToggle;
	private Button _backButton;

	public override void _Ready()
	{
		// Cache nodes
		_volumeSlider = GetNode<HSlider>("CanvasLayer/VBoxContainer/VolumeSlider");
		_accessibilityToggle = GetNode<CheckBox>("CanvasLayer/VBoxContainer/AccessabilityToggle");
		_backButton = GetNode<Button>("CanvasLayer/VBoxContainer/BackButton");

		// Connect signals
		_volumeSlider.ValueChanged += OnVolumeChanged;
		_accessibilityToggle.Toggled += OnAccessibilityToggled;
		_backButton.Pressed += OnBackPressed;
	}
	private void _on_volume_slider_value_changed(double value)
{
	float normalized = (float)(value / 100.0);
	GD.Print($"Volume changed: {normalized}");

	// If you have a GameManager:
	// GameManager.Instance.SetVolume(normalized);
}
private void _on_back_button_pressed()
{
	// If you have a SceneManager singleton:
	// SceneManager.LoadScene("res://Scenes/Menus/MainMenu.tscn");

	// Or use Godot's built-in scene change:
	GetTree().ChangeSceneToFile("res://Scenes/Menus/MainMenu.tscn");
}


	private void OnVolumeChanged(double value)
	{
		// Example: send volume to your GameManager
		// Convert 0–100 to 0–1
		float normalized = (float)(value / 100.0);

		// If you have a GameManager singleton:
		// GameManager.Instance.SetVolume(normalized);

		GD.Print($"Volume changed: {normalized}");
	}

	private void OnAccessibilityToggled(bool enabled)
	{
		GD.Print($"Accessibility mode: {enabled}");
		// Add your accessibility logic here
	}

	private void OnBackPressed()
	{
		GetTree().ChangeSceneToFile("res://Scenes/Menus/MainMenu.tscn");
	}
}
