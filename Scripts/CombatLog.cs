using Godot;

public partial class CombatLog : RichTextLabel
{
    public override void _Ready()
    {
        Text += "\n";
    }
    public void AddMessage(string message)
    {
        Text += $"  {message}\n";
    }
}