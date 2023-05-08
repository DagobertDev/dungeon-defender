using Godot;

namespace DungeonDefender;

public partial class Player : Node
{
	public HealthComponent Health { get; private set; }

	public override void _Ready()
	{
		Health = GetNode<HealthComponent>("Health");
		Health.ZeroHealthReached += GameOver;
	}

	public void OnEnemyCollision(Enemy enemy)
	{
		Health.ApplyDamage(1);
		enemy.QueueFree();
	}

	private void GameOver()
	{
		GetTree().Paused = true;
	}
}
