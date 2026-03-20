using Godot;
using System;

public partial class Plate : Area2D
{
	private Area2D _pastaDetectionZone;
	private Area2D _sauceDetectionZone;
	private Sprite2D _lockedPasta;
	private Sprite2D _lockedSauce;
	private Sprite2D _placeholderPasta;
	private Sprite2D _placeholderSauce;
	private bool pastaPlaced = false;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_pastaDetectionZone = GetNode<Area2D>("PastaZone");
		_sauceDetectionZone = GetNode<Area2D>("SauceZone");

		_lockedPasta = GetNode<Sprite2D>("PastaZone/ReadyPastaSprite2D");
		_lockedSauce = GetNode<Sprite2D>("SauceZone/ReadySauceSprite2D");
		_placeholderPasta = GetNode<Sprite2D>("PastaZone/PastaSprite2D");
		_placeholderSauce = GetNode<Sprite2D>("SauceZone/SauceSprite2D");

		_pastaDetectionZone.BodyEntered += OnBodyEnteredPastaZone;
		_sauceDetectionZone.BodyEntered += OnBodyEnteredSauceZone;
	}

	private void OnBodyEnteredPastaZone(Node2D body)
	{
		if (body is CookedIngredient ingredient)
		{
			if (ingredient.IngredientKind == CookedIngredient.CookedIngredientKind.Pasta)
			{
				ingredient.QueueFree();
				GD.Print("Pasta here!");
				_placeholderPasta.Visible = false;
				_lockedPasta.Visible = true;
				pastaPlaced = true;
				// Add +1 Score
			}
		}
	}

	private void OnBodyEnteredSauceZone(Node2D body)
	{
		if (body is CookedIngredient ingredient)
		{
			if (ingredient.IngredientKind == CookedIngredient.CookedIngredientKind.Sauce && pastaPlaced)
			{
				ingredient.QueueFree();
				GD.Print("Sauce here");
				_placeholderSauce.Visible = false;
				_lockedSauce.Visible = true;
				// Add +1 Score
			}
		}
	}
}