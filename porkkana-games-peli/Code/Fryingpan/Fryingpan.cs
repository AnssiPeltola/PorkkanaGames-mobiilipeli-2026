using Godot;
using System;

public partial class Fryingpan : Area2D
{
    public override void _Ready()
    {
        BodyEntered += OnBodyEntered;
        BodyExited += OnBodyExited;
    }

    private void OnBodyEntered(Node2D body)
    {
        if (body is FryingIngredient ingredient)
        {
			ingredient.IsInFryingPan = true;

            if (ingredient.State == FryingIngredient.IngredientState.Chopped)
            {
				GD.Print("Chopped Ingredient entered!");
                ingredient.StartCooking();
            }
        }
    }

    private void OnBodyExited(Node2D body)
    {
        if (body is FryingIngredient ingredient)
        {
			ingredient.IsInFryingPan = false;
			ingredient.StopCooking();
        }
    }
}

