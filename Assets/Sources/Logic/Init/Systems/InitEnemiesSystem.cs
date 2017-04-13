using System.Collections;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class InitEnemiesSystem : IInitializeSystem {

	GameContext _context;

	public InitEnemiesSystem(Contexts contexts) {
		_context = contexts.game;
	}

	public void Initialize() {
		Vector3 newPosition = new Vector3 (Random.Range (-5, 5), 0, Random.Range (-5, 5));
		var e = _context.CreateEntity();
		e.isEnemyUnit = true;
		e.AddPosition(newPosition.x, newPosition.y, newPosition.z);
		e.AddRotation (0, 0, 0);
		e.AddSpeed (0.01f, 0.01f);
		e.AddTargetPosition (0, 0, 0);
		e.AddAsset ("MonsterGhost");
	}
}
