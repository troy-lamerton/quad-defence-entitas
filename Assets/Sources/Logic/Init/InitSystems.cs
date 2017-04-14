using Entitas;

public class InitSystems : Feature {
	public InitSystems (Contexts contexts) {
		Add (new InitPlayerSystem(contexts));
		Add (new InitEnemiesSystem (contexts));
//		Add (new InitScoresSystem (contexts));
	}
}
