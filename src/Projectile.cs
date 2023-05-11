using Godot;

namespace DungeonDefender;

public partial class Projectile : Node2D
{
	private int _damage;
	private Enemy _target;

	[Export(PropertyHint.Range, "0, 1000, or_greater")]
	public int Speed { get; private set; }

	public override void _Ready()
	{
		Require.MoreThanZero(Speed);
		Require.NotNull(_target);
	}

	public override void _Process(double delta)
	{
		GlobalPosition = GlobalPosition.MoveToward(_target.GlobalPosition, Speed * (float)delta);

		if (GlobalPosition.DistanceSquaredTo(_target.GlobalPosition) > 10)
		{
			Rotation = GlobalPosition.AngleToPoint(_target.GlobalPosition);
			return;
		}

		_target.Health.ApplyDamage(_damage);
		QueueFree();
	}

	public void FireAt(Enemy target, int damage)
	{
		_target = target;
		_damage = damage;
		_target.TreeExiting += OnTargetLost;
	}

	private void OnTargetLost()
	{
		QueueFree();
	}
}
