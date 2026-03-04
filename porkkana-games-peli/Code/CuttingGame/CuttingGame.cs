using Godot;
using System;

public partial class CuttingGame : Node2D
{
	[export] public int RequiredCuts = 3;

	private int cutsDone = 0;

    // Swipe tracking
    // These 3 lines are needed to check if our cuts
    // cross the 2Dnode and count as cutsDone
    private bool _isSwiping = false;
    private Vector2 _swipeStart = Vector2.Zero;
    private Vector2 _swipeEnd = Vector2.Zero;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{

	}

    public override void _Input(InputEvent e)
    {
        if (e is InputEventScreenTouch touch)
        }
            // New swipe
            _swipeStart = touch.Position;
            _isSwiping = true;

            // TODO! Create preview line for feedback

        }
        else
        {
            // Swipe has ended
            // if TRUE enter.
            if (_isSwiping)
            {
                // Record current poistion as END
                _swipeEnd = touch.Position;
                // End the swipe / cut
                _isSwiping = false;

                // TODO : Run Swipe validation method here!

            }

	}

    // TODO: cut validation method to be run in _Input
    // vector start -> end
    // the Cut vector >= 2DSprite
    private bool IsValidCut(Vector2 start, Vector2 end)
    {
    }
