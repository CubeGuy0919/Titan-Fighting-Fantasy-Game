using Godot;
using System.Collections.Generic;

public partial class ActionMenu : Control
{
    private BattleManager battleManager;
    private Player player;

    // Pop-up UI Node References
    private PanelContainer subMenuPanel;
    private Label subMenuTitle;
    private Container buttonGrid;

    [Export] public string FontPath = "res://FontStyles/ByteBounce.ttf"; 
    [Export] public int CustomFontSize = 24;

    private Font customButtonFont;

    public override void _Ready()
    {
        battleManager = GetNode<BattleManager>("%BattleManager");
        player = GetNode<Player>("%Player");

        subMenuPanel = GetNode<PanelContainer>("%SubMenuPanel");
        subMenuTitle = subMenuPanel.GetNode<Label>("VBoxContainer/SubMenuTitle");
        buttonGrid = subMenuPanel.GetNode<Container>("VBoxContainer/ButtonGrid");

        if (ResourceLoader.Exists(FontPath))
        {
            customButtonFont = GD.Load<Font>(FontPath);
        }
        else
        {
            GD.PrintErr($"Could not find ByteBounce font at: {FontPath}. Check your folder name spelling!");
        }

        // Core Actions Hookup
        GetNode<Button>("%AttackButton").Pressed += OnAttackPressed;
        GetNode<Button>("%DefendButton").Pressed += OnDefendPressed;
        
        GetNode<Button>("%MagicButton").Pressed += OpenMagicMenu;
        GetNode<Button>("%ItemButton").Pressed += OpenItemMenu;
        GetNode<Button>("%BackButton").Pressed += CloseSubMenu;
    }

    private void OnAttackPressed() => battleManager?.PlayerAttack();
    private void OnDefendPressed() => battleManager?.PlayerDefend();

    private void OpenMagicMenu()
    {
        ClearGrid();
        subMenuTitle.Text = "SPELLS (Costs 15 Stamina)";
        subMenuPanel.Visible = true;

        Button spellButton = new Button();
        spellButton.Text = "Igni (Fire Blast)";
        
        ApplyCustomFont(spellButton);

        spellButton.Pressed += () => {
            GD.Print("Igni casted!");
            CloseSubMenu();
        };
        buttonGrid.AddChild(spellButton);
    }

    private void OpenItemMenu()
    {
        ClearGrid();
        subMenuTitle.Text = "INVENTORY ITEMS";
        subMenuPanel.Visible = true;

        if (player == null || player.Inventory.Count == 0)
        {
            Label emptyLabel = new Label();
            emptyLabel.Text = "Your pockets are empty!";
            buttonGrid.AddChild(emptyLabel);
            return;
        }

        foreach (Item item in player.Inventory)
        {
            Button itemButton = new Button();
            itemButton.Text = $"{item.Name} (+{item.HealAmount} HP)";
            
            ApplyCustomFont(itemButton);
            
            itemButton.Pressed += () => {
                GD.Print($"Used item: {item.Name}");
                player.Inventory.Remove(item); 
                CloseSubMenu();
            };
            buttonGrid.AddChild(itemButton);
        }
    }

    private void ApplyCustomFont(Button targetButton)
    {
        if (customButtonFont != null)
        {
            targetButton.AddThemeFontOverride("font", customButtonFont);
            targetButton.AddThemeFontSizeOverride("font_size", CustomFontSize);
        }
    }

    private void CloseSubMenu() => subMenuPanel.Visible = false;

    private void ClearGrid()
    {
        foreach (Node child in buttonGrid.GetChildren())
        {
            child.QueueFree();
        }
    }
}