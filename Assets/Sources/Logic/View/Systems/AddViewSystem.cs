using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public sealed class AddViewSystem : ReactiveSystem<GameEntity> {

	public AddViewSystem(Contexts contexts) : base(contexts.game) {
	}

	protected override Collector<GameEntity> GetTrigger (IContext<GameEntity> context) {
		return context.CreateCollector<GameEntity> (GameMatcher.Asset);
	}

	protected override bool Filter (GameEntity entity) {
		return entity.hasAsset;
	}

	readonly Transform _viewContainer = new GameObject("Views").transform;

	protected override void Execute (List<GameEntity> entities) {
		foreach(GameEntity e in entities) {
			var asset = Resources.Load<GameObject>(e.asset.name);

			// load game object
			GameObject gameObject = null;
			try {
				gameObject = UnityEngine.Object.Instantiate(asset);
			} catch(Exception) {
				Debug.Log("Cannot instantiate " + asset);
			}

			if(gameObject != null) {
				if (e.asset.lifetime > 0) {
					UnityEngine.Object.Destroy (gameObject, e.asset.lifetime / 60);
				}
				gameObject.transform.parent = _viewContainer;
				e.AddView(gameObject);
			}
		}
	}
}
