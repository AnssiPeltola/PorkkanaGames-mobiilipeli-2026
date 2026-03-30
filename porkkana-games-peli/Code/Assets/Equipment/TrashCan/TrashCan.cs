using Godot;
using System;
using System.Threading.Tasks;


//TODO:
//Make TrashCan talk to LevelOne.cs,
//for score
//not GameManager


// https://www.youtube.com/watch?v=RS1uqBIVruQ - GODOT - Removing Objects on Collisions
public partial class TrashCan : Area2D
{
	// private static readonly Color WrongColor = new Color(1.0f, 0.4f, 0.4f, 1.0f);
	// private static readonly Color NormalColor = Colors.White;

	// Makes the connection signal for method OnBodyEntered
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
		// BodyExited += OnBodyExited;
	}

	private async void OnBodyEntered(Node2D body)
	{
		if (body is LevelOneIngredient box)
		{
			// If box that collides TrashCan is in group "bad" it will delete this object from game
			if (box.IsInGroup("Bad"))
			{
				GD.Print("Bad Ingredient entered & Deleted");
				DelayMethod(box);
				// Add +1 Score
				GameManager.Instance.AddScore();
			}

			// if (box.IsInGroup("Good"))
			// {
			// 	box.Modulate = WrongColor;
			// }
		}
	}

	// private async void OnBodyExited(Node2D body)
	// {
	// 	if (body is LevelOneIngredient box)
	// 	{
	// 		if (box.IsInGroup("Good"))
	// 		{
	// 			box.Modulate = NormalColor;
	// 		}
	// 	}
	// }

	// https://forum.godotengine.org/t/create-a-delay-between-code-execution-using-c/12714/5
	// Delays function by 0.5sec and delete LevelOneIngredient
	private async void DelayMethod(LevelOneIngredient body)
	{
		await Task.Delay(TimeSpan.FromMilliseconds(500));
		// QueueFree() function will delete the Node and all its child nodes
		body.QueueFree();
	}
}
