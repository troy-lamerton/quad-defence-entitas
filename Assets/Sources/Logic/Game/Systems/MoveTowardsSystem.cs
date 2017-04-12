using Entitas;
using UnityEngine;

public class MoveTowardsSystem : IExecuteSystem {

	readonly IGroup<GameEntity> _group;

	public MoveTowardsSystem(Contexts contexts) {
		_group = contexts.game.GetGroup(Matcher<GameEntity>.AllOf(GameMatcher.Position, GameMatcher.Velocity));
	}

	public void Execute () {
		foreach (GameEntity e in _group.GetEntities()) {
			// update velocity and rotation so that the values will move this entity towards the target
			Vector3 curPosition = new Vector3(e.position.x, e.position.y, e.position.z);
			Vector3 targetPosition = new Vector3(e.targetPosition.x, e.targetPosition.y, e.targetPosition.z);
			Vector3 newVelocity = Vector3.MoveTowards(curPosition, targetPosition, 0.001f);
			Debug.Log (newVelocity);
			e.ReplaceVelocity (newVelocity);
		}
	}
}
