using Godot;

namespace DungeonDefender;

public partial class HealthComponent : Node
{
	[Signal]
	public delegate void CurrentHealthChangedEventHandler(int health);

	[Signal]
	public delegate void ZeroHealthReachedEventHandler();

	private int _currentHealth;

	[Export(PropertyHint.Range, "0, 1000, or_greater")]
	public int MaximumHealth { get; private set; }

	private int CurrentHealth
	{
		get => _currentHealth;
		set
		{
			_currentHealth = value;
			EmitSignal(SignalName.CurrentHealthChanged, CurrentHealth);

			if (CurrentHealth <= 0)
			{
				EmitSignal(SignalName.ZeroHealthReached);
			}
		}
	}

	public override void _Ready()
	{
		Require.MoreThanZero(MaximumHealth);
		CurrentHealth = MaximumHealth;
	}

	public void ApplyDamage(int damage)
	{
		CurrentHealth -= damage;
	}
}
