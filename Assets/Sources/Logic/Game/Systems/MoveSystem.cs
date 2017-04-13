using Entitas;
using UnityEngine;

public class MoveSystem : IExecuteSystem {

	readonly IGroup<GameEntity> _group;

	public MoveSystem(Contexts contexts) {
		_group = contexts.game.GetGroup(Matcher<GameEntity>.AllOf(GameMatcher.Position, GameMatcher.Rotation, GameMatcher.Speed));
	}

	public bool Filter (GameEntity entity) {
		return entity.hasPosition & entity.hasRotation & entity.hasSpeed;
	}

	public void Execute () {
		foreach (GameEntity e in _group.GetEntities()) {
			Vector3 curPosition = new Vector3(e.position.x, e.position.y, e.position.z);
			Vector3 curRotation = new Vector3(e.rotation.x, e.rotation.y, e.rotation.z);

			Vector3 newPos = curPosition + (curRotation * e.speed.speed);

			e.ReplacePosition (newPos.x, newPos.y, newPos.z);
		}
	}
}
