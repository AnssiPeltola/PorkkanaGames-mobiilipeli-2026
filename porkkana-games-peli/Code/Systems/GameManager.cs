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
	// Staattinen autoproperty.
	// Get on public, jotta GameManageriin päästään käsiksi mistä vain.
	// Set private, jotta sitä ei voisi helposti ylikirjoittaa.
	// https://docs.godotengine.org/en/stable/tutorials/scripting/singletons_autoload.html
	public static GameManager Instance
	{
		get;
		private set;
	}

	public GameManager()
	{
		// Singleton takaa, että luokasta voidaan tehdä vain yksi olio kerrallaan.
		if (Instance == null)
		{
			// Ainoata oliota ei ole vielä määritetty. Olkoon tämä olio se.
			Instance = this;
		}
		else if (Instance != this)
		{
			// Singleton-olio on jo olemassa! Tuhotaan juuri luotu olio.
			QueueFree();
			return;
		}
	}

	private int _score = 0;
	public int levelOneRequired { get; set; }
	public int levelTwoRequired { get; set; }
	public int levelThreeRequired { get; set; }
	public int currentLevel { get; set; }
	private bool levelOneWon = false;
	private bool levelTwoWon = false;

	public int Score
	{
		get { return _score; }
		set
		{
			// Mathf.Clamp restricts a number to stay within a minimum and maximum range
			_score = Mathf.Clamp(value, 0, Int32.MaxValue);
			GD.Print($"Points now: {_score}");
		}
	}

	public void AddScore()
	{
		Score += 1;
		CheckLevelComplete();
	}

	public void MinusScore()
	{
		Score -= 1;
	}

	public void ResetScore()
	{
		Score = 0;
	}

	public void ResetLevelProgress()
	{
		levelOneWon = false;
		levelTwoWon = false;
		currentLevel = 0;
	}

	private void CheckLevelComplete()
	{
		GD.Print(Score);
		// Level one completed, switch scene into level two and reset score
		if (Score >= levelOneRequired && !levelOneWon)
		{
			GD.Print("All items in right positions! Level 1 completed! Switching Scene!");
			// currentLevel +1
			// Pasta level starts from 1 (level1) and then currentlevel 2/3 are for pastalevel levels 2 and 3.
			// Fruit level starts from 4 (level1) and 5/6 are for level 2/3 in fruit recipe)
			currentLevel++;
			FadeTransition.ChangeSceneWithFade(LevelOrder.GetLevelPath(currentLevel));
			levelOneWon = true;
			ResetScore();
		}

		// Level two completed, switch scene into level three and reset score
		if (Score >=  levelTwoRequired && levelOneWon && !levelTwoWon) {
			GD.Print("All Ingredients are chopped and cooked! Level 2 completed! Switching Scene!");
			currentLevel++;
			FadeTransition.ChangeSceneWithFade(LevelOrder.GetLevelPath(currentLevel));
			levelTwoWon = true;
			ResetScore();
		}

		// Level three completed, switch scene into main menu and reset score+progress
		if (Score >= levelThreeRequired && levelOneWon && levelTwoWon)
		{
			GD.Print("All cooked ingredients in right spot! Level 3 completed! Switching Scene!");
			LevelWonTransition.ChangeSceneWithFade("res://Scenes/Menus/MainMenu.tscn");
			// FadeTransition.ChangeSceneWithFade("res://Scenes/Menus/MainMenu.tscn");
			// Reset score and progress
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
