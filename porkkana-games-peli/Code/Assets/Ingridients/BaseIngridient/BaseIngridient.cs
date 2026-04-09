using Godot;
using System;
using System.Collections.Generic;

/*
 * Purpose:
 *      Let all ingridients inherit from this BaseIngridient
 */

public partial class BaseIngridient : CharacterBody2D
{
	private bool _dragging = false;
	// Boolean values for prevent that only 1 minigame of this type can be opened at same time
	protected static bool IsAnyCuttingMiniGameOpen = false;
	protected static bool IsAnyPeelingMiniGameOpen = false;
	// Index of the finger currently controlling this ingredient (-1 means none).
	private int _activeTouchId = -1;
	// Latest world position of the controlling finger.
	private Vector2 _activeTouchPosition = Vector2.Zero;
	// One finger can claim only one ingredient, but many fingers can drag at once. (HashSet is like an list but does not allow duplicates)
	private static HashSet<int> _claimedTouchIds = new HashSet<int>();
	// Dictionary that stores key value pairs (int and Vector2)
	private Dictionary<int, Vector2> ingredientTouchDictionary = new Dictionary<int, Vector2>();
	// protected for class inheritance (class inheriting CAN access this but nobody else)
	protected Sprite2D _sprite;
	// Touch detection area
	private Area2D _touchArea;

	public override void _Ready()
	{
		// Get TouchArea
		_touchArea = GetNode<Area2D>("TouchArea");
		_touchArea.InputEvent += OnTouchInput;
	}

	// Called automatically when this node leaves the SceneTree
	// (for example during level changes or when the node is deleted).
	// We clear drag state here so claimed touches never stay locked
	// between scenes and blocks dragging in the next level.
	public override void _ExitTree()
	{
		if (_touchArea != null)
		{
			_touchArea.InputEvent -= OnTouchInput;
		}

		if (_activeTouchId != -1)
		{
			_claimedTouchIds.Remove(_activeTouchId);
		}

		_activeTouchId = -1;
		_dragging = false;
		Velocity = Vector2.Zero;
	}

	// Called when this ingredient's TouchArea receives an input event on one of its enabled touch shapes. shapeIdx = index of the touched collision shape inside TouchArea.
	private void OnTouchInput(Node viewport, InputEvent e, long shapeIdx)
	{
		// TouchArea is only used to start dragging this ingredient.
		// InputEvenScreenTouch counts touch index in the case of a multi-touch event. One index = one finger.
		if (e is InputEventScreenTouch touch)
		{
			// When touch is down
			if (touch.Pressed)
			{
				// if _activeTouchId is not -1 (not in use) return.
				if (_activeTouchId != -1)
				{
					return;
				}

				// If this finger already claimed another ingredient, return.
				if (_claimedTouchIds.Contains(touch.Index))
				{
					return;
				}
				// Claim this touch only if we are not already claimed
				if (_activeTouchId == -1)
				{
					_activeTouchId = touch.Index;
					_claimedTouchIds.Add(touch.Index);
					_activeTouchPosition = touch.Position;
					_dragging = true;
					// Todo: Scale thing here?
				}
			}
		}
	}

	// Handles screen-wide drag and release events after this ingredient has claimed a touch.
	// Start-touch is handled in OnTouchInput via TouchArea; here we keep tracking movement
	// and release for the claimed finger even when the finger moves outside the TouchArea.
	public override void _UnhandledInput(InputEvent e)
	{
		// Drag event fires while any finger moves on screen.
		if (e is InputEventScreenDrag drag)
		{
			// Keep latest position for this finger index (useful for multi-touch tracking/debug).
			ingredientTouchDictionary[drag.Index] = drag.Position;

			// Move this ingredient only when the moving finger is the owner of this ingredient.
			if (drag.Index == _activeTouchId)
			{
				// Physics step reads this target position and moves toward it.
				_activeTouchPosition = drag.Position;
			}
			return;
		}

		if (e is InputEventScreenTouch touch && !touch.Pressed)
		{
			ingredientTouchDictionary.Remove(touch.Index);

			// Stop dragging only if the released touch is the one we claimed.
			if (touch.Index == _activeTouchId)
			{
				_claimedTouchIds.Remove(_activeTouchId);
				_activeTouchId = -1;
				_dragging = false;
				Velocity = Vector2.Zero;
				// Todo: Scale thing here?
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

		// Follow the currently claimed touch/finger.
		Vector2 target = _activeTouchPosition;
		Vector2 direction = target - GlobalPosition;

		Velocity = direction / (float)delta;
		// Makes the move using Godot physics
		MoveAndSlide();
	}

	// Function that changes this scenes Sprite2D texture to new
	public void ChangeSprite(Texture2D newTexture)
	{
		_sprite.Texture = newTexture;
	}
}
