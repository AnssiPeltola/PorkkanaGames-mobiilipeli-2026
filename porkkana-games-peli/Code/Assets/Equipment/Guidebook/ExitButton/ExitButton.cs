using Godot;

public partial class ExitButton : Button
{
    private Control _guidebook;
    private Button _showButton;

    public override void _Ready()
    {
		// Load scenes into variables on load
        var scene = GetTree().CurrentScene;
        _guidebook = GetTree().CurrentScene.GetNode<Control>("Guidebook");
        _showButton = scene.GetNode<Button>("ShowGuidebookButton");

		// Active Pressed signal
        Pressed += OnPressed;
    }

	// When pressing Exit button it will make guidebook invisible and showButton visible
    private void OnPressed()
    {
        _guidebook.Visible = false;
        _showButton.Visible = true;
    }
}