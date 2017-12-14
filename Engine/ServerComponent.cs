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
    class ServerComponent : NetworkComponent
    {
        public int remotePort;

        public void Demo()
        {
            //1. Create a new server container.
            ServerConnectionContainer serverConnectionContainer = ConnectionFactory.CreateServerConnectionContainer(remotePort, false);
            //2. Apply some settings
            serverConnectionContainer.AllowUDPConnections = true;
            //3. Set a delegate which will be called if we receive a connection
            serverConnectionContainer.ConnectionEstablished += ServerConnectionContainer_ConnectionEstablished;
            //4. Set a delegate which will be called if we lose a connection
            serverConnectionContainer.ConnectionLost += ServerConnectionContainer_ConnectionLost;
            //4. Start listening on port 1234
            serverConnectionContainer.StartTCPListener();

            Console.ReadLine();
        }

        private static void ServerConnectionContainer_ConnectionLost(Connection connection, ConnectionType connectionType, CloseReason closeReason)
        {
            Console.WriteLine($"Connection {connection.IPRemoteEndPoint} {connectionType} lost. {closeReason}");
        }

        private static void ServerConnectionContainer_ConnectionEstablished(Connection connection, ConnectionType connectionType)
        {
            Console.WriteLine($"{connectionType} Connection received {connection.IPRemoteEndPoint}.");
        }
    }
}
