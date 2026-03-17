using System.Collections.Generic;
using Godot;

// https://forum.godotengine.org/t/implementing-conveyor-belt-in-2d/121821
// https://docs.godotengine.org/en/4.3/classes/class_vector2.html?utm_source=chatgpt.com#constants
// https://kidscancode.org/godot_recipes/3.x/physics/conveyor_belt/index.html
// https://catlikecoding.com/godot/true-top-down-2d/13-conveyors/

public partial class Conveyor : StaticBody2D
{
	// Conveyor moving speed
	[Export] private float speed = 700f;

	// Boolean value for either direction is Right or Left. True is from Left to Right
	[Export] private bool leftToRight = true;

	private Area2D detectionZone;
	// New List for Ingredient Object
	private readonly List<Node2D> bodiesOnBelt = new();

	public override void _Ready()
	{
		// Check if Area2D "DetectionZone" is found or not
		detectionZone = GetNode<Area2D>("DetectionZone");
		if (detectionZone == null)
		{
			return;
		}

		detectionZone.BodyEntered += OnBodyEntered;
		detectionZone.BodyExited += OnBodyExited;
	}

	public override void _PhysicsProcess(double delta)
	{
		// If leftToRight is true, set dir to Vector2.Right otherwise Vector2.Left
		Vector2 dir = leftToRight ? Vector2.Right : Vector2.Left;

		// Loops each item in bodiesOnBelt list and moves item GlobalPosition to direction that were just set with speed that is given
		// Works but is it the best practice?
		foreach (var item in bodiesOnBelt)
		{
			item.GlobalPosition += dir * speed * (float)delta;
		}
	}

	// When conveyor detects Ingredient it will add the Ingredient into bodiesOnBelt list and items on the list are moved on the conveyor
	private void OnBodyEntered(Node2D body)
	{
		if (body is Node2D item)
			bodiesOnBelt.Add(item);
	}

	// When Ingredient exits Conveyor the Ingredient is removed from the bodiesOnBelt list
	private void OnBodyExited(Node2D body)
	{
		if (body is Node2D item)
			bodiesOnBelt.Remove(item);
	}
}