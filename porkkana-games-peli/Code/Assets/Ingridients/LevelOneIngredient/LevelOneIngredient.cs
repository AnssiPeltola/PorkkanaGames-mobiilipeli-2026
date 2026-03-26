using Godot;
using System;

public partial class LevelOneIngredient : BaseIngridient
{
	// Set any sprite for the ingredient in editor
	[Export] public Texture2D IngredientTexture;
	private CollisionShape2D _circleCollision;
    private CollisionPolygon2D _bananaCollision;
    private CollisionPolygon2D _carrotCollision;
	private CollisionShape2D _pastaCollision;

	private CollisionShape2D _circleTouch;
    private CollisionShape2D _bananaTouch;
    private CollisionShape2D _carrotTouch;
    private CollisionShape2D _pastaTouch;

	public override void _Ready()
	{
		// We get this scenes Sprite2D node in variable _sprite
		_sprite = GetNode<Sprite2D>("Sprite2D");
		if (_sprite != null && IngredientTexture != null)
		{
			_sprite.Texture = IngredientTexture;
		}

		// Physics collisions
        _circleCollision = GetNode<CollisionShape2D>("CircleCollision");
        _bananaCollision = GetNode<CollisionPolygon2D>("BananaCollision");
        _carrotCollision = GetNode<CollisionPolygon2D>("CarrotCollision");
		_pastaCollision = GetNode<CollisionShape2D>("PastaCollision");

        // Touch collisions
        _circleTouch = GetNode<CollisionShape2D>("TouchArea/CircleTouch");
        _bananaTouch = GetNode<CollisionShape2D>("TouchArea/BananaTouch");
        _carrotTouch = GetNode<CollisionShape2D>("TouchArea/CarrotTouch");
		_pastaTouch = GetNode<CollisionShape2D>("TouchArea/PastaTouch");

		// Enables right collision and toucharea for ingredients based of group
        if (IsInGroup("Circle"))
        {
            _circleCollision.Disabled = false;
            _circleTouch.Disabled = false;
        }

        if (IsInGroup("Banana"))
        {
            _bananaCollision.Disabled = false;
            _bananaTouch.Disabled = false;
        }

        if (IsInGroup("Carrot"))
        {
            _carrotCollision.Disabled = false;
            _carrotTouch.Disabled = false;
        }

		if (IsInGroup("Pasta"))
        {
            _pastaCollision.Disabled = false;
            _pastaTouch.Disabled = false;
        }

		// Do also ready from BaseIngredient (Load TouchArea)
		base._Ready();

        // Set sprite
        if (_sprite != null && IngredientTexture != null)
        {
            _sprite.Texture = IngredientTexture;
        }
	}
}
