using Godot;
using System;

public partial class SeasoningIngredientScene : BaseIngridient
{
	public enum SeasoningKind
	{
		None,
		Pepper,
		Salt,
		Basil
	}
	[Export] public SeasoningKind SeasonKind = SeasoningKind.None;
	private Texture2D _pepperTexture;
	private Texture2D _saltTexture;
	private Texture2D _basilTexture;
	private CollisionShape2D _shakerCollision;
	private CollisionShape2D _basilCollision;

	public override void _Ready()
	{
		// We get this scenes Sprite2D node in variable _sprite
		_sprite = GetNode<Sprite2D>("Sprite2D");

		// Load texture and collision by what SeasoningKind is set from Editor
		if (SeasonKind == SeasoningKind.Pepper)
		{
			_pepperTexture = GD.Load<Texture2D>("res://Art/Assets/Ingridients/SaltPepper/pepper-shaker-v1.png");
			_shakerCollision = GetNode<CollisionShape2D>("ShakerCollision");
			ChangeSprite(_pepperTexture);
			_shakerCollision.Disabled = false;
		}

		if (SeasonKind == SeasoningKind.Salt)
		{
			_saltTexture = GD.Load<Texture2D>("res://Art/Assets/Ingridients/SaltPepper/salt-shaker-v1.png");
			_shakerCollision = GetNode<CollisionShape2D>("ShakerCollision");
			ChangeSprite(_saltTexture);
			_shakerCollision.Disabled = false;
		}

		if (SeasonKind == SeasoningKind.Basil)
		{
			_basilTexture = GD.Load<Texture2D>("res://Art/Assets/Ingridients/Basil/basil-leaf-v2.png");
			_basilCollision = GetNode<CollisionShape2D>("BasilCollision");
			ChangeSprite(_basilTexture);
			_basilCollision.Disabled = false;
		}
	}
}
