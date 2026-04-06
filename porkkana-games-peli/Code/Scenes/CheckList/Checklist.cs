using Godot;
using System;
using System.Security.Cryptography;

public partial class Checklist : Node2D
{
	[Export] public Texture2D _checklistTexture;
	private Sprite2D _checklistSprite;
	private Sprite2D _checkTomato;
	private Sprite2D _checkCarrot;
	private Sprite2D _checkOnion;
	private Sprite2D _checkPasta;
	private Sprite2D _checkApple;
	private Sprite2D _checkKiwi;
	private Sprite2D _checkBanana;
	private Sprite2D _checkGrapes;
	private Sprite2D _checkLime;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		// Pasta level
		_checklistSprite = GetNode<Sprite2D>("Sprite2D");
		_checkTomato = GetNode<Sprite2D>("TomatoCheck");
		_checkCarrot = GetNode<Sprite2D>("CarrotCheck");
		_checkOnion = GetNode<Sprite2D>("OnionCheck");
		_checkPasta = GetNode<Sprite2D>("PastaCheck");

		// Fruitsalad level
		_checkApple = GetNode<Sprite2D>("AppleCheck");
		_checkKiwi = GetNode<Sprite2D>("KiwiCheck");
		_checkBanana = GetNode<Sprite2D>("BananaCheck");
		_checkGrapes = GetNode<Sprite2D>("GrapesCheck");
		_checkLime = GetNode<Sprite2D>("LimeCheck");

		// Pasta level
		_checkTomato.Visible = false;
		_checkCarrot.Visible = false;
		_checkOnion.Visible = false;
		_checkPasta.Visible = false;

		// Fruitsalad level
		_checkApple.Visible = false;
		_checkKiwi.Visible = false;
		_checkBanana.Visible = false;
		_checkGrapes.Visible = false;
		_checkLime.Visible = false;

		// Set texture given from editor as texture for Sprite2D (Checklist)
		// This so we can use the same scene in other levels too
		if (_checklistSprite != null && _checklistTexture != null)
        {
            _checklistSprite.Texture = _checklistTexture;
        }
	}

	public void SetIngredientCheck(string ingredientGroup, bool isVisible)
	{
		switch (ingredientGroup)
		{
			case "Tomato":
				_checkTomato.Visible = isVisible;
				break;
			case "Carrot":
				_checkCarrot.Visible = isVisible;
				break;
			case "Onion":
				_checkOnion.Visible = isVisible;
				break;
			case "Pasta":
				_checkPasta.Visible = isVisible;
				break;
			case "Apple":
				_checkApple.Visible = isVisible;
				break;
			case "Kiwi":
				_checkKiwi.Visible = isVisible;
				break;
			case "Banana":
				_checkBanana.Visible = isVisible;
				break;
			case "Grapes":
				_checkGrapes.Visible = isVisible;
				break;
			case "Lime":
				_checkLime.Visible = isVisible;
				break;
		}
	}
}
