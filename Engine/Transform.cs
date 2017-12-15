using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Numerics;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography.X509Certificates;
using Engine.Utils;
using FMOD;
using Jitter.LinearMath;
using Newtonsoft.Json;

namespace Engine
{
	public enum Space
	{
		World,
		Self
	}
	
	public class Transform
	{
		[JsonIgnore]
		public Vector3 position
		{
			get => (parent == null) ? localPosition : localPosition +  parent.position;
			set => localPosition = (parent == null) ? value : value - parent.position;
		}

		// TODO: Do the same for local scale
		public Vector3 localPosition { get; set; } = Vector3.Zero;
		
		// TODO: Set this relative to the parent
		public Vector3 scale = Vector3.One;
		
		[JsonIgnore]
		public Quaternion rotation
		{
			get => (parent == null) ? localRotation : localRotation * parent.rotation;
			set => localRotation = (parent == null) ? value : value * Quaternion.Inverse(parent.rotation);
		}

		public Quaternion localRotation { get; set; } = Quaternion.Identity;

		[JsonProperty]
		public Transform parent { get; private set; } = null;

		/*[JsonConstructor]
		public Transform(Vector3 localPosition, Quaternion localRotation)
		{
			this.localPosition = localPosition;
			this.localRotation = localRotation;
		}*/
		//public List<Transform> children { get;} = new List<Transform>();

		/*private void _AddChild(Transform c)
		{
			children.Add(c);
		}
		
		private void _RemoveChild(Transform c)
		{
			children.Remove(c);
		}*/
		
		public void SetParent(Transform p)
		{
			/*if (children.Find(c => c == p) != null)
			{
				throw new Exception("Can not set child as parent.");
			}*/

			//parent?._RemoveChild(this);

			parent = p;

			//p?._AddChild(this);
		}
		
		// TODO: Add Rotate / Translate / Scale, ...

		public void Translate(Vector3 movement, Space space = Space.Self)
		{
			if (space == Space.World)
			{
				localPosition += movement;
			}
			else
			{
				localPosition += MathUtils.Rotate(movement, rotation);
			}
		}

		public void SetLocalPosition(Vector3 p, Space space = Space.Self)
		{
			if (space == Space.World)
			{
				localPosition = p;
			}
			else
			{
				localPosition = MathUtils.Rotate(p, rotation);
			}
		}
	}
}

