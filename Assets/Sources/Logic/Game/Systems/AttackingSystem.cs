using Entitas;
using UnityEngine;

public sealed class AttackingSystem : IExecuteSystem, ICleanupSystem {

	readonly GameContext _context;
	readonly IGroup<GameEntity> _enemyUnits;
	const float ATTACK_RADIUS = 2.0f;

	public AttackingSystem(Contexts contexts) {
		_context = contexts.game;
		_enemyUnits = contexts.game.GetGroup (Matcher<GameEntity>.AllOf (GameMatcher.EnemyUnit, GameMatcher.Position).NoneOf(GameMatcher.Destroyed));
	}

	public void Execute() {
		GameEntity player = _context.playerEntity;
		if (player.hasAttacking) {
			var atk = player.attacking;
			if (atk.state == atk.initState) {
				int dir = atk.direction;
				// ATTACK!!!

				// add the damagey segment and the particle effect
				GameEntity e = _context.CreateEntity ();
				e.AddAsset ("Quarter", (float)atk.initState);

				Vector3 rotNew = Vector3.zero;

				bool isVertical = atk.direction % 2 == 0;
				if (isVertical) {
					rotNew = (dir == 0) ? Vector3.forward : Vector3.back; // 0 is fwd, 2 is back
				} else {
					rotNew = (dir == 1) ? Vector3.right : Vector3.left; // 1 is right, 3 is left
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
						Debug.Log (enemyDistance);
						if (enemyDistance <= ATTACK_RADIUS) {
							// blast that sucker
							enemy.isDestroyed = true;
						}
					}
					// and same signs on x and z
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
