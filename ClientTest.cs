using System;
using Engine;

public class ClientTest : GameComponent,ILogicComponent
{
    ClientComponent client;
    public void Start()
	{
        client = new ClientComponent("192.168.43.103",1234);
        client.Demo();
	}
    public void Update()
    {
        client.Demo();
    }
}
