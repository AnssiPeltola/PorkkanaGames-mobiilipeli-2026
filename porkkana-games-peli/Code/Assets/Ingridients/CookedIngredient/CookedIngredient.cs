using Godot;


// TODO:
// Anssi katso turhia pois

public partial class CookedIngredient : BaseIngridient
{
	public enum CookedIngredientKind
	{
		None,
		Pasta,
		Sauce
	}
	public CookedIngredientKind IngredientKind { get; set; } = CookedIngredientKind.None;
	private Texture2D _pastaTexture;
	private Texture2D _sauceTexture;
	private CollisionShape2D _pastaCollision;
	private CollisionShape2D _sauceCollision;

	public override void _Ready()
	{
		// We get this scenes Sprite2D node in variable _sprite
		_sprite = GetNode<Sprite2D>("Sprite2D");

		if (IngredientKind == CookedIngredientKind.Pasta)
		{
			_pastaTexture = GD.Load<Texture2D>("res://Art/Assets/Ingridients/Pasta/pasta.png");
			_pastaCollision = GetNode<CollisionShape2D>("PastaCollision");
			ChangeSprite(_pastaTexture);
			_pastaCollision.Disabled = false;
		}

		if (IngredientKind == CookedIngredientKind.Sauce)
		{
			_sauceTexture = GD.Load<Texture2D>("res://Art/Assets/Ingridients/Tomato/tomatosauce.png");
			_sauceCollision = GetNode<CollisionShape2D>("SauceCollision");
			ChangeSprite(_sauceTexture);
			_sauceCollision.Disabled = false;
		}
	}
}
