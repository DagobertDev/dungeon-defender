using DungeonDefender.Enemies;

namespace DungeonDefender;

public interface IProjectile
{
	void FireAt(Tower tower, Enemy target);
}
