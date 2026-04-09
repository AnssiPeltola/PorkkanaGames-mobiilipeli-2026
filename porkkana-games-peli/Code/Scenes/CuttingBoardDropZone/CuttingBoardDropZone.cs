using Godot;
using System;

public partial class CuttingBoardDropZone : Area2D
{
	// Makes the connection signal for methods OnBodyEntered and OnBodyExited
    public override void _Ready()
    {
        BodyEntered += OnBodyEntered;
        BodyExited += OnBodyExited;

    }

	// When Object Ingredient enters this DropZone it will either turn Ingredients Sprite2D into green (when good) or red (when bad) and print info into console.
    private void OnBodyEntered(Node2D body)
    {
        if (body is LevelTwoIngredient box && !box.IsInGroup("Pasta"))
        {
            GD.Print("Ingredient entered!" + box);
			box.IsInDropZone = true;
			GD.Print(box.IsInDropZone);
        }

        if (body is FruitLevelTwoIngredient ingredient)
        {
            GD.Print("Ingredient entered!" + ingredient);
			ingredient.IsInDropZone = true;
			GD.Print(ingredient.IsInDropZone);
        }
    }

	// When Object Ingredient exits this DropZone it will turn the Ingredient Sprite2D into the normal blue icon and tell info into console.
    private void OnBodyExited(Node2D body)
    {
        if (body is LevelTwoIngredient box)
		{
            GD.Print("Ingredient Exited!" + box);
			box.IsInDropZone = false;
			GD.Print(box.IsInDropZone);
		}

        if (body is FruitLevelTwoIngredient ingredient)
		{
            GD.Print(ingredient);
			ingredient.IsInDropZone = false;
			GD.Print(ingredient.IsInDropZone);
		}
    }
}

