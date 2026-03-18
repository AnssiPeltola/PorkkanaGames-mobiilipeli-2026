using Godot;
using System;

public partial class IngredientSpawner : Area2D
{
    [Export] public PackedScene CookedIngredientScene;
    [Export] public Texture2D SpawnerTexture;
    [Export] public Texture2D SpawnedIngredientTexture;
	[Export] public CookedIngredient.CookedIngredientKind SpawnedIngredientKind = CookedIngredient.CookedIngredientKind.None;
    private CookedIngredient _cookedIngredient;
    private Sprite2D _sprite;
    private bool hasSpawned = false;

    public override void _Ready()
    {
        _sprite = GetNodeOrNull<Sprite2D>("Sprite2D");
        if (_sprite != null && SpawnerTexture != null)
        {
            _sprite.Texture = SpawnerTexture;
        }

        InputEvent += OnInputEvent;
    }

	// When we press with touch use SpawnIngredient(); function
    private void OnInputEvent(Node viewport, InputEvent @event, long shapeIdx)
    {
        if (hasSpawned)
            return;

		if (@event is InputEventScreenTouch touch && touch.Pressed)
		{
			SpawnIngredient();
			GD.Print("Spawner pressed!");
			return;
		}
    }

	// Spawns ingridient into scene on top of the IngridientSpawner and sets hasSpawned true to prevent multiple ingridient spawn.
    private void SpawnIngredient()
    {
        if (CookedIngredientScene == null)
        {
            GD.PrintErr("CookedIngredientScene not assigned!");
            return;
        }

        Vector2 spawnPosition = GlobalPosition;
        _cookedIngredient = CookedIngredientScene.Instantiate<CookedIngredient>();
        _cookedIngredient.IngredientKind = SpawnedIngredientKind;

        // Spawn at this spawner instance position in the level scene.
        GetTree().CurrentScene.AddChild(_cookedIngredient);
        _cookedIngredient.GlobalPosition = spawnPosition;

        if (SpawnedIngredientTexture != null)
        {
            Sprite2D cookedSprite = _cookedIngredient.GetNodeOrNull<Sprite2D>("Sprite2D");
            if (cookedSprite != null)
            {
                cookedSprite.Texture = SpawnedIngredientTexture;
            }
        }

        hasSpawned = true;
		InputEvent -= OnInputEvent;
    }
}
