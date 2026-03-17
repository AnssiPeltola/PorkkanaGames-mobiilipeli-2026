using Godot;
using System;
using System.Threading.Tasks;


//TODO:
//Make TrashCan talk to LeveLone.cs
//for score
//not GameManager


// https://www.youtube.com/watch?v=RS1uqBIVruQ - GODOT - Removing Objects on Collisions
public partial class TrashCan : Area2D
{
	// Makes the connection signal for method OnBodyEntered
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
	}

	private async void OnBodyEntered(Node2D body)
	{
		if (body is Ingredient box)
		{
			// If box that collides TrashCan is in group "bad" it will delete this object from game
			if (box.IsInGroup("Bad"))
			{
				GD.Print("Bad Ingredient entered & Deleted");
				DelayMethod(box);
				// Set +1 point on LevelOneScore
				//LevelOne.GetScore();
			}
			// If
			if (box.IsInGroup("Good"))
			{
				//// GameManager.Instance.LoseHealth();
			
			}
		}
	}

	// https://forum.godotengine.org/t/create-a-delay-between-code-execution-using-c/12714/5
	// Delays function by 0.5sec
	private async void DelayMethod(Ingredient body)
	{
		await Task.Delay(TimeSpan.FromMilliseconds(500));
		// QueueFree() function will delete the Node and all its child nodes
		body.QueueFree();
	}
}
