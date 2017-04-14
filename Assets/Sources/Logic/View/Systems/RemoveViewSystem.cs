using System.Collections.Generic;
using Entitas;
using UnityEngine;

public sealed class RemoveViewSystem : ReactiveSystem<GameEntity> {

	readonly GameContext _context;

	public RemoveViewSystem(Contexts contexts) : base(contexts.game) {
		_context = contexts.game;
	}

	protected override Collector<GameEntity> GetTrigger(IContext<GameEntity> context) {
		return new Collector<GameEntity>(context.GetGroup(GameMatcher.Destroyed), GroupEvent.AddedOrRemoved);
	}

	protected override bool Filter(GameEntity entity) {
		return entity.hasView && entity.isDestroyed;
	}

	protected override void Execute(List<GameEntity> entities) {
		foreach(GameEntity e in entities) {
			Object.Destroy(e.view.gameObject);
			e.RemoveView();
			_context.DestroyEntity (e);
		}
	}
}
