using DungeonDefender.Components;
using Godot;

namespace DungeonDefender;

public partial class HPBar : ProgressBar
{
	[Export]
	private HealthComponent _healthComponent;

	public override void _Ready()
	{
		Require.NotNull(_healthComponent);
		MaxValue = _healthComponent.MaximumHealth;
		_healthComponent.CurrentHealthChanged += OnCurrentHealthChanged;
	}

	private void OnCurrentHealthChanged(int health)
	{
		Value = health;
	}
}
