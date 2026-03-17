using Godot;
using System;

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
	// LEVEL ONE
	// LEVEL ONE
	private int _levelOneScore { get; set; }
	private int _totalScore { get; set; }

	#endregion
	public void SetScore(int Score)
	{
		_levelOneScore = Score;
		GD.Print($"Level score saved!");
	}
	

}
