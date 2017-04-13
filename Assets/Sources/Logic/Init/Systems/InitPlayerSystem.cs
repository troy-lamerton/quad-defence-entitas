using System.Collections;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class InitPlayerSystem : IInitializeSystem {

	GameContext _context;

	public InitPlayerSystem(Contexts contexts) {
		_context = contexts.game;
	}

	public void Initialize() {
		var e = _context.CreateEntity();
		e.isPlayer = true;
		e.AddPosition (0, 0, 0);
		// e.isTargetPosition = true; see TODO in GameSystems
		e.AddRotation (0, 0, 0);
		e.AddAsset ("Player");
	}
}
