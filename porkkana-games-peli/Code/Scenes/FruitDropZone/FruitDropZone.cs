using Godot;
using System;

public partial class FruitDropZone : Area2D
{
	public enum FruitGroup
	{
		Apple,
		Banana,
		Kiwi,
		Grapes,
		Lime,
	}
	[Export] private FruitGroup _fruit;
	private Sprite2D _plateSprite;
	private Texture2D _plateApple;
	private Texture2D _plateBanana;
	private Texture2D _plateKiwi;
	private Texture2D _plateGrape;


	public override void _Ready()
	{
		_plateSprite = GetNode<Sprite2D>("Sprite2D");
		// TODO: SWITCH CORRECT TEXTURE PATHS
		_plateApple = GD.Load<Texture2D>("res://Art/Assets/Ingridients/Pasta/complete-dish-v1.png");
		_plateBanana = GD.Load<Texture2D>("res://Art/Assets/Ingridients/Pasta/complete-dish-v1.png");
		_plateKiwi = GD.Load<Texture2D>("res://Art/Assets/Ingridients/Pasta/complete-dish-v1.png");
		_plateGrape = GD.Load<Texture2D>("res://Art/Assets/Ingridients/Pasta/complete-dish-v1.png");
		BodyEntered += OnBodyEntered;
	}

	private void OnBodyEntered(Node2D body)
	{

		if (body is FruitLevelTwoIngredient fruit)
		{
			// If fruit is in same group that in editor set _fruit is
			// AND fruits ingredientState is Chopped
			if (fruit.IsInGroup(_fruit.ToString()) && fruit.State == FruitLevelTwoIngredient.IngredientState.Chopped)
			{
				fruit.QueueFree();
				GD.Print($"Fruit: {_fruit} entered!");
				ChangeSprite();
				GameManager.Instance.AddScore();
			}
		}
	}

	public void ChangeSprite()
	{
		if (_fruit.ToString().Equals("Apple"))
		{
			_plateSprite.Texture = _plateApple;
		}

		if (_fruit.ToString().Equals("Banana"))
		{
			_plateSprite.Texture = _plateBanana;
		}

		if (_fruit.ToString().Equals("Kiwi"))
		{
			_plateSprite.Texture = _plateKiwi;
		}

		if (_fruit.ToString().Equals("Grapes"))
		{
			_plateSprite.Texture = _plateGrape;
		}
	}
}
