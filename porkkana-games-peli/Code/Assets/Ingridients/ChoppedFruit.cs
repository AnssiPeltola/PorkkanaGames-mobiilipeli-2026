using Godot;
using System;

public partial class ChoppedFruit : BaseIngridient
{
	[Export] public Texture2D IngredientTexture;
	// private Sprite2D _sprite;

	public override void _Ready()
	{
		// We get this scenes Sprite2D node in variable _sprite
		_sprite = GetNode<Sprite2D>("Sprite2D");
		if (_sprite != null && IngredientTexture != null)
		{
			_sprite.Texture = IngredientTexture;
		}

		base._Ready();
	}
}
