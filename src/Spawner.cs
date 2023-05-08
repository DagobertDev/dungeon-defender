using Godot;

namespace DungeonDefender;

public partial class Spawner : Node
{
	[Export]
	public PackedScene Enemy { get; set; }

	[Export]
	public Lane Lane { get; set; }

	public void SpawnEnemy()
	{
		var enemy = Enemy.Instantiate<Enemy>();
		Lane.AddEnemy(enemy);
	}
}
