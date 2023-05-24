using System;
using DungeonDefender.Enemies;

namespace DungeonDefender;

public static class MessageBus
{
	public static event Action<IEnemy> EnemyDeath;

	public static void OnEnemyDeath(IEnemy enemy)
	{
		EnemyDeath?.Invoke(enemy);
	}
}
