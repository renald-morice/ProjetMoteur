using System;
using System.Diagnostics;
using Newtonsoft.Json;

namespace Engine
{
	// Basic component.
	// TODO/FIXME: In Unity, a component that can be activated/enabled is a 'Behavior' (derived from component).
	//             Still in Unity, a component has a reference to a GameObject, not a GameEntity.
	// TODO: Add a OnDestroy method that each component can/must implement.
	public abstract class Component : IDisposable
	{
		// NOTE: Only the engine should be able to modify this value.
		//       Ideally, it would only be GameEntity, but this can not be done in C#.
		[JsonProperty]
		internal GameEntity _entity;

		[JsonIgnore]
		public GameEntity entity {
			get { return this._entity; }
		}

		private bool _active = true;

		public bool isActive {
			get { return this._active; }
		}

		public void SetActive(bool active) {
			this._active = active;
		}
		
		virtual public void Awake() {}
		virtual public void OnDestroy() {}

		public Component()
		{
			Game.Instance.RegisterComponent(this);
		}

		public void Dispose()
		{
			Game.Instance.UnregisterComponent(this);
			_active = false;
			
			OnDestroy();
		}
	}
}

