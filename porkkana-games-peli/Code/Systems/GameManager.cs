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
