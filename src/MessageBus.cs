using System;
using DungeonDefender.Enemies;

namespace DungeonDefender;

public static class MessageBus
{
	public static event Action<Enemy> EnemyDeath;

	public static void OnEnemyDeath(Enemy enemy)
	{
		EnemyDeath?.Invoke(enemy);
	}
}
