using DungeonDefender.Enemies;
using Godot;

namespace DungeonDefender.Player;

public partial class Player : Node
{
	[Export]
	private GoldComponent _gold;

	[Export]
	private HealthComponent _health;

	[Export]
	private Lane _lane;

	public override void _Ready()
	{
		Require.NotNull(_gold);
		Require.NotNull(_health);
		Require.NotNull(_lane);
		_health.ZeroHealthReached += GameOver;
		_lane.EnemyReachedEnd += OnEnemyCollision;
		MessageBus.EnemyDeath += OnEnemyDeath;
	}

	public override void _ExitTree()
	{
		MessageBus.EnemyDeath -= OnEnemyDeath;
	}

	public void OnEnemyCollision(Enemy enemy)
	{
		_health.ApplyDamage(1);
		enemy.QueueFree();
	}

	private void GameOver()
	{
		GetTree().Paused = true;
	}

	private void OnEnemyDeath(Enemy enemy)
	{
		_gold.CurrentGold += enemy.KillReward;
	}
}
