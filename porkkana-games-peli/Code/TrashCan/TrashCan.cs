using Godot;

// https://www.youtube.com/watch?v=RS1uqBIVruQ - GODOT - Removing Objects on Collisions
public partial class TrashCan : Area2D
{
	// Makes the connection signal for method OnBodyEntered
    public override void _Ready()
    {
        BodyEntered += OnBodyEntered;
    }

    private void OnBodyEntered(Node2D body)
    {
        if (body is Ingredient box)
        {
			// If box that collides TrashCan is in group "bad" it will delete this object from game
            if (box.IsInGroup("Bad"))
            {
                GD.Print("Bad Ingredient entered & Deleted");
				// QueueFree() function will delete the Node and all its child nodes
				body.QueueFree();
                // Set +1 point on LevelOneScore
                GameManager.Instance.BadItemEntered();
            }
        }
    }
}