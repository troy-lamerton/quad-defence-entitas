using System.Collections;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class InitEnemiesSystem : IInitializeSystem, ICleanupSystem {

	GameContext _context;

	public InitEnemiesSystem(Contexts contexts) {
		_context = contexts.game;
	}

	public void Initialize() {
		Vector3 newPosition = RandomPosition ();
		CreateEnemy (_context, newPosition);
	}

	public Vector3 RandomPosition(int min = -5, int max = 5) {
		return new Vector3 (Random.Range (-5, 5), 0, Random.Range (-5, 5));
	}

	public void CreateEnemy(GameContext game, Vector3 newPosition) {
		GameEntity e = game.CreateEntity();
		e.isEnemyUnit = true;
		e.AddPosition(newPosition.x, newPosition.y, newPosition.z);
		e.AddRotation (0, 0, 0);
		e.AddSpeed (0.01f, 0.01f);
		e.AddTargetPosition (0, 0, 0);
		e.AddAsset ("MonsterGhost", -1f);
	}

	public void Cleanup () {
		foreach (GameEntity e in _context.GetEntities()) {
			if (e.isDestroyed) {
				_context.DestroyEntity (e);
			}
		}
	}
}
