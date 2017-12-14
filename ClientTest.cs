using System;
using Engine;

public class ClientTest : GameComponent,ILogicComponent
{
    public void Start()
	{
        ClientComponent client = new ClientComponent("193.162.....",1234);
        client.Demo();
	}
    public void Update()
    {

    }
}
