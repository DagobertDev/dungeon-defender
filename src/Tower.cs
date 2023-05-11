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
	public PackedScene Projectile { get; set; }

	public override void _Ready()
	{
		Require.MoreThanZero(Damage);
		Require.MoreThanZero(Range);
		Require.MoreThanZero(ReloadTime);
		Require.NotNull(Projectile);
		_reloadTimer = new Timer { OneShot = true };
		AddChild(_reloadTimer);
		_reloadTimer.Timeout += Enable;
	}

	public override void _Process(double delta)
	{
		var enemy = GetTree().GetNodesInGroup(Groups.Enemy).Cast<Enemy>().FirstOrDefault(InRange);

		if (enemy is null)
		{
			return;
		}

		var projectile = Projectile.Instantiate<Projectile>();
		projectile.FireAt(enemy, Damage);
		AddChild(projectile);
		_reloadTimer.Start(ReloadTime);
		Disable();
	}

	private void Enable()
	{
		SetProcess(true);
	}

	private void Disable()
	{
		SetProcess(false);
	}

	private bool InRange(Enemy enemy)
	{
		return enemy.GlobalPosition.DistanceSquaredTo(GlobalPosition) < Range * Range;
	}
}
