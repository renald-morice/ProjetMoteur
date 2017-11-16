using System;
using System.Numerics;
using Engine;

public class HelloWorldComponent : GameComponent, ILogicComponent, IRenderComponent
{
	private int _count = 0;

	public void Update() {
		++_count;

		/*if (_count > 5) {
			SetActive(false);
		}
		*/
		var position = gameObject.transform.position;
		var rotatin = gameObject.transform.rotation;
		//gameObject.transform.position = new Vector3(position.X + (1.0f / 60), position.Y, position.Z);
		var angle = (float) Math.PI * (10 * _count / 60.0f) / 180;
		
		gameObject.transform.rotation = Quaternion.CreateFromAxisAngle(new Vector3(0, 0, 1), angle);
		gameObject.transform.scale = Vector3.One * (float) Math.Sin(angle);
	}

	public void Render() {
		Console.Out.WriteLine ("hello, world: " + _count);
	}
}

