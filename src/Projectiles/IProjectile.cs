﻿using DungeonDefender.Enemies;

namespace DungeonDefender.Projectiles;

public interface IProjectile
{
	void FireAt(Tower tower, IEnemy target);
}
