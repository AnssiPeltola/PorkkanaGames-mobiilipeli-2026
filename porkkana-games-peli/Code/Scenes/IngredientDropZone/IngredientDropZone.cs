using Godot;
using System;

public partial class IngredientDropZone : Area2D
{
	private static readonly Color CorrectColor = new Color(0.4f, 1.0f, 0.4f, 1.0f);
	private static readonly Color WrongColor   = new Color(1.0f, 0.4f, 0.4f, 1.0f);
	private static readonly Color NormalColor  = Colors.White;

	// Makes the connection signal for methods OnBodyEntered and OnBodyExited
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExited;
	}

	// When Object Ingredient enters this DropZone it will either turn Ingredients Sprite2D into green (when good) or red (when bad) and print info into console.
	private void OnBodyEntered(Node2D body)
	{
		if (body is LevelOneIngredient box)
		{
			if (box.IsInGroup("Good"))
			{
				GD.Print("Good Ingredient entered!");
				box.Modulate = CorrectColor;

				// Add +1 Score here when "Good" Ingredient hits IngredientDropZone
				// Add +1 Score
				// Set +1 point on LevelOneScore
				//GameManager.Instance.GoodItemEntered();
				GameManager.Instance.GoodItemEntered();
			}

			if (box.IsInGroup("Bad"))
			{
				GD.Print("Bad Ingredient entered!");
				box.Modulate = WrongColor;
			}
		}
	}

	// When Object Ingredient exits this DropZone it will turn the Ingredient Sprite2D into the normal blue icon and tell info into console.
	private void OnBodyExited(Node2D body)
	{
		if (body is LevelOneIngredient box)
		{
			if (box.IsInGroup("Good"))
			{
				GD.Print("Good Ingredient Exited!");
				box.Modulate = NormalColor;
				// Take point off from Score when "Good" Ingredient leaves IngredientDropZone
				// Minus -1 Score
				//GameManager.Instance.GoodItemExited();
				GameManager.Instance.GoodItemExited();
			}

			if (box.IsInGroup("Bad"))
			{
				GD.Print("Bad Ingredient Exited!");
				box.Modulate = NormalColor;
			}
		}
	}
}
