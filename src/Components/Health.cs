using System;

namespace DungeonDefender.Components;

public class Health : IHealth
{
	private int _currentHealth;

	public Health(int maximumHealth)
	{
		Require.MoreThanZero(maximumHealth);
		MaximumHealth = maximumHealth;
		CurrentHealth = maximumHealth;
	}

	public int MaximumHealth { get; }

	public int CurrentHealth
	{
		get => _currentHealth;
		private set
		{
			_currentHealth = value;
			CurrentHealthChanged?.Invoke(value);

			if (_currentHealth <= 0)
			{
				ZeroHealthReached?.Invoke();
			}
		}
	}

	public event Action<int> CurrentHealthChanged;
	public event Action ZeroHealthReached;

	public void ApplyDamage(int damage)
	{
		CurrentHealth -= damage;
	}
}
