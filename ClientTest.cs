using System;
using Engine;

public class ClientTest : GameComponent,ILogicComponent
{
    ClientComponent client;
    private int cpt;
    public void Start()
	{
        cpt = -1;
        client = new ClientComponent("192.168.43.103",8000);
        client.Init(); ;
        //client.InitTcp();
        //client.InitUdp();
	}
    public void Update()
    {/*
        cpt++;
        if (cpt == 0)
        {
            client.Loop();
        }
        if (cpt == 20)
        {
            cpt = -1;
        }
        */
    }
}
