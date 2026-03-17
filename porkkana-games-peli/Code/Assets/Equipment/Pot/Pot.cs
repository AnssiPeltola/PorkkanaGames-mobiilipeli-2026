using Godot;
using System;

public partial class Pot : Area2D
{
    private Timer _cookTimer;
	private ProgressBar _progressBar;
    private Sprite2D _sprite;
    private Texture2D _pastaPot;
	private Texture2D _pastaCookedPot;
    private LevelTwoIngredient _currentIngredient;
    [Export] private float CookTime = 5f;

    public override void _Ready()
    {
        _sprite = GetNode<Sprite2D>("Sprite2D");

        // Load textures for different state of pot
        _pastaPot = GD.Load<Texture2D>("res://Art/Assets/Equipment/Pot/pot-uncooked-v1.png");
		_pastaCookedPot = GD.Load<Texture2D>("res://Art/Assets/Equipment/Pot/pot-cooked-v1.png");

        // Timer and progressBar
        _cookTimer = GetNode<Timer>("Timer");
        _progressBar = GetNode<ProgressBar>("ProgressBar");

        // Init _progressBar
        _progressBar.Visible = false;
        _progressBar.Value = 0;
        _progressBar.MaxValue = CookTime;

		// Init _cookTimer
        _cookTimer.WaitTime = CookTime;
        _cookTimer.OneShot = true;
		// When Ingredient is cooked trigger function OnCookFinished(). Timer's Timeout has signal into our function
        _cookTimer.Timeout += OnCookFinished;

        BodyEntered += OnBodyEntered;
        // BodyExited += OnBodyExited;
    }

    public override void _Process(double delta)
    {
        if (!_cookTimer.IsStopped())
        {
            float elapsed = CookTime - (float)_cookTimer.TimeLeft;
            _progressBar.Value = elapsed;
        }
    }

    // When FryingIngredient's body enters in fryingpan it will check the state and which group ingredient is.
    // By this we will make an order what to cook first, second and third.
    private void OnBodyEntered(Node2D body)
    {
        // Check if ingredient is Onion and state is 0
        if (body is LevelTwoIngredient ingredient &&
            ingredient.State == LevelTwoIngredient.IngredientState.Raw && ingredient.IsInGroup("Pasta"))
        {
            GD.Print("Pasta Entered into pot!");
            _currentIngredient = ingredient;
			ChangeSprite(_pastaPot);
			_currentIngredient.Hide();
            StartCooking();
        }
    }

    // Makes progressBar visible and set timer on
    private void StartCooking()
    {
        GD.Print("Start cooking");
        _progressBar.Visible = true;
        _progressBar.Value = 0;
        _cookTimer.Start();
    }

    // Function is triggered when _cookTimer.Timeout
    // Changes ingredients IngredientState as Cooked
    // ProgressBar set invisible
    // Removes ingredient and its childs from Scene with QueueFree()
    // Checks what state we are on and changes new Sprite2D for fryingpan
    // Plus +1 into state
    private void OnCookFinished()
    {
        if (_currentIngredient == null)
        {
            return;
        }

        _currentIngredient.changeStateCooked();
        _progressBar.Visible = false;
		ChangeSprite(_pastaCookedPot);
        GD.Print("Ingredient cooked!");
        _currentIngredient.QueueFree();
		// Do we need to unsubscribe from signal - BodyEntered -= OnBodyEntered; ???
    }

    // Function that changes this scenes Sprite2D texture to new
	public void ChangeSprite(Texture2D newTexture)
	{
		_sprite.Texture = newTexture;
	}
}


