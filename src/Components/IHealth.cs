using System;

namespace DungeonDefender.Components;

public interface IHealth
{
	int MaximumHealth { get; }

	int CurrentHealth { get; }
	event Action<int> CurrentHealthChanged;

	event Action ZeroHealthReached;

	void ApplyDamage(int damage);
}
