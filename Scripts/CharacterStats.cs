using Godot;

public partial class CharacterStats : Resource
{
    [Export] public int HP = 100;

    [Export] public int Skill = 100;
    [Export] public int Stamina = 20;
    [Export] public int MaxStamina = 100;

    [Export] public int Magic = 10;
    [Export] public int MaxMagic = 100;

    [Export] public int Luck = 10;
}