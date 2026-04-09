using Godot;
using System;


public partial class PeelingMiniGame : Node2D
{
	// Texture is what we display
	// Sprite is the node displaying it
	public Texture2D IngridientTexture { get; set; }



	// Receives texture @_Ready()
	private Sprite2D _ingridientSprite;

	// Label reference point
	private Label _cutsLabel;

	// Reference point
	private CutArea _cutArea;

	// Signals:
	// https://docs.godotengine.org/en/stable/getting_started/step_by_step/signals.html
	// -> Custom signals
	// <CustomSignalName>EventHandler();
	[Signal] public delegate void PeelingCompleteEventHandler();

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

		NOTE:
		Cut game will now display different sprite2d's
		Consider, they might be different sizes.

		/*
		TODO:
		Actual swipe vectors

		Something along the lines of:
		_swipeStart = GetTouchPosition();
		_registerCut(_swipeStart, _swipeEnd);
		*/

	public override void _Ready()
	{
		// Find the IngridientSprite node from the tree at the address
		_ingridientSprite = GetNode<Sprite2D>("CanvasLayer/IngridientSprite");
		// Set our chosen IngridientTexture to our _ingridientSprite
		_ingridientSprite.Texture = IngridientTexture;

		// give the label node path for _cutsLabel
		_cutsLabel = GetNode<Label>("CanvasLayer/Label");
        _updateLabel(0);

        // set the reference to CutArea
        _cutArea = GetNode<CutArea>("CutArea");
        // And Subscribe to the signal
        // and run _registerCut() when signal is received
        _cutArea.CutRegistered += _registerCut;
	}

	private void _updateLabel(int _cutsDone)
	{
		_cutsLabel.Text = ($"Peels: {_cutsDone} / 3");
	}

	private void _registerCut()
	{
        GD.Print("_registerCut() fired");
		_cutsDone++;
		_updateLabel(_cutsDone);

		if (_cutsDone >= RequiredCuts)
			// Run the _finishMinigame() to signal MiniGameIngridient
			// the CuttingMiniGame should be closed.
			_finishMinigame();
	}

	private void _finishMinigame()
	{
		EmitSignal(SignalName.PeelingComplete);
	}
}
