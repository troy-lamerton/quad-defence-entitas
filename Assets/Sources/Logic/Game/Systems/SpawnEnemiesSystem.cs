using Entitas;
using UnityEngine;

public class SpawnEnemiesSystem : IExecuteSystem {

	GameContext _context;

	float nextWaveAt = 0f;
	int unitsToSpawn = 30;
	int waveNumber = 1;

	public SpawnEnemiesSystem(Contexts contexts) {
		_context = contexts.game;
	}
		
	public Vector3 RandomPositionOnCircle(float radius = 5.5f) {
		Vector2 randomPoint = Random.insideUnitCircle.normalized;
		return new Vector3 (randomPoint.x * radius, 0, randomPoint.y * radius);
	}

	public void CreateEnemy(GameContext game, float speedMultiplier = 1f) {
		Vector3 newPosition = RandomPositionOnCircle ();
		float speed = 0.0012f * (float)Random.Range (4, 6) * speedMultiplier;

		GameEntity e = game.CreateEntity();
		e.isEnemyUnit = true;
		e.AddPosition(newPosition.x, newPosition.y, newPosition.z);
		e.AddRotation (0, 0, 0);
		e.AddSpeed (speed, speed);
		e.AddTargetPosition (0, 0, 0);
		e.AddAsset ("Monster1", -1f);
	}

	public void Execute() {
		float t = Time.time;
		if (t >= nextWaveAt) {
			if (unitsToSpawn > 300) {
				unitsToSpawn = 300;
			}
			float unitSpeedMuliplier = Mathf.Clamp (Mathf.Pow (waveNumber, 0.5f), 1f, 4.0f);
			Debug.Log ("WAVE: " + waveNumber);
			Debug.Log ("Spawn count: " + (int)unitsToSpawn);
			for (int i = 1; i <= unitsToSpawn; i++) {
				CreateEnemy (_context, unitSpeedMuliplier);
			}
			// finish up
			waveNumber++;
			nextWaveAt += 5f;
			unitsToSpawn = (int)Mathf.Clamp(Mathf.Pow (unitsToSpawn, 1.1f), unitsToSpawn + 2f, unitsToSpawn * 1.5f);
		}
	}

}
