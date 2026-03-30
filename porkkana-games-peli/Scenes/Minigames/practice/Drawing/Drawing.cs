// NOTE:
// Create lines from dragging inputs
// Put the lines in "DrawingContainer"
// Touch = Create new Line2D()
// Drag = Add points to the line
// EndTouch = Stop adding points save the Line2D and prepare for new "cut"




using Godot;
using System;

public partial class Drawing : Node2D
{
 
 
    // Declaration
    private Node2D _drawingContainer;

    //----------
    // lines
    //----------
    // Since the cut lines are temporary
    // Create new node from code (Line2D)
    _currentLine = new Line2D();
    // Line properties:
    _currentLine.Width = 5f;

    // Is the player CURRENTLY drawing
    private bool _isPlayerDrawing = false;


    public override void _Ready()
    {
        // Give reference 
        _drawingContainer = GetNode<Node2D>("DrawingContainer");
    }


}
