using Godot;
using System;

public partial class IngredientDropZone : Area2D
{
	// private static readonly Color CorrectColor = new Color(0.4f, 1.0f, 0.4f, 1.0f);
	// private static readonly Color WrongColor = new Color(1.0f, 0.4f, 0.4f, 1.0f);
	// private static readonly Color NormalColor = Colors.White;
	private Checklist _checklist;

	public override void _Ready()
	{
		// Get the Checklist node from the current scene root.
		_checklist = GetTree().CurrentScene.GetNodeOrNull<Checklist>("Checklist");

		// Makes the connection signal for methods OnBodyEntered and OnBodyExited
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
				// Set Checkmark in Scene Checklist visible for right ingredient
				SetChecklistFromIngredient(box, true);
				// box.Modulate = CorrectColor;

				// Add +1 Score here when "Good" Ingredient hits IngredientDropZone
				GameManager.Instance.AddScore();
			}

			if (box.IsInGroup("Bad"))
			{
				GD.Print("Bad Ingredient entered!");
				// box.Modulate = WrongColor;
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
				// Set Checkmark in Scene Checklist not visible
				SetChecklistFromIngredient(box, false);
				// box.Modulate = NormalColor;

				// Take point off from Score when "Good" Ingredient leaves IngredientDropZone
				GameManager.Instance.MinusScore();
			}

			if (box.IsInGroup("Bad"))
			{
				GD.Print("Bad Ingredient Exited!");
				// box.Modulate = NormalColor;
			}
		}
	}

	// Helps us make right parameters for function SetIngredientCheck(string, bool)
	private void SetChecklistFromIngredient(LevelOneIngredient ingredient, bool isVisible)
	{
		string ingredientGroup = GetIngredientGroupName(ingredient);
		_checklist.SetIngredientCheck(ingredientGroup, isVisible);
	}

	// Returns an string based on LevelOneIngredients group
	// This is used for SetChecklistFromIngredient
	private static string GetIngredientGroupName(LevelOneIngredient ingredient)
	{
		if (ingredient.IsInGroup("Tomato"))
		{
			return "Tomato";
		}

		if (ingredient.IsInGroup("Carrot"))
		{
			return "Carrot";
		}

		if (ingredient.IsInGroup("Onion"))
		{
			return "Onion";
		}

		if (ingredient.IsInGroup("Pasta"))
		{
			return "Pasta";
		}

		return null;
	}
}
