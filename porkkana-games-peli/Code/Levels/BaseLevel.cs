using Godot;
using System;


/* Base class for all levels to inherit from.
 *
 * Consider:
 *  Change this into Abstract class? 
 *
 *  Contains methods
 *  - GainScore()
 *  - TakeScore()
 *  - CheckScore()
 *  - ResetScore()
 *  - PrintLevelComplete()
 *  - RunLevelComplete()
 *
 *  NOTE:
 *      Again, we need to make to reference the instances,
 *      run by this non-static class
 *      This happens in _Ready() where we store the current,
 *      instance to CurrentActiveLevel
 *
 */


public partial class BaseLevel : Node
{
	// Create a reference for currentl instance of a level
	// Each level stores its "this" (=current instance),
	// To the CurrentActiveLevel reference.
	public static BaseLevel CurrentActiveLevel { get; set; }

	// Protected = only member of the BaseLevel can access;
	protected int CurrentLevel { get; set; }
	protected int Score { get; set; }
	protected int RequiredScore { get; set; }

	public override void _Ready()
	{
		// Each level on _Ready() will run:
		// Save "this" (= current instance that is running)
		// to reference "CurrentActiveLevel"
		CurrentActiveLevel = this;
	}
 
	// Just a test print method to inherit
	public virtual void PrintLevelComplete()
	{
		GD.Print($"From Level { CurrentLevel } We are complete!");
	}

	public virtual void GainScore()
	{
		Score += 1;
		CheckScore();
	}

	public virtual void LoseScore()
	{
		Score -= 1;
		CheckScore();
	}

	public void CheckScore()
	{
		if (Score >= RequiredScore)
		{
			PrintLevelComplete();
			RunLevelComplete();
		}
		else
		{
			GD.Print($"Need more score");
		}
	}

	protected virtual void ResetScore()
	{
		Score = 0;
		RequiredScore = 0;
	}
	protected virtual void RunLevelComplete()
	{
		// Contact SceneControl's
		// Instance: "Current"
		// Give it the CurrentLevel (int)
		SceneControl.Current.OnComplete(CurrentLevel);

		// Give GameManager CurrentLevelScore
		GameManager.Instance.SetScore(Score);
		ResetScore();
	}
}
