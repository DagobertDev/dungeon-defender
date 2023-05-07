using Godot;

namespace DungeonDefender;

public partial class Player : Node
{
	public HealthComponent Health { get; private set; }

	public override void _Ready()
	{
		Health = GetNode<HealthComponent>("Health");
	}

	public void OnPossibleEnemyReached(Area2D area)
	{
		var enemy = area.GetParent();

		if (!enemy.IsInGroup(Groups.Enemy))
		{
			return;
		}

		Health.ApplyDamage(1);
		enemy.QueueFree();
	}
}
