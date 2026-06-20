using Godot;
using System.Collections.Generic;

public partial class Player : Node2D
{
    [Export] public CharacterStats playerStats;
    public List<Item> Inventory { get; set; } = new List<Item>();

    public override void _Ready()
    {
        // Character stats setup
        if (playerStats == null)
        {
            playerStats = new CharacterStats();
        }
        playerStats.CharacterName = "Geralt Of Rivia";

        playerStats.MaxHP = 100;
        playerStats.HP = 100;

        playerStats.MaxMana = 100;
        playerStats.Mana = 100;

        playerStats.Skill = 100;
        playerStats.MaxSkill = 100;

        playerStats.Stamina = 100;
        playerStats.MaxStamina = 100;

        playerStats.Magic = 100;
        playerStats.MaxMagic = 100;

        playerStats.Luck = 10;

        playerStats.AttackPower = 20;

        playerStats.IsDefending = false;

        Inventory.Clear();

        Inventory.Add(new Item("Health Potion", "Potion", 30));
        Inventory.Add(new Item("Health Potion", "Potion", 30));
        Inventory.Add(new Item("Dragon Scale", "Material"));
    }
}