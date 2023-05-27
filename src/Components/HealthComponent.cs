using System;
using Godot;

namespace DungeonDefender.Components;

public partial class HealthComponent : Node, IHealth
{
	private int _currentHealth;

	public event Action<int> CurrentHealthChanged;
	public event Action ZeroHealthReached;

	[Export(PropertyHint.Range, "0, 1000, or_greater")]
	public int MaximumHealth { get; private set; }

	public int CurrentHealth
	{
		get => _currentHealth;
		private set
		{
			_currentHealth = value;
			CurrentHealthChanged?.Invoke(value);

			if (CurrentHealth <= 0)
			{
				ZeroHealthReached?.Invoke();
			}
		}
	}

	public void ApplyDamage(int damage)
	{
		CurrentHealth -= damage;
	}

	public override void _Ready()
	{
		Require.MoreThanZero(MaximumHealth);
		CurrentHealth = MaximumHealth;
	}
}
