using System;
using Engine;

public class ServerTest : GameComponent,ILogicComponent
{
    ServerComponent server;
    public void Start()
	{
        server = new ServerComponent(8000);
        server.Demo();
	}
    public void Update()
    {
        //server.Demo();

    }
}
