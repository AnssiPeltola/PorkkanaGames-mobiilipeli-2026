using Godot;
using System;

public partial class IngredientDropZone : Area2D
{
	private Texture2D _greenTexture;
	private Texture2D _redTexture;
	private Texture2D _returnTexture;

	// Makes the connection signal for methods OnBodyEntered and OnBodyExited
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
		BodyExited += OnBodyExited;

		// Loads the path of new texture into variables
		_greenTexture = GD.Load<Texture2D>("res://Art/icon-green.svg");
		_redTexture = GD.Load<Texture2D>("res://Art/icon-red.svg");
		_returnTexture = GD.Load<Texture2D>("res://Art/icon.svg");
	}

	// When Object Ingredient enters this DropZone it will either turn Ingredients Sprite2D into green (when good) or red (when bad) and print info into console.
	private void OnBodyEntered(Node2D body)
	{
		if (body is Ingredient box)
		{
			if (box.IsInGroup("Good"))
			{
				GD.Print("Good Ingredient entered!");
				box.ChangeSprite(_greenTexture);
				// Set +1 point on LevelOneScore
				//GameManager.Instance.GoodItemEntered();
			}

			if (box.IsInGroup("Bad"))
			{
				GD.Print("Bad Ingredient entered!");
				box.ChangeSprite(_redTexture);
			}
		}
	}

	// When Object Ingredient exits this DropZone it will turn the Ingredient Sprite2D into the normal blue icon and tell info into console.
	private void OnBodyExited(Node2D body)
	{
		if (body is Ingredient box)
		{
			if (box.IsInGroup("Good"))
			{
				GD.Print("Good Ingredient Exited!");
				box.ChangeSprite(_returnTexture);
				// Take point off from LevelOneScore
				//GameManager.Instance.GoodItemExited();
			}

			if (box.IsInGroup("Bad"))
			{
				GD.Print("Bad Ingredient Exited!");
				box.ChangeSprite(_returnTexture);
			}
		}
	}
}
