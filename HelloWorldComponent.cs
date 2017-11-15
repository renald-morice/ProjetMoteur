using System;
using Engine;

public class HelloWorldComponent : GameComponent, ILogicComponent, IRenderComponent
{
	private int _count = 0;

	public void Update() {
		++_count;

		if (_count > 5) {
			SetActive(false);
		}
	}

	public void Render() {
		Console.Out.WriteLine ("hello, world: " + _count);
	}
}

