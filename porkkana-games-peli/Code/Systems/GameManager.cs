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

	#region Singleton
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
	#endregion

	#region Game Data
	// Use List to store completed levelScores and to calculate
	// https://www.geeksforgeeks.org/c-sharp/list-class-in-c-sharp/
	// Requires:
	//      using System.Collections.Generic;
	List<int> SavedScore = new List<int>();
	int FinalScore = 0;


	#endregion
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

}
