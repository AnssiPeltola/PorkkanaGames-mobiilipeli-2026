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
	private CollisionPolygon2D _pastaCollision;
	private CollisionPolygon2D _sauceCollision;
	private CollisionPolygon2D _pastaTouch;
	private CollisionPolygon2D _sauceTouch;

	public override void _Ready()
	{
		// We get this scenes Sprite2D node in variable _sprite
		_sprite = GetNode<Sprite2D>("Sprite2D");

		if (IngredientKind == CookedIngredientKind.Pasta)
		{
			_pastaCollision = GetNode<CollisionPolygon2D>("PastaCollision");
			_pastaTouch = GetNode<CollisionPolygon2D>("TouchArea/PastaTouch");
			_pastaCollision.Disabled = false;
			_pastaTouch.Disabled = false;
		}

		if (IngredientKind == CookedIngredientKind.Sauce)
		{
			_sauceCollision = GetNode<CollisionPolygon2D>("SauceCollision");
			_sauceTouch = GetNode<CollisionPolygon2D>("TouchArea/SauceTouch");
			_sauceCollision.Disabled = false;
			_sauceTouch.Disabled = false;
		}

		// Do also ready from BaseIngredient (Load TouchArea)
		base._Ready();
	}
}
