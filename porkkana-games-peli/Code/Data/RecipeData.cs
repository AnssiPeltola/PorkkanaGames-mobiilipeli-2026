using Godot;
using System;

[GlobalClass] // Makes it appear in “New Resource”
public partial class RecipeData : Resource
{
	[Export] public string RecipeName { get; set; }
	[Export] public Texture2D RecipeIcon { get; set; }
	[Export] public string Description { get; set; }
}
