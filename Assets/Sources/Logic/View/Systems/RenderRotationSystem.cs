using System.Collections.Generic;
using Entitas;
using UnityEngine;

public sealed class RenderRotationSystem : ReactiveSystem<GameEntity> {

	public RenderRotationSystem(Contexts contexts) : base(contexts.game) {
	}

	protected override Collector<GameEntity> GetTrigger(IContext<GameEntity> context) {
		return context.CreateCollector(GameMatcher.Rotation);
	}

	protected override bool Filter(GameEntity entity) {
		return entity.hasView & entity.hasRotation;
	}

	protected override void Execute(List<GameEntity> entities) {
		foreach(GameEntity e in entities) {
			var rot = e.rotation;
			Vector3 rotVector = new Vector3(rot.x, rot.y, rot.z);
			e.view.gameObject.transform.forward = (rotVector);
		}
	}
}
