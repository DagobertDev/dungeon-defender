using Godot;

namespace DungeonDefender;

public partial class Projectile : Node2D
{
	private int _damage;
	private Enemy _enemy;

	[Export(PropertyHint.Range, "0, 1000, or_greater")]
	public int Speed { get; set; }

	public override void _Process(double delta)
	{
		GlobalPosition = GlobalPosition.MoveToward(_enemy.GlobalPosition, Speed * (float)delta);

		if (GlobalPosition.DistanceSquaredTo(_enemy.GlobalPosition) > 10)
		{
			return;
		}

		_enemy.Health.ApplyDamage(_damage);
		QueueFree();
	}

	public void FireAt(Enemy enemy, int damage)
	{
		_enemy = enemy;
		_damage = damage;
	}
}
