using Entitas;
using UnityEngine;

public class MoveSystem : IExecuteSystem {

	readonly IGroup<GameEntity> _group;

	public MoveSystem(Contexts contexts) {
		_group = contexts.game.GetGroup(Matcher<GameEntity>.AllOf(GameMatcher.Position, GameMatcher.Velocity));
	}

	public bool Filter (GameEntity entity) {
		return entity.hasPosition & entity.hasVelocity;
	}

	public void Execute () {
		foreach (GameEntity e in _group.GetEntities()) {
			var curPosition = new Vector3(e.position.x, e.position.y, e.position.z);
			var newPos = curPosition + e.velocity.value;
			e.ReplacePosition (newPos.x, newPos.y, newPos.z);
		}
	}
}
