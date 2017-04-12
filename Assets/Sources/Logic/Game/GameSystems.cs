using Entitas;

public class GameSystems : Feature {

	public GameSystems(Contexts contexts) : base("Game Systems") {
		Add (new MoveSystem (contexts));
		Add (new MoveTowardsSystem (contexts));
		//		Add(new PlayerAttackSystem(contexts));
	}
}
