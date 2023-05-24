using DungeonDefender.Enemies;
using Godot;

namespace DungeonDefender.Projectiles;

public partial class HomingProjectile : Node2D, IProjectile
{
	[Export]
	private Area2D _area;

	private int _damage;
	private IEnemy _target;

	[Export(PropertyHint.Range, "0, 1000, or_greater")]
	public int Speed { get; private set; }

	public void FireAt(Tower tower, IEnemy target)
	{
		_target = target;
		_damage = tower.Damage;
		Position = tower.Position;
		_target.Destroyed += OnTargetLost;
		tower.GetParent().AddChild(this);
	}

	public override void _Ready()
	{
		Require.MoreThanZero(Speed);
		Require.NotNull(_target);
		Require.NotNull(_area);
		_area.AreaEntered += OnCollision;
	}

	public override void _ExitTree()
	{
		_target.Destroyed -= OnTargetLost;
		_area.AreaEntered -= OnCollision;
	}

	public override void _Process(double delta)
	{
		GlobalPosition = GlobalPosition.MoveToward(_target.Position, Speed * (float)delta);
		Rotation = GlobalPosition.AngleToPoint(_target.Position);
	}

	private void OnCollision(Area2D area)
	{
		if (area.GetParent() == _target)
		{
			OnEnemyHit(_target);
		}
	}

	private void OnEnemyHit(IEnemy enemy)
	{
		enemy.Health.ApplyDamage(_damage);
		QueueFree();
	}

	private void OnTargetLost()
	{
		QueueFree();
	}
}
