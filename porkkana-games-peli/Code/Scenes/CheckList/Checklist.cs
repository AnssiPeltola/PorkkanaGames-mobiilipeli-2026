using Godot;
using System;

public partial class Checklist : Node2D
{
	private Sprite2D _checkTomato;
	private Sprite2D _checkCarrot;
	private Sprite2D _checkOnion;
	private Sprite2D _checkPasta;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_checkTomato = GetNode<Sprite2D>("TomatoCheck");
		_checkCarrot = GetNode<Sprite2D>("CarrotCheck");
		_checkOnion = GetNode<Sprite2D>("OnionCheck");
		_checkPasta = GetNode<Sprite2D>("PastaCheck");

		_checkTomato.Visible = false;
		_checkCarrot.Visible = false;
		_checkOnion.Visible = false;
		_checkPasta.Visible = false;
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
		}
	}
}
