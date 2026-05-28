using Godot;

public partial class CharacterStats : Resource
{
    [Export] public int Skill = 10;
    [Export] public int Stamina = 20;
    [Export] public int MaxStamina = 20;

    [Export] public int Magic = 10;
    [Export] public int MaxMagic = 10;

    [Export] public int Luck = 10;
}