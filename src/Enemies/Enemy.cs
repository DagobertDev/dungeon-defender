using Godot;

namespace DungeonDefender.Enemies;

public partial class Enemy : PathFollow2D
{
	[Export(PropertyHint.Range, "0, 1000, or_greater")]
	public int Speed { get; private set; }

	public HealthComponent Health { get; private set; }

	public override void _Ready()
	{
		Require.MoreThanZero(Speed);
		Health = GetNode<HealthComponent>("Health");
		Health.ZeroHealthReached += QueueFree;
		AddToGroup(Groups.Enemy);
	}

	public override void _Process(double delta)
	{
		Progress += Speed * (float)delta;
	}
}
