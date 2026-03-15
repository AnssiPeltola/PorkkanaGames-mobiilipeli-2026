using Godot;
using System;

public partial class CuttingMiniGame : Node2D
{
	public Texture2D IngridientTexture { get; set; }
	
	// Declaration for the picture frame
	private Sprite2D _ingridientSprite;

	// Signals:
	// https://docs.godotengine.org/en/stable/getting_started/step_by_step/signals.html 
	// -> Custom signals
	// <CustomSignalName>EventHandler();
	[Signal] public delegate void CuttingCompleteEventHandler();

	[Export] public int RequiredCuts = 3;
	private int _cutsDone = 0;

	// Swipe tracking
	private bool _isSwiping = false;
	// Stored first touch position
	// Vector2.Zero = (0,0) vector
	private Vector2 _swipeStart = Vector2.Zero;
	// Stored last touch position
	private Vector2 _swipeEnd = Vector2.Zero;


	// TEMP:
	// Placeholder for testing untill the Cut logic is in
	/*
		TODO:
		Cut logic using vectors. 
		Get positions from Start cut / End cut
		Record the vector and check validations does it count
		
		TODO: NOTE
		Cut game will now display different sprite2d's
		Consider, they might be different sizes.
	*/
	public override void _Input(InputEvent e)
	{
		if (e is InputEventScreenTouch touch && touch.Pressed)
		{
			RegisterCut();
		}
		
		/*
		TODO:
		Actual swipe vectors	
		
		Something along the lines of:
		_swipeStart = GetTouchPosition();
		RegisterCut(_swipeStart, _swipeEnd);
		*/
	}

	public override void _Ready()
	{
		// Set the texture for the ingridient to be cutted to IngridientSprite
		_ingridientSprite = GetNode<Sprite2D>("CanvasLayer/ColorRect/IngridientSprite");
		//_ingridientSprite.Texture = IngridientTexture;
	}

	public override void _Process(double delta)
	{
	}

	private void RegisterCut()
	{
		_cutsDone++;

		if (_cutsDone >= RequiredCuts)
			// Run the FinishMinigame() to signal MiniGameIngridient
			// the CuttingMiniGame should be closed.
			FinishMinigame();
	}
	
	private void FinishMinigame()
	{
		EmitSignal(SignalName.CuttingComplete);
	}

}
