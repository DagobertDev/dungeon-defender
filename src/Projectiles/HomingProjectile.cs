using DungeonDefender.Enemies;
using Godot;

namespace DungeonDefender.Projectiles;

public partial class HomingProjectile : Node2D, IProjectile
{
	[Export]
	private Area2D _area;

	private int _damage;
	private Enemy _target;

	[Export(PropertyHint.Range, "0, 1000, or_greater")]
	public int Speed { get; private set; }

	public void FireAt(Tower tower, Enemy target)
	{
		_target = target;
		_damage = tower.Damage;
		Position = tower.Position;
		_target.TreeExiting += OnTargetLost;
		tower.GetParent().AddChild(this);
	}

	public override void _Ready()
	{
		Require.MoreThanZero(Speed);
		Require.NotNull(_target);
		Require.NotNull(_area);
		_area.AreaEntered += OnCollision;
	}

	public override void _Process(double delta)
	{
		GlobalPosition = GlobalPosition.MoveToward(_target.GlobalPosition, Speed * (float)delta);
		Rotation = GlobalPosition.AngleToPoint(_target.GlobalPosition);
	}

	private void OnCollision(Area2D area)
	{
		if (area.GetParent() == _target)
		{
			OnEnemyHit(_target);
		}
	}

	private void OnEnemyHit(Enemy enemy)
	{
		enemy.Health.ApplyDamage(_damage);
		QueueFree();
	}

	private void OnTargetLost()
	{
		QueueFree();
	}
}
