using Godot;

namespace DungeonDefender;

public partial class Spawner : Node
{
	[Export]
	public PackedScene Enemy { get; set; }

	[Export]
	public Node Path { get; set; }

	public void SpawnEnemy()
	{
		var enemy = Enemy.Instantiate();
		Path.AddChild(enemy);
	}
}
