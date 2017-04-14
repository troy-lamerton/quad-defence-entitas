using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class MoveTowardsSystem : ReactiveSystem<GameEntity> {

	public MoveTowardsSystem(Contexts contexts) : base(contexts.game) {
	}

	protected override Collector<GameEntity> GetTrigger(IContext<GameEntity> context) {
		return context.CreateCollector(Matcher<GameEntity>.AllOf(GameMatcher.TargetPosition, GameMatcher.Position), GroupEvent.Added);
	}

	protected override bool Filter(GameEntity e) {
		return e.hasTargetPosition && e.hasPosition & e.hasRotation & e.hasSpeed;
	}


	protected override void Execute (List<GameEntity> entities) {
		foreach (GameEntity e in entities) {
			float s = e.speed.speed;
			float maxS = e.speed.maxSpeed;

			Vector3 curPosition = new Vector3(e.position.x, e.position.y, e.position.z);
			Vector3 targetPosition = new Vector3(e.targetPosition.x, e.targetPosition.y, e.targetPosition.z);

			float distanceTo = Vector3.Distance (curPosition, targetPosition);

			if (Mathf.Approximately(distanceTo, 0.2f) | distanceTo <= s) {
				// we have arrived, stop moving
				e.isDestroyed = true;
			} else {
				if (s != maxS) {
					// this is the place to manipulate speed of the unit, for faster or slower speeds depending on position
					e.ReplaceSpeed (maxS, maxS);
				}

				Vector3 newRot = (targetPosition - curPosition).normalized;
			
				if (newRot.y != e.rotation.y | newRot.z != e.rotation.z) {
					e.ReplaceRotation (newRot.x, newRot.y, newRot.z);
				}
			}
		}
	}
}
