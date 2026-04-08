using Godot;
using System;

public partial class Guidebook : Control
{
	[Export] public Texture2D PageOneTexture;
	[Export] public Texture2D PageTwoTexture;

	 private Sprite2D _pageOne;
	 private Sprite2D _pageTwo;

	// On load make this guidebook invisible
	public override void _Ready()
	{
		Visible = false;
		_pageOne = GetNodeOrNull<Sprite2D>("PageOne");
		_pageTwo = GetNodeOrNull<Sprite2D>("PageTwo");

        if (_pageOne != null && PageOneTexture != null)
        {
            _pageOne.Texture = PageOneTexture;
        }

		if (_pageTwo != null && PageTwoTexture != null)
        {
            _pageTwo.Texture = PageTwoTexture;
        }
	}
}
