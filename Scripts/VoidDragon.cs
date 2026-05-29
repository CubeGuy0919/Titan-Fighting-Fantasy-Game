using Godot;
using System;

public partial class VoidDragon : Node2D
{
    private float time = 0f;
    private Vector2 startPosition;

    public override void _Ready()
    {
        startPosition = Position;
    }

    public override void _Process(double delta)
    {
        time += (float)delta;

        // Breathing / floating idle animation
        Position = startPosition + new Vector2(Mathf.Sin(time * 2f) * 2f, 0);
    }
}