using System;
using System.Numerics;

namespace Engine
{
	public class Transform
	{
		// NOTE (francois):
		//  This is done to disallow modifying the vectors' fields directly (i.e, position.x = 5),
		//  to mimic the behaviour of Unity.
		//  Is this a good thing? (Currently, nothing breaks if we change them manually
		//  (and I do not see how it could...)
		private Vector3 _position;
		public Vector3 position { get ; set; }
		
		public Vector3 localPosition
		{
			get => (parent == null) ? position : position - parent.transform.position;
			set => position = (parent == null) ? value : parent.transform.position + value;
		}
		
		// NOTE (francois):
		//  See NOTE about _position.
		//  Should this also be done here? (Currently, these fields are just placeholders,
		//  so I did not think about it any further)
		public Vector3 scale;
		public Quaternion rotation;
		
		public GameObject parent { get; private set; }
		public GameObject[] children { get; private set; }
		
		// TODO: Add SetParent(Transform) method (a SetChild(Transform) method may not be _that_ useful)
	}
}

