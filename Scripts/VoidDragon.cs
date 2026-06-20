using Godot;
using System;

public partial class VoidDragon : Node2D
{
    private float time = 0f;
    private Vector2 startPosition;

    [Export] private AnimatedSprite2D animatedSprite;

    [Export] public CharacterStats enemyStats;

    public override void _Ready()
    {
        // Character stats setup
                if (enemyStats == null)
        {
            enemyStats = new CharacterStats(); // Or GD.Load<CharacterStats>("res://path_to_resource.tres") if it's a saved resource file
        }
        enemyStats.CharacterName = "Void Dragon";
        enemyStats.MaxHP = 250;
        enemyStats.HP = 250;

        enemyStats.MaxMana = 150;
        enemyStats.Mana = 150;

        enemyStats.AttackPower = 25;

        enemyStats.TakeDamage(0); // Ensure HP is set correctly in the UI
        enemyStats.IsDead(); // Ensure death status is correct at the start

        startPosition = Position;

        if (animatedSprite == null)
        {
            animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
        }

        if (animatedSprite != null)
        {
            animatedSprite.Play("Idle Stock");
        }
        else
        {
            GD.PrintErr("Could not find AnimatedSprite2D node!");
        }
    }

    public override void _Process(double delta)
    {
        time += (float)delta;

        Position = startPosition + new Vector2(0, Mathf.Sin(time * 2f) * 5f);
    }

    public void TakeDamage(int damage)
    {
        enemyStats.TakeDamage(damage);
    }

    public bool IsDead()
    {
        return enemyStats.IsDead();
    }
}