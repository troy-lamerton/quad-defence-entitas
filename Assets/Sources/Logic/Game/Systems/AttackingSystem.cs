using Entitas;
using UnityEngine;

public sealed class AttackingSystem : IExecuteSystem, ICleanupSystem {

	readonly GameContext _context;
	readonly IGroup<GameEntity> _enemyUnits;
	readonly IGroup<GameEntity> _otherUnits;
	const float ATTACK_RADIUS = 2.15f;

	public AttackingSystem(Contexts contexts) {
		_context = contexts.game;
		_enemyUnits = contexts.game.GetGroup (Matcher<GameEntity>.AllOf (GameMatcher.EnemyUnit, GameMatcher.Position).NoneOf(GameMatcher.Destroyed));
		_otherUnits = contexts.game.GetGroup (Matcher<GameEntity>.AllOf (GameMatcher.Asset).NoneOf (GameMatcher.EnemyUnit, GameMatcher.View));
	}

	public void Execute() {
		GameEntity player = _context.playerEntity;
		if (player.hasAttacking) {
			var atk = player.attacking;
			if (atk.state == atk.initState) {
				foreach (GameEntity shite in _otherUnits.GetEntities()) {
					_context.DestroyEntity (shite);
				}
				// ATTACK!!!

				// add the damagey segment and any FX
				GameEntity e = _context.CreateEntity ();

				e.AddAsset ("Quarter", (float)atk.initState);

				Vector3 rotNew = Vector3.zero;

				bool isVertical = atk.direction % 2 == 0;
				if (isVertical) {
					rotNew = (atk.direction == 0) ? Vector3.forward : Vector3.back; // 0 is fwd, 2 is back
				} else {
					rotNew = (atk.direction == 1) ? Vector3.right : Vector3.left; // 1 is right, 3 is left
				}

				e.AddRotation (rotNew.x, rotNew.y, rotNew.z);

				player.ReplaceRotation (rotNew.x, rotNew.y, rotNew.z);

				// damage enemies inside the quarter
				foreach (GameEntity enemy in _enemyUnits.GetEntities()) {
					// if enemy inside this segment, gtfo! isDestroyed = true mofo
					float eX = enemy.position.x;
					float eZ = enemy.position.z;
					Vector2 enemyPosition2D = new Vector2 (eX, eZ);
					float angleBetween = Vector2.Angle (enemyPosition2D.normalized, new Vector2 (rotNew.x, rotNew.z).normalized);
					if (angleBetween <= 45f) {
						float enemyDistance = enemyPosition2D.magnitude;
						if (enemyDistance <= ATTACK_RADIUS) {
							// blast that sucker
							enemy.isDestroyed = true;
						}
					}
				}

			} else if (atk.state > 0) {
				// cooling down...
			}

			player.ReplaceAttacking (atk.direction, atk.state - 1, atk.initState);
		}
	}

	public void Cleanup() {
		GameEntity player = _context.playerEntity;
		if (player.hasAttacking && player.attacking.state <= 0) {
			player.RemoveAttacking ();
		}
	}
}
