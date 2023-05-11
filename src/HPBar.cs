using Godot;

namespace DungeonDefender;

public partial class HPBar : ProgressBar
{
	[Export]
	public HealthComponent HealthComponent { get; private set; }

	public override void _Ready()
	{
		Require.NotNull(HealthComponent);
		MaxValue = HealthComponent.MaximumHealth;
		HealthComponent.CurrentHealthChanged += OnCurrentHealthChanged;
	}

	private void OnCurrentHealthChanged(int health)
	{
		Value = health;
	}
}
