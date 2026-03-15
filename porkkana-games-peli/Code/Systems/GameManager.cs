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
	private int _levelOneScore = 0;
	[Export] public int RequiredGoodItems = 2;
	[Export] public int RequiredBadItems = 1;

	public int Health = 3;

	public int LevelOneScore
	{
		get { return _levelOneScore; }
		set
		{
			// Mathf.Clamp restricts a number to stay within a minimum and maximum range
			_levelOneScore = Mathf.Clamp(value, 0, Int32.MaxValue);
			GD.Print($"Points now: {LevelOneScore} Needed points: {RequiredGoodItems + RequiredBadItems}");
		}
	}

	#endregion

	public void LoseHealth()
	{
		Health--;
		GD.Print("Current health: ", Health);
		if (Health == 0)
		{
			GD.Print("You are dead!");
			Die();
		}
	}

	public void Die()
	{
		GetTree().Quit();
	}


	// Set +1 point on LevelOneScore
	public void GoodItemEntered()
	{
		LevelOneScore += 1;
		CheckLevelOneComplete();
	}

	// Take -1 point from LevelOneScore
	public void GoodItemExited()
	{
		LevelOneScore -= 1;
	}

	// Set +1 point on LevelOneScore
	public void BadItemEntered()
	{
		LevelOneScore += 1;
		CheckLevelOneComplete();
	}

    /* I will comment this out because chainging scenes is handled @ SceneControl:
	// Check if Level One is Completed and switch Scene
	private void CheckLevelOneComplete()
	{
		if (LevelOneScore >= (RequiredGoodItems + RequiredBadItems))
		{
			GD.Print("All items in right positions! Level completed! Switching Scene!");
			// Switch scene - https://docs.godotengine.org/en/latest/tutorials/scripting/change_scenes_manually.html
			GetTree().ChangeSceneToFile("res://Scenes/Levels/TestScene/TestScene.tscn");
		}
	}
    */
}
