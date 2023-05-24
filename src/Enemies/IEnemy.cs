using System;
using Godot;

namespace DungeonDefender.Enemies;

public interface IEnemy
{
	int KillReward { get; }
	HealthComponent Health { get; }
	Vector2 Position { get; }
	event Action Destroyed;
	void Destroy();
}
