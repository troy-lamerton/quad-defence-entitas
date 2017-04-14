using Entitas;

public class GameSystems : Feature {

	public GameSystems(Contexts contexts) : base("Game Systems") {
		Add (new MoveTowardsSystem (contexts));
		Add (new MoveSystem (contexts));
		Add (new AddAttackSystem (contexts));
		Add (new AttackingSystem (contexts));
		Add (new SpawnEnemiesSystem (contexts));
		// TODO: remove target position component from enemies and add it to the player instead
		// that way there is only one instance of the target position component
		// enemy systems get the targetposiion from the _context.playerEntity
		//		Add(new PlayerAttackSystem(contexts));
	}
}
