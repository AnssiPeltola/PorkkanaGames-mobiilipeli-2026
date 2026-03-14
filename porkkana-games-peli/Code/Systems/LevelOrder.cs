using Godot;
using System;

/* Purpose:
 *  One place to decide what level plays next
 *
 * Features:
 *  - Method() to get path to a level.
 *          GetPath(int LevelNumber)
 *  - Method() to return next level number to sceneControl
 *          GetNextLevel(int currentLevel)
 *              NextLevel = n + 1
 *           
 */


public static class LevelOrder
{

	// Paths to all levels in the game.
	private static readonly string LevelOne = "res://Scenes/Levels/LevelOne/LevelOne.tscn";
	private static readonly string LevelTwo = "res://Scenes/Levels/LevelTwo/LevelTwo.tscn";
	private static readonly string LevelThree = "res://Scenes/Levels/LevelTwo/LevelTwo.tscn";

	// Method that returns the current level path
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
				return 1;
		}
	}

	// Load next level 
	// simple logic if level 1 then return (n + 1);
	public static int GetNextLevel(int currentLevel)
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
		
