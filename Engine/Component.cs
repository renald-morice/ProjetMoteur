using System;
using System.Diagnostics;

namespace Engine
{
	// Basic component.
	// TODO/FIXME: In Unity, a component that can be activated/enabled is a 'Behavior' (derived from component).
	//             Still in Unity, a component has a reference to a GameObject, not a GameEntity.
	// TODO: Probably add an Awake method called just after the component instantiation (_entity must be set).
	public abstract class Component
	{
		// NOTE: Only the engine should be able to modify this value.
		//       Ideally, it would only be GameEntity, but this can not be done in C#.
		internal GameEntity _entity;

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
		
		abstract public void Awake();

		public Component()
		{
			Game.Instance.RegisterComponent(this);
		}

		~Component()
		{
			Game.Instance.UnregisterComponent(this);
		}
	}
}

