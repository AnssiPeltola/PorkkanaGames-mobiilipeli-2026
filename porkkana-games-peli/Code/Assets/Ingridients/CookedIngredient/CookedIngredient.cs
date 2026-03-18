using Godot;

public partial class CookedIngredient : CharacterBody2D
{
	public enum CookedIngredientKind
	{
		None,
		Pasta,
		Sauce
	}
	private bool _dragging = false;
	[Export] private int _clickRadius = 32;
	public CookedIngredientKind IngredientKind { get; set; } = CookedIngredientKind.None;
	private Sprite2D _sprite;
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

	// This function is called for every input event (mouse, keyboard, touch, etc.)
   public override void _Input(InputEvent e)
    {
		// Only react to screen touch events (mobile / mouse click)
        if (e is InputEventScreenTouch touch)
        {
			// Check if the touch is close enough to this object to start dragging and set _dragging true
			if ((touch.Position - GlobalPosition).Length() < _clickRadius)
			{
            	_dragging = touch.Pressed;
			}
        }
    }

	// if dragging false does nothing
	public override void _PhysicsProcess(double delta)
    {
        if (!_dragging)
		{
			return;
		}

		// Get the current position of finger/mouse
        Vector2 target = GetGlobalMousePosition();
        Vector2 direction = target - GlobalPosition;

        Velocity = direction / (float)delta;
		// Makes the move using Godot physics
        MoveAndSlide();
    }

	// Function that changes this scenes Sprite2D texture to new
	public void ChangeSprite(Texture2D newTexture)
	{
		_sprite.Texture = newTexture;
	}
}
