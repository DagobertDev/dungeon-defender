using Godot;

namespace DungeonDefender;

public partial class HPBar : ProgressBar
{
	[Export]
	public Enemy Parent;

	public override void _Ready()
	{
		MaxValue = Parent.MaximumHealth;
		Parent.HealthChanged += OnParentHealthChanged;
	}

	private void OnParentHealthChanged(int health)
	{
		Value = health;
	}
}
