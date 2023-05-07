using System;
using Godot;

namespace DungeonDefender;

public partial class Enemy : PathFollow2D
{
	[Signal]
	public delegate void HealthChangedEventHandler(int health);

	private int _currentHealth;

	[Export(PropertyHint.Range, "0, 1000, or_greater")]
	public int Speed { get; set; }

	[Export(PropertyHint.Range, "0, 1000, or_greater")]
	public int MaximumHealth { get; set; }

	private int CurrentHealth
	{
		get => _currentHealth;
		set
		{
			_currentHealth = value;
			EmitSignal(SignalName.HealthChanged, CurrentHealth);
		}
	}

	public override void _Ready()
	{
		HealthChanged += health =>
		{
			if (health <= 0)
			{
				QueueFree();
			}
		};
		AddToGroup(Groups.Enemy);
		CurrentHealth = MaximumHealth;
	}

	public override void _Process(double delta)
	{
		Progress += Speed * (float)delta;

		if (Math.Abs(ProgressRatio - 1f) < float.Epsilon)
		{
			QueueFree();
		}
	}

	public void ApplyDamage(int damage)
	{
		CurrentHealth -= damage;
	}
}
