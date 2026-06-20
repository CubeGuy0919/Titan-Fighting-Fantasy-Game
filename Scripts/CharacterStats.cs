using Godot;

[GlobalClass]
public partial class CharacterStats : Resource
{
    [Export] public string CharacterName = "";

    [Export] public int MaxHP = 100;
    [Export] public int HP = 100;

    [Export] public int MaxMana = 50;
    [Export] public int Mana = 50;

    [Export] public int Skill = 100;
    [Export] public int MaxSkill = 100;

    [Export] public int Stamina = 20;
    [Export] public int MaxStamina = 100;

    [Export] public int Magic = 10;
    [Export] public int MaxMagic = 100;

    [Export] public int Luck = 0;

    [Export] public int AttackPower = 15;

    public bool IsDefending { get; set; } = false;

    public virtual void TakeDamage(int damage)
    {
        HP -= damage;

        if (HP < 0)
            HP = 0;
    }

    public bool UseStamina(int amount)
    {
        if (Stamina >= amount)
        {
            Stamina -= amount;
            return true; // Successfully spent
        }
        return false; // Not enough stamina!
    }

    public bool IsDead()
    {
        return HP <= 0;
    }
}