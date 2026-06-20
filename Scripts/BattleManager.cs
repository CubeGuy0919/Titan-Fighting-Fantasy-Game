using Godot;

public partial class BattleManager : Node
{
    private Player player;
    private VoidDragon enemy;

    private CombatLog combatLog;
    private bool playerTurn = true;

    private TextureProgressBar playerHPBar;
    private TextureProgressBar enemyHPBar;

    private Label turnIndicatorLabel;
    private RichTextLabel turnIndicator;

    public override void _Ready()
    {
        player = GetNode<Player>("%Player");
        enemy = GetNode<VoidDragon>("%VoidDragon");
        combatLog = GetNode<CombatLog>("%CombatLog");

        playerHPBar = GetNode<TextureProgressBar>("%PlayerHPBar");
        enemyHPBar = GetNode<TextureProgressBar>("%EnemyHPBar");

        turnIndicatorLabel = GetNode<Label>("%TurnIndicatorLabel");
        turnIndicator = GetNode<RichTextLabel>("%TurnIndicator");
        
        if (turnIndicator != null)
        {
            turnIndicator.BbcodeEnabled = true;
        }

        UpdateTurnUI();
    }

    public override void _Process(double delta)
    {
        if (player == null || enemy == null || playerHPBar == null || enemyHPBar == null)
            return;

        playerHPBar.MaxValue = player.playerStats.MaxHP;
        playerHPBar.Value = player.playerStats.HP;

        enemyHPBar.MaxValue = enemy.enemyStats.MaxHP;
        enemyHPBar.Value = enemy.enemyStats.HP;
    }

    private void UpdateTurnUI()
    {
        if (turnIndicator == null || turnIndicatorLabel == null) return;

        StyleBoxFlat styleBox = new StyleBoxFlat();
        styleBox.BorderWidthTop = 2;
        styleBox.BorderWidthBottom = 2;
        styleBox.BorderWidthLeft = 2;
        styleBox.BorderWidthRight = 2;
        styleBox.CornerRadiusTopLeft = 4;
        styleBox.CornerRadiusTopRight = 4;
        styleBox.CornerRadiusBottomLeft = 4;
        styleBox.CornerRadiusBottomRight = 4;

        if (playerTurn)
        {
            turnIndicator.Text = "[center][color=#33e64d]YOUR TURN[/color][/center]";
            styleBox.BgColor = Color.Color8(71, 177, 111);
            styleBox.BorderColor = Color.Color8(0, 64, 32);
        }
        else
        {
            turnIndicator.Text = "[center][color=#e63333]ENEMY'S TURN[/color][/center]";
            styleBox.BgColor = Color.Color8(248, 100, 91);
            styleBox.BorderColor = Color.Color8(98, 13, 14);
        }

        turnIndicatorLabel.AddThemeStyleboxOverride("normal", styleBox);
    }

    public async void PlayerAttack()
    {
        if (!playerTurn || player == null || enemy == null)
            return;

        playerTurn = false;
        UpdateTurnUI();

        int damage = player.playerStats.AttackPower;
        enemy.TakeDamage(damage);

        if (combatLog != null)
        {
            combatLog.AddMessage($"{player.playerStats.CharacterName} attacks for {damage} damage!");
        }

        if (enemy.IsDead())
        {
            combatLog?.AddMessage("VOID DRAGON HAS BEEN DEFEATED!");
            turnIndicator.Text = "[center][color=#ffcc00]VICTORY![/color][/center]";
            return;
        }

        await ToSignal(GetTree().CreateTimer(1.0f), "timeout");
        EnemyTurn();
    }

    public async void PlayerDefend()
    {
        if (!playerTurn || player == null) return;

        playerTurn = false;
        UpdateTurnUI();

        player.playerStats.IsDefending = true;

        if (combatLog != null)
        {
            combatLog.AddMessage($"{player.playerStats.CharacterName} assumes a defensive stance!");
        }

        await ToSignal(GetTree().CreateTimer(1.0f), "timeout");
        EnemyTurn();
    }

    private async void EnemyTurn()
    {
        if (player == null || enemy == null) return;

        int damage = player.playerStats.IsDefending ? enemy.enemyStats.AttackPower / 2 : enemy.enemyStats.AttackPower;
        player.playerStats.TakeDamage(damage);

        if (combatLog != null)
        {
            combatLog.AddMessage($"{enemy.enemyStats.CharacterName} attacks for {damage} damage!");
        }

        if (player.playerStats.IsDead())
        {
            combatLog?.AddMessage("GERALT HAS FALLEN!");
            turnIndicator.Text = "[center][color=#ff3333]DEFEAT[/color][/center]";
            return;
        }

        await ToSignal(GetTree().CreateTimer(1.0f), "timeout");

        player.playerStats.IsDefending = false; 
        playerTurn = true;
        UpdateTurnUI();
    }
}