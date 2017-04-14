using Entitas;
using UnityEngine;

public class MoveSystem : IExecuteSystem {

	readonly IGroup<GameEntity> _group;

	public MoveSystem(Contexts contexts) {
		_group = contexts.game.GetGroup(Matcher<GameEntity>.AllOf(GameMatcher.Position, GameMatcher.Rotation, GameMatcher.Speed));
	}

	public void Execute () {
		foreach (GameEntity e in _group.GetEntities()) {
			if (e.speed.speed != 0) {
				e.ReplacePosition (
					e.position.x + e.rotation.x * e.speed.speed,
					e.position.y + e.rotation.y * e.speed.speed,
					e.position.z + e.rotation.z * e.speed.speed
				);
			}
		}
	}
}
