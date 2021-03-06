﻿using System;
using System.Collections.Generic;

namespace Engine
{
	// NOTE(francois): This is just so I can create a List<ISystem>.
	public interface ISystem 
	{
		void Iterate();
		void LateIterate(); // Called after each ISystem.Iterate has been called (just used for LogicSystem right now).
		bool IsValidComponent(Component component);
		void TrackComponent(Component component);
		void UntrackComponent(Component component);
		void BuryComponents();
	}
	
	public abstract class System<C> : ISystem where C : class
	{
		// FIXME: This should only keep active components
		protected List<C> _components = new List<C>();
		protected List<C> _newComponents = new List<C>();
		// We can not immediatly remove components in case someone is iterating over the lists
		// NOTE: It assumes that nobody wants to remove a not-yet-started component (those in _newComponent).
		// TODO?: Assert that it is the case.
		protected List<C> _destroyedComponents = new List<C>();

		public bool IsValidComponent(Component component)
		{
			return component is C;
		}
		
		// NOTE(francois)/FIXME: Checking to see if the component's type is valid is already done beforehand. 
		public virtual void TrackComponent(Component component)
		{
			var tracked = component as C;
			
			// TODO: This should be an assert
			if (_newComponents.Contains(tracked) || _components.Contains(tracked)) Console.Out.WriteLine("Component already present " + tracked);
			else
			{
				_newComponents.Add(tracked);
			}
		}
		
		public virtual void UntrackComponent(Component component)
		{
			var destroyed = component as C;
			
			// TODO: This should be an assert (and it is not critical, it will just do nothing)
			if (!_components.Contains(destroyed) && !_newComponents.Contains(destroyed)) Console.Out.WriteLine("Component not present " + destroyed);
			else
			{
				_destroyedComponents.Add(destroyed);
			}
		}

		public abstract void Iterate();
		public abstract void LateIterate();

		// Move new component to the component list
		protected void UpdateComponentList()
		{
			if (_newComponents.Count != 0)
			{
				_components.AddRange(_newComponents);
				_newComponents.Clear();
			}
		}

		// Remove deleted components from the list
		public void BuryComponents()
		{
			foreach (var deleted in _destroyedComponents)
			{
				bool result = _components.Remove(deleted);
			}
			
			_destroyedComponents.Clear();
		}
	}
}

