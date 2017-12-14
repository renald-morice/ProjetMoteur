using System;
using Engine;

public class ServerTest : GameComponent,ILogicComponent
{
    public void Start()
	{
       ServerComponent client = new ServerComponent(1234);
        client.Demo();
	}
    public void Update()
    {

    }
}
