using Entitas;
using UnityEngine;

public class MoveTowardsSystem : IExecuteSystem {

	readonly IGroup<GameEntity> _group;

	public MoveTowardsSystem(Contexts contexts) {
		_group = contexts.game.GetGroup(Matcher<GameEntity>.AllOf(GameMatcher.Position, GameMatcher.Rotation, GameMatcher.Speed, GameMatcher.TargetPosition));
	}

	public void Execute () {
		foreach (GameEntity e in _group.GetEntities()) {
			if (e.speed.speed != 0) {

				Vector3 curPosition = new Vector3(e.position.x, e.position.y, e.position.z);
				Vector3 targetPosition = new Vector3(e.targetPosition.x, e.targetPosition.y, e.targetPosition.z);

				float distanceTo = Vector3.Distance (curPosition, targetPosition);

				if (Mathf.Approximately(distanceTo, 0) | distanceTo <= e.speed.speed) {
					// we have arrived, stop moving
					e.ReplaceSpeed (0, e.speed.maxSpeed);
					e.isDestroyed = true;
				} else {
					//Vector3 curRot = new Vector3 (e.rotation.x, e.rotation.y, e.rotation.z);
					Vector3 newRot = (targetPosition - curPosition).normalized;

					e.ReplaceRotation (newRot.x, newRot.y, newRot.z);

					// this is the place to manipulate speed of the unit, for faster or slower speeds depending on position
					e.ReplaceSpeed (e.speed.maxSpeed, e.speed.maxSpeed);
				}
			}
		}
	}
}
