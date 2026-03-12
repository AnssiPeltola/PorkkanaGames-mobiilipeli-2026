using Godot;
using System;
using System.Data;

public partial class Fryingpan : Area2D
{
    private Timer _cookTimer;
	private ProgressBar _progressBar;
    private Sprite2D _sprite;
    private Texture2D _onionFryingpan;
    private Texture2D _carrotFryingpan;
    private Texture2D _tomatoFryingpan;
    private FryingIngredient _currentIngredient;
    [Export] private float CookTime = 3f;
    private int state = 0;

    public override void _Ready()
    {
        _sprite = GetNode<Sprite2D>("Sprite2D");

        // Load textures for different state of frying pan
        _onionFryingpan = GD.Load<Texture2D>("res://Art/Assets/Equipment/Frying Pan/fryingpan2.png");
        _carrotFryingpan = GD.Load<Texture2D>("res://Art/Assets/Equipment/Frying Pan/fryingpan3.png");
        _tomatoFryingpan = GD.Load<Texture2D>("res://Art/Assets/Equipment/Frying Pan/fryingpan4.png");

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
        BodyExited += OnBodyExited;
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
        if (body is FryingIngredient ingredient &&
            ingredient.State == FryingIngredient.IngredientState.Chopped && ingredient.IsInGroup("Onion") && state == 0)
        {
            GD.Print("Chopped Onion entered pan");
            _currentIngredient = ingredient;
            StartCooking();
        }

        // Check if ingredient is Carrot and state is 1
        if (body is FryingIngredient ingredient2 &&
            ingredient2.State == FryingIngredient.IngredientState.Chopped && ingredient2.IsInGroup("Carrot") && state == 1)
        {
            GD.Print("Chopped Carrot entered pan");
            _currentIngredient = ingredient2;
            StartCooking();
        }

        // Checks if ingredient is Tomato and state is 2
        if (body is FryingIngredient ingredient3 &&
            ingredient3.State == FryingIngredient.IngredientState.Chopped && ingredient3.IsInGroup("Tomato") && state == 2)
        {
            GD.Print("Chopped Tomato entered pan");
            _currentIngredient = ingredient3;
            StartCooking();
        }
    }

    // When body leaves fryingpan StopCooking
    private void OnBodyExited(Node2D body)
    {
        if (body == _currentIngredient)
        {
            StopCooking();
            _currentIngredient = null;
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

    // Makes progressBar invisible and stops clock
    private void StopCooking()
    {
        GD.Print("Stop cooking");

        _cookTimer.Stop();
        _progressBar.Visible = false;
        _progressBar.Value = 0;
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
        GD.Print("Ingredient cooked!");
        _currentIngredient.QueueFree();

        if (state == 0)
        {
        ChangeSprite(_onionFryingpan);
        }

        if (state == 1)
        {
            ChangeSprite(_carrotFryingpan);
        }

        if (state == 2)
        {
            ChangeSprite(_tomatoFryingpan);
        }

        state++;
    }

    // Function that changes this scenes Sprite2D texture to new
	public void ChangeSprite(Texture2D newTexture)
	{
		_sprite.Texture = newTexture;
	}
}

