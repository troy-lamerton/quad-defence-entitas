using Entitas;

public class InitEnemiesSystem : ICleanupSystem {

	GameContext _context;

	public InitEnemiesSystem(Contexts contexts) {
		_context = contexts.game;
	}

	public void Cleanup () {
		foreach (GameEntity e in _context.GetEntities()) {
			if (e.isDestroyed) {
				_context.DestroyEntity (e);
			}
		}
	}
}
