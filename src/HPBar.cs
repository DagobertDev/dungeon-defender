using DungeonDefender.Components;
using Godot;

namespace DungeonDefender;

public partial class HPBar : ProgressBar
{
	[Export]
	private HealthWrapper _healthWrapper;

	public override void _Ready()
	{
		Require.NotNull(_healthWrapper);
		var health = _healthWrapper.Value;
		MaxValue = health.MaximumHealth;
		health.CurrentHealthChanged += OnCurrentHealthChanged;
		Value = health.CurrentHealth;
	}

	private void OnCurrentHealthChanged(int health)
	{
		Value = health;
	}
}
