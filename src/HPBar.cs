using Godot;

namespace DungeonDefender;

public partial class HPBar : ProgressBar
{
	[Export]
	public HealthComponent HealthComponent;

	public override void _Ready()
	{
		MaxValue = HealthComponent.MaximumHealth;
		HealthComponent.CurrentHealthChanged += OnCurrentHealthChanged;
	}

	private void OnCurrentHealthChanged(int health)
	{
		Value = health;
	}
}
