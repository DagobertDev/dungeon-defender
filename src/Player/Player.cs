using DungeonDefender.Components;
using DungeonDefender.Enemies;
using Godot;

namespace DungeonDefender.Player;

public partial class Player : Node
{
	[Export]
	private GoldComponent _gold;

	[Export]
	private HealthWrapper _healthWrapper;

	[Export]
	private Lane _lane;

	private IHealth Health => _healthWrapper.Value;

	public override void _Ready()
	{
		Require.NotNull(_gold);
		Require.NotNull(_healthWrapper);
		Require.NotNull(_lane);
		Health.ZeroHealthReached += GameOver;
		_lane.EnemyReachedEnd += OnEnemyCollision;
		MessageBus.EnemyDeath += OnEnemyDeath;
	}

	public override void _ExitTree()
	{
		MessageBus.EnemyDeath -= OnEnemyDeath;
	}

	public void OnEnemyCollision(IEnemy enemy)
	{
		Health.ApplyDamage(1);
		enemy.Destroy();
	}

	private void GameOver()
	{
		GetTree().Paused = true;
	}

	private void OnEnemyDeath(IEnemy enemy)
	{
		_gold.CurrentGold += enemy.KillReward;
	}
}
