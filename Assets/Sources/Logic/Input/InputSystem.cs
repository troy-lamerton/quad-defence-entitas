using Entitas;
using UnityEngine;

public sealed class InputSystem : IExecuteSystem, ICleanupSystem {

	readonly InputContext _context;

	public InputSystem(Contexts contexts) {
		_context = contexts.input;
	}

	public void Execute() {
		float rawH = Input.GetAxisRaw ("Horizontal");
		float rawV = Input.GetAxisRaw ("Vertical");

		bool hIsZero = Mathf.Approximately (rawH, 0);
		bool vIsZero = Mathf.Approximately (rawV, 0);

		if (vIsZero & hIsZero) {
			return;
		} else if (rawV == rawH) {
			// up and left held down, or other right angle
			return;
		} else {
			_context.CreateEntity ()
				.AddDirection (rawH, rawV);
		}
	}

	public void Cleanup() {
		foreach(var e in _context.GetGroup(InputMatcher.Direction).GetEntities()) {
			_context.DestroyEntity (e);
		}
	}
}
