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
	[Export] public Texture2D PlateTexture;
	private Texture2D _plateApple;
	private Texture2D _plateBanana;
	private Texture2D _plateKiwi;
	private Texture2D _plateGrape;
	private Texture2D _plateLime;

	public override void _Ready()
	{
		_plateSprite = GetNode<Sprite2D>("Sprite2D");
		if (_plateSprite != null && PlateTexture != null)
        {
            _plateSprite.Texture = PlateTexture;
        }

		// Plate textures for chopped fruits on plate
		_plateApple = GD.Load<Texture2D>("res://Art/Assets/Equipment/FruitPlates/apple-plate-v1.png");
		_plateBanana = GD.Load<Texture2D>("res://Art/Assets/Equipment/FruitPlates/banana-plate-v1.png");
		_plateKiwi = GD.Load<Texture2D>("res://Art/Assets/Equipment/FruitPlates/kiwi-plate-v1.png");
		_plateGrape = GD.Load<Texture2D>("res://Art/Assets/Equipment/FruitPlates/grape-plate-v1.png");
		_plateLime = GD.Load<Texture2D>("res://Art/Assets/Equipment/JuiceCup/juice-cup-v1.png");
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

		if (_fruit.ToString().Equals("Lime"))
		{
			_plateSprite.Texture = _plateLime;
		}
	}
}
