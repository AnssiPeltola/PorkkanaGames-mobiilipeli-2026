using Godot;

public partial class RecipeButton : Button
{
	private RecipeData _recipe;

	public void Setup(RecipeData recipe)
	{
		_recipe = recipe;

		GetNode<TextureRect>("VBoxContainer/TextureRect").Texture = recipe.RecipeIcon;
		GetNode<Label>("VBoxContainer/Label").Text = recipe.RecipeName;

		Pressed += OnPressed;
	}

	private void OnPressed()
	{
		GameManager.Instance.SelectedRecipe = _recipe;
		SceneManager.Instance.LoadScene("res://Scenes/Levels/LevelOne.tscn");
	}
}
