using Godot;

public partial class ShowGuidebookButton : Button
{
    private Control _guidebook;

    public override void _Ready()
    {
        // Load scenes into variables on load
        var scene = GetTree().CurrentScene;
        _guidebook = scene.GetNode<Control>("Guidebook");

        // Active Pressed signal
        Pressed += OnPressed;
    }

    // When pressing showButton it will make guidebook visible and showButton invisible
    private void OnPressed()
    {
        _guidebook.Visible = true;
        Visible = false;
    }
}