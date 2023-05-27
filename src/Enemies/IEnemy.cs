using System;
using DungeonDefender.Components;
using Godot;

namespace DungeonDefender.Enemies;

public interface IEnemy
{
	int KillReward { get; }
	IHealth Health { get; }
	Vector2 Position { get; }
	event Action Destroyed;
	void Destroy();
}
