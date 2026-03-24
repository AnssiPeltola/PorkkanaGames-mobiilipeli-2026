using Godot;
using System;
using System.Collections.Generic;
using System.Linq;


/* Purpose:
 *      Holds all data and "things",
 *      That span accross levels
 *      Like:
 *          Total score
 *          Health
 *
 * Contains methods
 * - SetScore()
 * - GetFinalScore()
 *
 *
 */

public partial class GameManager : Node
{
	// ---------------------------------------------------------
	// Singleton
	// ---------------------------------------------------------
	public static GameManager Instance { get; private set; }

	private int _overallScore = 0;
	public readonly int levelOneRequired = 7;
	public readonly int levelTwoRequired = 4;
	public readonly int levelThreeRequired = 5;
	private bool levelOneWon = false;
	private bool levelTwoWon = false;
	public override void _Ready()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else if (Instance != this)
		{
			QueueFree();
			return;
		}
	}

	public int overAllScore
	{
		get { return _overallScore; }
		set
		{
			// Mathf.Clamp restricts a number to stay within a minimum and maximum range
			_overallScore = Mathf.Clamp(value, 0, Int32.MaxValue);
			GD.Print($"Points now: {_overallScore} Needed points: {levelOneRequired + levelTwoRequired + levelThreeRequired} to win game!");
		}
	}

		// Set +1 point on LevelOneScore
	public void GoodItemEntered()
	{
		_overallScore += 1;
		CheckLevelComplete();
	}

	// Take -1 point from LevelOneScore
	public void GoodItemExited()
	{
		_overallScore -= 1;
	}

	// Set +1 point on LevelOneScore
	public void BadItemEntered()
	{
		_overallScore += 1;
		CheckLevelComplete();
	}

	public void IngredientCooked()
	{
		_overallScore += 1;
		CheckLevelComplete();
	}

	public void CookedIngredientInDropzone()
	{
		_overallScore += 1;
		CheckLevelComplete();
	}

	public void ResetScore()
	{
		_overallScore = 0;
	}

	public void ResetLevelProgress()
	{
		levelOneWon = false;
		levelTwoWon = false;
	}

	private void CheckLevelComplete()
	{
		GD.Print(_overallScore);
		if (_overallScore >= levelOneRequired && !levelOneWon)
		{
			GD.Print("All items in right positions! Level 1 completed! Switching Scene!");
			// Switch scene - https://docs.godotengine.org/en/latest/tutorials/scripting/change_scenes_manually.html
			GetTree().ChangeSceneToFile("res://Scenes/Levels/LevelTwo/LevelTwoReal.tscn");
			levelOneWon = true;
			ResetScore();
		}

		if (_overallScore >=  levelTwoRequired && levelOneWon && !levelTwoWon) {
			GD.Print("All Ingredients are chopped and cooked! Level 2 completed! Switching Scene!");
			GetTree().ChangeSceneToFile("res://Scenes/Levels/LevelThree/LevelThree.tscn");
			levelTwoWon = true;
			ResetScore();
		}

		if (_overallScore >= levelThreeRequired && levelOneWon && levelTwoWon)
		{
			GD.Print("All cooked ingredients in right spot! Level 3 completed! Switching Scene!");
			GetTree().ChangeSceneToFile("res://Scenes/Menus/MainMenu.tscn");

			// Reset score!
			ResetScore();
			ResetLevelProgress();
		}
	}

	// Use List to store completed levelScores and to calculate
	// https://www.geeksforgeeks.org/c-sharp/list-class-in-c-sharp/
	// Requires:
	//      using System.Collections.Generic;
	List<int> SavedScore = new List<int>();
	int FinalScore = 0;

	public void SetScore(int Score)
	{
		//List feature <Name>.Add(n);
		SavedScore.Add(Score);
		GD.Print($"Level score {Score} saved!");
	}

	// Get the total score of all levels summed together
	// Reqires:
	//      using System.Linq;
	//      for the .Sum()
	public void GetFinalScore(int x)
	{
		FinalScore = SavedScore.Sum();
	}

	// ---------------------------------------------------------
	// Recipe Selection
	// ---------------------------------------------------------
	public RecipeData SelectedRecipe { get; set; }

}
