using Godot;
using System;

// https://www.youtube.com/watch?v=K_mZeYLYpgg - SubViewport tutorial - Make minigame scene with subViewport?

public partial class MiniGameIngredient : CharacterBody2D
{
	private bool _dragging = false;
	[Export] private int _clickRadius = 32;

	[Export] public Texture2D ChoppedSprite;
	
	
	// PackedScene = Godots own .tscn type
	// https://docs.godotengine.org/en/stable/classes/class_packedscene.html
	// We can now assign .tscn scenes to _CuttingMiniGameScene
	[Export] private PackedScene _cuttingMiniGameScene;


	// When public we can set this as true or false in other code where this object is used
	public bool IsInDropZone { get; set; } = false;
	public bool OpenMiniGame { get; set; } = false;

	private CuttingMiniGame _activeMiniGame = null;

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
				_sprite.Hide();
				StartCuttingMiniGame();
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

	private void StartCuttingMiniGame()
	{
		// https://docs.godotengine.org/en/stable/tutorials/scripting/nodes_and_scene_instances.html
		// Make new instance of the cutting minigame (object of CuttingMiniGame class)
		// call that instance _cuttingMiniGameScene
		// NOTE: _activeMiniGame = godots datatype "PackedScene" = takes (.tscn)
		_activeMiniGame = _cuttingMiniGameScene.Instantiate<CuttingMiniGame>();
		
		// Read the current texture and hand it to the new instance ("_activeMinGame")
		_activeMiniGame.IngridientTexture = _sprite.Texture;
		
		// Breakdown:
		/*
			.GetTree().Root						→ get the Root node
			.GetChildCount() - 1				→ count how many children it has, subtract 1
			.GetChild( that number )			→ get the child at that index (= current scene)
			.AddChild(_activeMiniGame)			→ attach minigame to it
		*/
		GetTree().Root.GetChild(GetTree().Root.GetChildCount() - 1).AddChild(_activeMiniGame);
		
		// Subscribe for a signal from _activeMinigame called CuttingCompplete.
		// When signal is received run OnCuttingComplete();
		_activeMiniGame.CuttingComplete += OnCuttingComplete;

		GD.Print("CuttingMiniGame started.");
	}

	private void OnCuttingComplete()
	{
		GD.Print("Ingredient chopped!");
		// Close the running minigame
		// Also unsubscribes the signals!
		_activeMiniGame.QueueFree();

		// Reset _activeMiniGame back to null
		_activeMiniGame = null;


		/* TODO: 
			Currently: Change the sprite for something else
			Consider: Swap the entire MiniGameIngridient node for another
			Ingridient node "example: IngridientToCook"
			Also prevents multiple cutting games per ingridient
		*/
		_sprite.Texture = ChoppedSprite;
		_sprite.Show();


		/*TODO:
			Inform "GameManager" that ingridient has been cut etc.
		*/
	}
}
