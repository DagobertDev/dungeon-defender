using DungeonDefender.Enemies;
using Godot;

namespace DungeonDefender.Projectiles;

public partial class Arrow : Node2D, IProjectile
{
	[Export]
	private Area2D _area;

	private int _damage;
	private Vector2 _targetPosition;

	[Export(PropertyHint.Range, "0, 1000, or_greater")]
	public int Speed { get; private set; }

	public void FireAt(Tower tower, IEnemy target)
	{
		_targetPosition = target.Position;
		_damage = tower.Damage;
		Position = tower.Position;
		tower.GetParent().AddChild(this);
	}

	public override void _Ready()
	{
		Require.MoreThanZero(Speed);
		Require.NotNull(_targetPosition);
		Require.NotNull(_area);
		_area.AreaEntered += OnCollision;
	}

	public override void _Process(double delta)
	{
		Position = Position.MoveToward(_targetPosition, Speed * (float)delta);
		Rotation = Position.AngleToPoint(_targetPosition);

		if (Position.DistanceSquaredTo(_targetPosition) < 1)
		{
			QueueFree();
		}
	}

	private void OnCollision(Area2D area)
	{
		if (area.GetParent() is IEnemy enemy)
		{
			OnEnemyHit(enemy);
		}
	}

	private void OnEnemyHit(IEnemy enemy)
	{
		enemy.Health.ApplyDamage(_damage);
		QueueFree();
	}
}
