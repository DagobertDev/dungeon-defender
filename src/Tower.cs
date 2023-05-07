using System.Linq;
using Godot;

namespace DungeonDefender;

public partial class Tower : Sprite2D
{
	private Timer _reloadTimer;

	[Export(PropertyHint.Range, "0, 100, or_greater")]
	public int Damage { get; set; }

	[Export(PropertyHint.Range, "0, 1000, or_greater")]
	public int ReloadTime { get; set; }

	public override void _Ready()
	{
		_reloadTimer = new Timer { OneShot = true };
		AddChild(_reloadTimer);
		_reloadTimer.Timeout += Enable;
	}

	public override void _Process(double delta)
	{
		var enemy = GetTree().GetNodesInGroup(Groups.Enemy).Cast<Enemy>().FirstOrDefault();

		if (enemy is null)
		{
			return;
		}

		enemy.ApplyDamage(Damage);
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
}
