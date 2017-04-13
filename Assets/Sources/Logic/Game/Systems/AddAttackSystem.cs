using System;
using System.Collections.Generic;
using Entitas;

public sealed class AddAttackSystem : ReactiveSystem<InputEntity> {

	readonly GameContext _context;

	public AddAttackSystem(Contexts contexts) : base(contexts.input) {
		_context = contexts.game;
	}


	protected override Collector<InputEntity> GetTrigger(IContext<InputEntity> context) {
		return context.CreateCollector(InputMatcher.Direction, GroupEvent.AddedOrRemoved);
	}

	protected override bool Filter(InputEntity entity) {
		return entity.hasDirection;
	}

	protected override void Execute(List<InputEntity> entities) {
		var playerEntity = _context.playerEntity;
		if (!playerEntity.hasAttacking) {
			// expect entities to be list of 1 input entity
			foreach (InputEntity e in entities) {
				// can assume:
				// direction values are > 0
				// and x != y
				float x = e.direction.x;
				float y = e.direction.y;
				
				bool isVertical = Math.Abs(y) > Math.Abs(x);

				int attackDirection = 0;

				if (isVertical) {
					attackDirection = (y > 0) ? 0 : 2; // 0 is up, 2 is down
				} else {
					attackDirection = (x > 0) ? 1 : 3; // 1 is right, 3 is left
				}

				playerEntity.AddAttacking (attackDirection, 30, 30);
			}
		}
	}
}
