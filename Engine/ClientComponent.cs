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
    class ClientComponent : NetworkComponent
    {
        public string ipAdress;
        public int remotePort;
        public CalculationRequest calculationRequest;
        public CalculationResponse calculationResponse;

        private ClientConnectionContainer clientConnectionContainer;

        public void Demo(int id, string ipAdress, int remotePort)
        {
            //1. Establish a connection to the server.
            clientConnectionContainer = ConnectionFactory.CreateClientConnectionContainer(ipAdress, remotePort);
            //2. Register what happens if we get a connection
            clientConnectionContainer.ConnectionEstablished += ClientConnectionContainer_ConnectionEstablished;
            //2. Register what happens if we lose a connection
            clientConnectionContainer.ConnectionLost += ClientConnectionContainer_ConnectionLost;

        }

        private void ClientConnectionContainer_ConnectionLost(Connection connection, Network.Enums.ConnectionType connectionType, Network.Enums.CloseReason closeReason)
        {
            Console.WriteLine($"Connection {connection.IPRemoteEndPoint} {connectionType} lost. {closeReason}");
        }

        private void ClientConnectionContainer_ConnectionEstablished(Connection connection, Network.Enums.ConnectionType connectionType)
        {
            Console.WriteLine($"{connectionType} Connection received {connection.IPRemoteEndPoint}.");
            //3. Register what happens if we receive a packet of type "CalculationResponse"
            clientConnectionContainer.RegisterPacketHandler<CalculationResponse>(calculationResponseReceived, this);
            //4. Send a calculation request.
            connection.Send(new CalculationRequest(10, 10), this);
        }

        private void calculationResponseReceived(CalculationResponse response, Connection connection)
        {
            //5. CalculationResponse received.
            Console.WriteLine($"Answer received {response.Result}");
        }
    }
}
