using Godot;

namespace DungeonDefender.Enemies;

public partial class Enemy : PathFollow2D
{
	[Export(PropertyHint.Range, "0, 1000, or_greater")]
	public int Speed { get; private set; }

	[Export(PropertyHint.Range, "0, 100, or_greater")]
	public int KillReward { get; private set; }

	public HealthComponent Health { get; private set; }

	public override void _Ready()
	{
		Require.MoreThanZero(Speed);
		Health = GetNode<HealthComponent>("Health");
		Health.ZeroHealthReached += OnDeath;
		AddToGroup(Groups.Enemy);
	}

	public override void _Process(double delta)
	{
		Progress += Speed * (float)delta;
	}

	private void OnDeath()
	{
		MessageBus.OnEnemyDeath(this);
		QueueFree();
	}
}
