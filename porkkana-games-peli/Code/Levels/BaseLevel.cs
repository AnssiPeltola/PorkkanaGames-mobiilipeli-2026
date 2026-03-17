using Godot;
using System;


/* Base class for all levels to inherit from.
 *
 * Consider:
 *  Change this into Abstract class to force intialisation to include CurrentLevel number
 *
 *  Contains methods
 *  - GainScore()
 *  - TakeScore()
 *  - CheckScore()
 *  - ResetScore()
 *  - PrintLevelComplete()
 *  - RunLevelComplete()
 */


public partial class BaseLevel : Node
{
	// Protected = only member of the BaseLevel can access;
	protected int CurrentLevel { get; set; }
	protected int Score { get; set; }
	protected int RequiredScore { get; set; }
 
	// Just a test print method to inherit
	public virtual void PrintLevelComplete()
	{
		GD.Print($"From Level { CurrentLevel } We are complete!");
	}

	protected virtual void GainScore()
	{
		Score += 1;
		CheckScore();
	}

	protected virtual void LoseScore()
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
		RequiredScore = 0;
	}


}
