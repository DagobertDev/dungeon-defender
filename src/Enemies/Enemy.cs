using System;
using DungeonDefender.Components;
using Godot;

namespace DungeonDefender.Enemies;

public partial class Enemy : PathFollow2D, IEnemy
{
	[Export]
	private HealthWrapper _health;

	[Export(PropertyHint.Range, "0, 1000, or_greater")]
	public int Speed { get; private set; }

	[Export(PropertyHint.Range, "0, 100, or_greater")]
	public int KillReward { get; private set; }

	public IHealth Health => _health.Value;

	public void Destroy()
	{
		Destroyed?.Invoke();
		QueueFree();
	}

	public event Action Destroyed;

	public override void _Ready()
	{
		Require.NotNull(Health);
		Require.MoreThanZero(Speed);
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
		Destroy();
	}
}
