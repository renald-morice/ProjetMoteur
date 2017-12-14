using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Network;
using Network.Attributes;
using Network.Interfaces;
using Network.Packets;
using Network.Extensions;
using Network.Enums;
using Network.Converter;

namespace Engine
{
    class ClientComponent : GameComponent
    {
        private string ipAdress;
        private int cpt;
        private int remotePort;
        private CalculationRequest calculationRequest;
        private CalculationResponse calculationResponse;
        private ClientConnectionContainer clientConnectionContainer;
        private Connection actualConnection;
        private int connected;

        public ClientComponent(string ipAd, int port, CalculationRequest calcRequest, CalculationResponse calcResponse)
        {
            this.ipAdress = ipAd;
            this.remotePort = port;
            this.calculationResponse = calcResponse;
            this.calculationRequest = calcRequest;
            this.cpt = 0;
        }

        public ClientComponent(string ipAd, int port)
        {
            this.cpt = 0;
            this.ipAdress = ipAd;
            this.remotePort = port;
        }

        public void Init()
        {
            connected = -1;
            //1. Establish a connection to the server.
            Console.WriteLine("Init");
            clientConnectionContainer = ConnectionFactory.CreateClientConnectionContainer(ipAdress, remotePort);
            //2. Register what happens if we get a connection
            clientConnectionContainer.ConnectionEstablished += ClientConnectionContainer_ConnectionEstablished;
            //2. Register what happens if we lose a connection
            clientConnectionContainer.ConnectionLost += ClientConnectionContainer_ConnectionLost;

        }


        public void Loop()
        {
           

            if (connected == 1)
            {
                Console.WriteLine("loop");


                actualConnection.Send(new CalculationRequest(5, 5));
               
            }
            else
            {
                if (connected == 0)
                {
                    Init();
                }
            }
        }

        private void ClientConnectionContainer_ConnectionLost(Connection connection, Network.Enums.ConnectionType connectionType, Network.Enums.CloseReason closeReason)
        {
            actualConnection = connection;
            connected = 0;
            Console.WriteLine("Connection client lost");
            Console.WriteLine($"Connection {connection.IPRemoteEndPoint} {connectionType} lost. {closeReason}");
        }

        private void ClientConnectionContainer_ConnectionEstablished(Connection connection, Network.Enums.ConnectionType connectionType)
        {
            actualConnection = connection;
            connected = 1;
            Console.WriteLine("Connection client Established");
            Console.WriteLine($"{connectionType} Connection received {connection.IPRemoteEndPoint}.");
            actualConnection.Send(new CalculationRequest(5, 5));
            actualConnection.RegisterStaticPacketHandler<CalculationResponse>(calculationResponseReceived);

        }


        private void calculationResponseReceived(CalculationResponse response, Connection connection)
        {
            //5. CalculationResponse received.
            Console.WriteLine($"Answer received {response.X}");
            actualConnection.Send(new CalculationRequest(6, 6));
        }
    }
}
