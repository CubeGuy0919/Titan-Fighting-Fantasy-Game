using Godot;

public partial class Item
{
    public string Name { get; set; }
    public string Type { get; set; } // "Potion" or "Material"
    public int HealAmount { get; set; }

    public Item()
    {
    }
    
    public Item(string name, string type, int healAmount = 0)
    {
        Name = name;
        Type = type;
        HealAmount = healAmount;
    }
}