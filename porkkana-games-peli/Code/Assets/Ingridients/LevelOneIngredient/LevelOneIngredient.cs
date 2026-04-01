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

	// Uses tween animation before destroying this ingredient from Scene with QueueFree()
	// https://www.patreon.com/posts/tween-cheatsheet-148942435
	public void DestroyTrash()
	{
		const float duration = 0.40f;
		var tween = CreateTween();

		// Tells that TweenPropertyes has to run at the same time
		tween.SetParallel(true);

		// Defines the shape of the animation curve (Cubic = smooth curve)
		tween.SetTrans(Tween.TransitionType.Cubic);

		// How the tween flow goes, In starts slow and speeds up
		tween.SetEase(Tween.EaseType.In);

		// Drops the ingredient slightly like it would "drop down" (Switches its position.Y for 30px in 0,4sec)
		tween.TweenProperty(this, "position:y", Position.Y + 30f, duration);

		// Scales down to 0,0 to create a quick "shrink away" effect.
		tween.TweenProperty(this, "scale", Vector2.Zero, duration);

		// Adds a small spin while disappearing. (Makes it rotate)
		tween.TweenProperty(this, "rotation", Rotation + 1.35f, duration);

		// Fades the sprite out completely. (Alpha -> 0)
		tween.TweenProperty(_sprite, "modulate:a", 0f, duration);

		// Run QueueFree after the parallel tween step has finished. (Chain() does this)
		tween.Chain().TweenCallback(Callable.From(() => QueueFree()));
	}
}
