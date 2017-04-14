using System.Collections.Generic;
using Entitas;
using UnityEngine;

//public sealed class RenderPositionSystem : ReactiveSystem<GameEntity> {
//
//	public RenderPositionSystem(Contexts contexts) : base(contexts.game) {
//	}
//
//	protected override Collector<GameEntity> GetTrigger(IContext<GameEntity> context) {
//		return context.CreateCollector(
//			GameMatcher.Position,
//			GroupEvent.Added
//		);
//	}
//
//	protected override bool Filter(GameEntity entity) {
//		return entity.hasView & entity.hasPosition;
//	}
//
//	protected override void Execute(List<GameEntity> entities) {
//		foreach(GameEntity e in entities) {
//			e.view.gameObject.transform.position = new Vector3(e.position.x, e.position.y, e.position.z);
//		}
//	}
//}
public sealed class RenderPositionSystem : IExecuteSystem {

	readonly IGroup<GameEntity> _group;

	public RenderPositionSystem(Contexts contexts) {
		_group = contexts.game.GetGroup (Matcher<GameEntity>.AllOf (GameMatcher.Position, GameMatcher.View));
	}

	public bool Filter(GameEntity entity) {
		return entity.hasView & entity.hasPosition;
	}

	public void Execute() {
		foreach(GameEntity e in _group.GetEntities()) {
			e.view.gameObject.transform.position = new Vector3(e.position.x, e.position.y, e.position.z);
		}
	}
}
