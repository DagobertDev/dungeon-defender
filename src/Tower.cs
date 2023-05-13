using System.Linq;
using Godot;

namespace DungeonDefender;

public partial class Tower : Sprite2D
{
	private Timer _reloadTimer;

	[Export(PropertyHint.Range, "0, 100, or_greater")]
	public int Damage { get; set; }

	[Export(PropertyHint.Range, "0, 1000, or_greater")]
	public int Range { get; set; }

	[Export(PropertyHint.Range, "0, 1000, or_greater")]
	public int ReloadTime { get; set; }

	[Export]
	public PackedScene Projectile { get; private set; }

	[Export]
	public CollisionObject2D CollisionBody { get; private set; }

	public override void _Ready()
	{
		Require.MoreThanZero(Damage);
		Require.MoreThanZero(Range);
		Require.MoreThanZero(ReloadTime);
		Require.NotNull(Projectile);
		Require.NotNull(CollisionBody);
		_reloadTimer = new Timer { OneShot = true };
		AddChild(_reloadTimer);
		_reloadTimer.Timeout += Enable;
	}

	public override void _Process(double delta)
	{
		var enemy = GetTree().GetNodesInGroup(Groups.Enemy).Cast<Enemy>().FirstOrDefault(IsInRange);

		if (enemy is not null)
		{
			FireAt(enemy);
		}
	}

	private void Enable()
	{
		SetProcess(true);
	}

	private void Disable()
	{
		SetProcess(false);
	}

	private void FireAt(Enemy enemy)
	{
		Rotation = GlobalPosition.AngleToPoint(enemy.GlobalPosition);
		var projectile = Projectile.Instantiate<Projectile>();
		projectile.GlobalPosition = GlobalPosition;
		projectile.FireAt(enemy, Damage);
		GetParent().AddChild(projectile);
		_reloadTimer.Start(ReloadTime);
		Disable();
	}

	private bool IsInRange(Enemy enemy)
	{
		return enemy.GlobalPosition.DistanceSquaredTo(GlobalPosition) < Range * Range;
	}
}
