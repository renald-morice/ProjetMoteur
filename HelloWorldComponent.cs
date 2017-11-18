using System;
using System.IO.MemoryMappedFiles;
using System.Numerics;
using Engine;
using Engine.Utils;
using OpenTK.Input;

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
		
		var angle = MathUtils.Deg2Rad((10 * _count / 60.0f));
		
		gameObject.transform.rotation = Quaternion.CreateFromAxisAngle(new Vector3(1, 1, 1), angle * 2);
		//gameObject.transform.scale = 0.5f * Vector3.One * (float) Math.Sin(angle);
	}

	public void Render() {
		//Console.Out.WriteLine ("hello, world: " + _count);
	}
}

