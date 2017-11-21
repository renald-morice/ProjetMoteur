using System;
using System.IO.MemoryMappedFiles;
using System.Numerics;
using Engine;
using Engine.Utils;
using Jitter.LinearMath;
using OpenTK.Input;

public class HelloWorldComponent : GameComponent, ILogicComponent, IRenderComponent
{
	private int _count = 0;
	private RigidBodyComponent _body;

	public void Start()
	{
		_body = gameObject.GetComponent<RigidBodyComponent>();
		
		if (_body != null)
		{
			Game.Instance.window.Mouse.ButtonDown += (sender, e) =>
			{
				var horizontalSign = (e.Button == 0) ? 1 : -1;
				var magnitude = 10000.0f;

				var movement = new JVector(horizontalSign * 1, 1, 0) * magnitude;
				_body.rigidBody.AddForce(movement);
			};
		}
	}
	
	public void Update() {
		++_count;

		/*if (_count > 5) {
			SetActive(false);
		}
		*/
		var position = gameObject.transform.position;
		var rotatin = gameObject.transform.rotation;
		
		var angle = MathUtils.Deg2Rad((10 * _count / 60.0f));
		
		//gameObject.transform.rotation = Quaternion.CreateFromAxisAngle(new Vector3(1, 1, 1), angle * 2);
		//gameObject.transform.scale = 0.5f * Vector3.One * (float) Math.Sin(angle);	
	}

	public void Render() {
		//Console.Out.WriteLine ("hello, world: " + _count);
	}
}

