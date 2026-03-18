using Godot;
using System;
using System.Threading.Tasks;

public partial class TrashCan : Area2D
{
	public override void _Ready()
	{
		BodyEntered += OnBodyEntered;
	}

	private async void OnBodyEntered(Node2D body)
	{
		if (body is Ingredient box)
		{
			if (box.IsInGroup("Bad"))
			{
				GD.Print("Bad Ingredient entered & Deleted");
				DelayMethod(box);
				GameManager.Instance.BadItemEntered();
			}

			if (box.IsInGroup("Good"))
			{
				// Health system removed — no action needed
				GD.Print("Good Ingredient entered TrashCan (no penalty)");
			}
		}
	}

	private async void DelayMethod(Ingredient body)
	{
		await Task.Delay(TimeSpan.FromMilliseconds(500));
		body.QueueFree();
	}
}
