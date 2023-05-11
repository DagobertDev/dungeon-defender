using Godot;

namespace DungeonDefender;

public partial class Spawner : Node
{
	[Export]
	public PackedScene Enemy { get; private set; }

	[Export]
	public Lane Lane { get; private set; }

	public override void _Ready()
	{
		Require.NotNull(Enemy);
		Require.NotNull(Lane);
	}

	public void SpawnEnemy()
	{
		var enemy = Enemy.Instantiate<Enemy>();
		Lane.AddEnemy(enemy);
	}
}
