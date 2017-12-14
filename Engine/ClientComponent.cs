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
    class ClientComponent
    {
        private string ipAdress ;
        private int remotePort ;
        private CalculationRequest calculationRequest;
        private CalculationResponse calculationResponse;
        private ClientConnectionContainer clientConnectionContainer;

        public ClientComponent(string ipAd,int port,CalculationRequest calcRequest, CalculationResponse calcResponse )
        {
            this.ipAdress = ipAd;
            this.remotePort = port;
            this.calculationResponse = calcResponse;
            this.calculationRequest = calcRequest;
        }

        public ClientComponent(string ipAd, int port)
        {
            this.ipAdress = ipAd;
            this.remotePort = port;
        }


        public void Demo()
        {
            //1. Establish a connection to the server.
            Console.WriteLine("test1");
            clientConnectionContainer = ConnectionFactory.CreateClientConnectionContainer(ipAdress, remotePort);
            //2. Register what happens if we get a connection
            //Console.WriteLine("test2");
            clientConnectionContainer.ConnectionEstablished += ClientConnectionContainer_ConnectionEstablished;
            //2. Register what happens if we lose a connection
            //Console.WriteLine("test3");
            clientConnectionContainer.ConnectionLost += ClientConnectionContainer_ConnectionLost;

        }

        private void ClientConnectionContainer_ConnectionLost(Connection connection, Network.Enums.ConnectionType connectionType, Network.Enums.CloseReason closeReason)
        {
            Console.WriteLine("Connection client lost");
            Console.WriteLine($"Connection {connection.IPRemoteEndPoint} {connectionType} lost. {closeReason}");
        }

        private void ClientConnectionContainer_ConnectionEstablished(Connection connection, Network.Enums.ConnectionType connectionType)
        {
            Console.WriteLine("Connection client Established");
            Console.WriteLine($"{connectionType} Connection received {connection.IPRemoteEndPoint}.");
            connection.Send(new CalculationRequest(5, 6));
            //3. Register what happens if we receive a packet of type "CalculationResponse"
            //clientConnectionContainer.RegisterPacketHandler<CalculationResponse>(calculationResponseReceived, this);
            //4. Send a calculation request.
            //connection.Send(new CalculationRequest(10, 10), this);
        }

        private void calculationResponseReceived(CalculationResponse response, Connection connection)
        {
            //5. CalculationResponse received.
            Console.WriteLine($"Answer received {response.Result}");
        }
    }
}
