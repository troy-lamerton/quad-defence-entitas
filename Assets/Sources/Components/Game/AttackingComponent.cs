using Entitas;
using Entitas.CodeGenerator.Api;

[Game, Unique]
public class AttackingComponent : IComponent {

	public int direction; // from top to right: 0, 1, 2, 3
	public int state;
	public int initState;
}
