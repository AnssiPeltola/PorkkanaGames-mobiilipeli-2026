using Godot;
using System;

public partial class Guidebook : Control
{
	[Export] public Texture2D GuideBookPageTexture;
	 private Sprite2D _sprite;

	// On load make this guidebook invisible
	public override void _Ready()
	{
		Visible = false;
		_sprite = GetNodeOrNull<Sprite2D>("Sprite2D");
        if (_sprite != null && GuideBookPageTexture != null)
        {
            _sprite.Texture = GuideBookPageTexture;
        }
	}
}
