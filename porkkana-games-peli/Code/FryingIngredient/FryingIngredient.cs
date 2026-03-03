using Godot;
using System;

public partial class FryingIngredient : CharacterBody2D
{
	// https://www.w3schools.com/cs/cs_enums.php - An enum is a special "class" that represents a group of constants.
	public enum IngredientState
	{
		Raw,
		Chopped,
		Cooked
	}

	private bool _dragging = false;
	[Export] private int _clickRadius = 32;
	[Export] private float CookTime = 5f;

	// Init IngredientState. For testing its now Chopped
	public IngredientState State = IngredientState.Chopped;
	private Timer _cookTimer;
	private ProgressBar _progressBar;

	// When public we can set this as true or false in other code where this object is used
	public bool IsInDropZone { get; set; } = false;
	public bool IsInFryingPan { get; set; } = false;
	public bool OpenMiniGame { get; set; } = false;
	private Sprite2D _sprite;
	private Texture2D _sauceTexture;

    public override void _Ready()
    {
		// Load Nodes and textures
        _sprite = GetNode<Sprite2D>("Sprite2D");
		_cookTimer = GetNode<Timer>("Timer");
        _progressBar = GetNode<ProgressBar>("ProgressBar");
		_sauceTexture = GD.Load<Texture2D>("res://Art/Examples/tomatosauce.png");

		// Init _progressBar
        _progressBar.Visible = false;
        _progressBar.Value = 0;
        _progressBar.MaxValue = CookTime;

		// Init _cookTimer
        _cookTimer.WaitTime = CookTime;
        _cookTimer.OneShot = true;
		// When Ingredient is cooked trigger function OnCookFinished(). Timer's Timeout has signal into our function
        _cookTimer.Timeout += OnCookFinished;
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
				_sprite.Hide();
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

	// Refresh on every frame cooking progressBar when timer is not stopped
	public override void _Process(double delta)
    {
        if (!_cookTimer.IsStopped())
        {
            float elapsed = CookTime - (float)_cookTimer.TimeLeft;
            _progressBar.Value = elapsed;
        }
    }

	// Start cooking Ingredient that is in State Chopped, if not return.
	public void StartCooking()
    {
        if (State != IngredientState.Chopped)
		{
            return;
		}

		GD.Print("Start Cooking!");
        _progressBar.Visible = true;
        _progressBar.Value = 0;
        _cookTimer.Start();
    }

	// Stop cooking and hide progressBar from shown.
	public void StopCooking()
	{

		if (State != IngredientState.Chopped)
		{
			return;
		}

		if (!IsInFryingPan)
		{
			GD.Print("Stop Cooking!");
			_progressBar.Visible = false;
			_progressBar.Value = 0;

			_cookTimer.Stop();
		}
	}

	// When cooking is done set State to Cooked
	private void OnCookFinished()
    {
        State = IngredientState.Cooked;
		RemoveFromGroup("Chopped");
		AddToGroup("Cooked");
        _progressBar.Visible = false;

        GD.Print($"{Name} cooked!");
		ChangeSprite(_sauceTexture);
		// ChangeCollisionShape2D here too?
    }

	// Use after minigame is completed?
    public void Chop()
    {
        State = IngredientState.Chopped;

        RemoveFromGroup("Raw");
        AddToGroup("Chopped");
    }

	// Function that changes this scenes Sprite2D texture to new
	public void ChangeSprite(Texture2D newTexture)
	{
		_sprite.Texture = newTexture;
	}
}