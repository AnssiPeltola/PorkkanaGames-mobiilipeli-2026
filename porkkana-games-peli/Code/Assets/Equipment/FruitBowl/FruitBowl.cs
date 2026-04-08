using Godot;
using System;

public partial class FruitBowl : Area2D
{
	private Sprite2D _bowlSprite;
	private Area2D _fruitBowlDetectionZone;
	private Texture2D _bowlStateOne;
	private Texture2D _bowlStateTwo;
	private Texture2D _bowlStateThree;
	private Texture2D _bowlStateFour;
	private Texture2D _bowlStateFive;
	private int state = 0;

	public override void _Ready()
	{
		_bowlSprite = GetNode<Sprite2D>("Sprite2D");
		_fruitBowlDetectionZone = GetNode<Area2D>("DropZone");
		_bowlStateOne = GD.Load<Texture2D>("res://Art/Assets/Equipment/FruitBowl/fruit-bowl-one-v1.png");
		_bowlStateTwo = GD.Load<Texture2D>("res://Art/Assets/Equipment/FruitBowl/fruit-bowl-two-v1.png");
		_bowlStateThree = GD.Load<Texture2D>("res://Art/Assets/Equipment/FruitBowl/fruit-bowl-three-v1.png");
		_bowlStateFour = GD.Load<Texture2D>("res://Art/Assets/Equipment/FruitBowl/fruit-bowl-four-v1.png");
		_bowlStateFive = GD.Load<Texture2D>("res://Art/Assets/Equipment/FruitBowl/fruit-bowl-complete-v1.png");

		_fruitBowlDetectionZone.BodyEntered += OnBodyEntered;
	}

	private void OnBodyEntered(Node2D body)
	{
		if (body is ChoppedFruit fruit)
		{
			bool isLime = fruit.IsInGroup("Lime");

			// Switch case based on state. Each state does same but bowl texture switch differet on every state.
			// Allows to add any fruit but forces lime to be added last
			switch (state)
			{
				case 0:
					if (!isLime)
					{
						fruit.QueueFree();
						ChangeBowlSprite(_bowlStateOne);
						state = 1;
						GameManager.Instance.AddScore();
					}
					break;
				case 1:
					if (!isLime)
					{
						fruit.QueueFree();
						ChangeBowlSprite(_bowlStateTwo);
						state = 2;
						GameManager.Instance.AddScore();
					}
					break;
				case 2:
					if (!isLime)
					{
						fruit.QueueFree();
						ChangeBowlSprite(_bowlStateThree);
						state = 3;
						GameManager.Instance.AddScore();
					}
					break;
				case 3:
					if (!isLime)
					{
						fruit.QueueFree();
						ChangeBowlSprite(_bowlStateFour);
						state = 4;
						GameManager.Instance.AddScore();
					}
					break;
				case 4:
					if (isLime)
					{
						fruit.QueueFree();
						ChangeBowlSprite(_bowlStateFive);
						state = 0;
						GameManager.Instance.AddScore();
					}
					break;
			}
		}
	}

	public void ChangeBowlSprite(Texture2D newTexture)
	{
		_bowlSprite.Texture = newTexture;
	}
}
