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
	private static readonly string LevelFruitOne = "res://Scenes/Levels/FruitLevels/FruitLevelOne/FruitLevelOne.tscn";
	private static readonly string LevelFruitTwo = "res://Scenes/Levels/FruitLevels/FruitLevelTwo/FruitLevelTwo.tscn";
	private static readonly string LevelFruitThree = "res://Scenes/Levels/FruitLevels/FruitLevelThree/FruitLevelThree.tscn";

	// Method that returns the path of given LevelNumber
	// Pasta level starts from 1 (level1) and then currentlevel 2/3 are for pastalevel levels 2 and 3.
	// Fruit level starts from 4 (level1) and 5/6 are for level 2/3 in fruit recipe)
	public static string GetLevelPath(int LevelNumber)
	{
		switch (LevelNumber)
		{
			// Pasta
			case 1:
				return LevelOne;
			case 2:
				return LevelTwo;
			case 3:
				return LevelThree;
			// Fruit
			case 4:
				return LevelFruitOne;
			case 5:
				return LevelFruitTwo;
			case 6:
				return LevelFruitThree;
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
