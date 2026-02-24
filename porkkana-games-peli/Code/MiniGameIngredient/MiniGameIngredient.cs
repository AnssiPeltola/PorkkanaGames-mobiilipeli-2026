using Godot;
using System;

// https://www.youtube.com/watch?v=K_mZeYLYpgg - SubViewport tutorial - Make minigame scene with subViewport?

public partial class MiniGameIngredient : CharacterBody2D
{
	private bool _dragging = false;
	[Export] private int _clickRadius = 32;

	// When public we can set this as true or false in other code where this object is used
	public bool IsInDropZone { get; set; } = false;
	public bool OpenMiniGame { get; set; } = false;
	private Sprite2D _sprite;

    public override void _Ready()
    {
		// We get this scenes Sprite2D node in variable _sprite
        _sprite = GetNode<Sprite2D>("Sprite2D");
    }

	// This function is called for every input event (mouse, keyboard, touch, etc.)
   public override void _Input(InputEvent e)
    {
		// Only react to screen touch events (mobile / mouse click)
        if (e is InputEventScreenTouch touch)
        {
			// Check if the touch is close enough to this object to start dragging and set _dragging true
			if ((touch.Position - GlobalPosition).Length() < _clickRadius)
			{
            	_dragging = touch.Pressed;
			}
        }

		// touchtap.Pressed prevents releasing touch to register as click.
		if (e is InputEventScreenTouch touchtap && touchtap.Pressed)
		{
			if ((touchtap.Position - GlobalPosition).Length() < _clickRadius && IsInDropZone)
			{
				OpenMiniGame = true;
				GD.Print("Open minigame!");
			}
		}
    }

	// if dragging false does nothing
	public override void _PhysicsProcess(double delta)
    {
        if (!_dragging)
		{
			return;
		}

		// Get the current position of finger/mouse
        Vector2 target = GetGlobalMousePosition();
        Vector2 direction = target - GlobalPosition;

        Velocity = direction / (float)delta;
		// Makes the move using Godot physics
        MoveAndSlide();
    }
}
