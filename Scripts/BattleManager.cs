using Godot;

public partial class BattleManager : Node
{
    public enum BattleState
    {
        PlayerTurn,
        EnemyTurn,
        Busy,
        Victory,
        Defeat
    }

    private BattleState currentState;
    private Player player;
    private Enemy enemy;

    public override void _Ready()
    {
        player = GetNode<Player>("../Player");
        enemy = GetNode<Enemy>("../Enemy");

        currentState = BattleState.PlayerTurn;
    }

    public void PlayerAttack()
    {
        if (currentState != BattleState.PlayerTurn)
            return;

        enemy.TakeDamage(3);

        currentState = BattleState.EnemyTurn;

        EnemyTurn();
    }

    private async void EnemyTurn()
    {
        await ToSignal(GetTree().CreateTimer(1.0f), "timeout");

        player.TakeDamage(2);

        currentState = BattleState.PlayerTurn;
    }
    public void OnAttackPressed()
    {
        GD.Print("Player attacks!");
    }
}