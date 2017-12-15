using System;
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
	}
	
	public abstract class System<C> : ISystem where C : class
	{
		protected List<C> _components = new List<C>();
		protected List<C> _newComponents = new List<C>();

		public bool IsValidComponent(Component component)
		{
			return component is C;
		}
		
		// NOTE(francois)/FIXME: Checking to see if the component's type is valid is already done beforehand. 
		public virtual void TrackComponent(Component component)
		{
			var tracked = component as C;
			
			if (_newComponents.Contains(tracked) || _components.Contains(tracked)) Console.Out.WriteLine("Component already present " + tracked);
			else
			{
				_newComponents.Add(tracked);
			}
		}
		
		public virtual void UntrackComponent(Component component)
		{
			bool result = _components.Remove(component as C) || _newComponents.Remove(component as C);
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
	}
}

