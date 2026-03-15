using Godot;
using System;

/* 
 * Purpose:
 *      One place to decide what level plays next
 *
 * Features:
 * 		Assign paths to variables Level<Nr> etc etc.
 * 		Fetch the next level if current level = n, next level = (n + 1)
 *
 * How to use:
 *      First add new variable and path to it
 *      Scale thegetLevelPath & GetNextLevel Switch boards by 1
 *
 * Consider should we use arrays here instead?
 */


public static class LevelOrder
{
	// Paths to all levels in the game.
	private static readonly string LevelOne = "res://Scenes/Levels/LevelOne/LevelOne.tscn";
	private static readonly string LevelTwo = "res://Scenes/Levels/LevelTwo/LevelTwoReal.tscn";
	// private static readonly string LevelTwo = "res://Scenes/Levels/LevelTwo/LevelTwo.tscn";
	private static readonly string LevelThree = "res://Scenes/Levels/LevelThree/LevelThree.tscn";

	// Method that returns the path of given LevelNumber
	public static string GetLevelPath(int LevelNumber)
	{
		switch (LevelNumber)
		{
			case 1:
				return LevelOne;
			 case 2:
				return LevelTwo;
			case 3:
				return LevelThree;
			default:
				return LevelOne;
		}
	}

	// Return Next level's number 
	// Give CurrentLevel n, then return (n + 1);
	public static int GetNextLevel(int currentLevel)
	{
		switch (currentLevel)
		{
			case 1:
				return 2;
			 case 2:
				return 3;
			// For now: Loop back to start
			case 3:
				return 1;
			default:
				return 1;
		}
	}
		
}
