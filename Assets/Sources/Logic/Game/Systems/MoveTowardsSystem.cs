using Entitas;
using UnityEngine;

public class MoveTowardsSystem : IExecuteSystem {

	readonly IGroup<GameEntity> _group;

	public MoveTowardsSystem(Contexts contexts) {
		_group = contexts.game.GetGroup(Matcher<GameEntity>.AllOf(GameMatcher.Position, GameMatcher.Rotation, GameMatcher.Speed));
	}

	public void Execute () {
		foreach (GameEntity e in _group.GetEntities()) {
			Vector3 curPosition = new Vector3(e.position.x, e.position.y, e.position.z);
			Vector3 targetPosition = new Vector3(e.targetPosition.x, e.targetPosition.y, e.targetPosition.z);

//			Vector3 curRot = new Vector3 (e.rotation.x, e.rotation.y, e.rotation.z);
			Vector3 newRot = (targetPosition - curPosition).normalized;

			e.ReplaceRotation (newRot.x, newRot.y, newRot.z);
			// this is the place to manipulate speed of the unit, for faster or slower speeds depending on position
			// how? replace speed to a value like e.speed.maxSpeed

			float distanceTo = Vector3.Distance (curPosition, targetPosition);
			if (e.speed.speed != 0 & distanceTo <= e.speed.speed) {
				// we have arrived, stop moving
				e.ReplaceSpeed (0, e.speed.maxSpeed);
			}
		}
	}
}
