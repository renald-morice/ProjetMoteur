﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Engine
{
	// NOTE/TODO: It may happen that we try to query a non-existing GameEntity.
	//  In that case, maybe it would be useful to introduce a NullGameEntity that just does nothing,
    //  instead of crashing when accessing a real null value.
	//  The same could be done with GameComponents.
	// Basic entity.
 	public abstract class GameEntity : IDisposable
	{
		[JsonProperty]
		private List<Component> _allComponents;

		public GameEntity ()
		{
			_allComponents = new List<Component>();
		}

		public T AddComponent<T>() where T : Component {
			T result = (T) Activator.CreateInstance(typeof(T), new object[] {});
			result._entity = this;

			_allComponents.Add(result);
			
			result.Awake();

			return result;
		}

		public T GetComponent<T>() where T : class {
			T result = _allComponents.Find(c => c is T) as T;

			return result;
		}

		public List<T> GetComponents<T>() where T : class {
			List<T> result = _allComponents.FindAll(c => c is T).ConvertAll(c => c as T);

			return result;
		}

		public void RemoveComponent(Component component) {
			if (component != null)
			{
				component.Dispose();
				_allComponents.Remove(component);
			}
		}

		public void RemoveComponent<T>() where T : Component {
			T result = GetComponent<T>();
			
			if (result != null)
			{
				result.Dispose();

				_allComponents.Remove(result);
			}
		}

		public void RemoveComponents<T>() where T : Component
		{
			List<Component> components = _allComponents.FindAll(c => c is T);

			foreach (var c in components)
			{
				c.Dispose();
			}

			_allComponents.RemoveAll(c => c is T);
		}

		public void Dispose()
		{
			foreach (var c in _allComponents)
			{
				c.Dispose();
			}
			
			_allComponents.Clear();
		}
	}
}

