using Godot;

namespace DungeonDefender;

public partial class Player : Node
{
	[Export]
	private HealthComponent _health;

	[Export]
	private Lane _lane;

	public override void _Ready()
	{
		Require.NotNull(_lane);
		Require.NotNull(_health);
		_lane.EnemyReachedEnd += OnEnemyCollision;
		_health.ZeroHealthReached += GameOver;
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
}
