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
    class ServerComponent
    {
        private int remotePort;
        private CalculationRequest calculationRequest;
        private CalculationResponse calculationResponse;
  
        public ServerComponent(int port, CalculationRequest calcRequest, CalculationResponse calcResponse)
        {
            this.remotePort = port;
            this.calculationResponse = calcResponse;
            this.calculationRequest = calcRequest;
        }
        // Default request and response
        public ServerComponent(int port)
        {
            this.remotePort = port;
        }

        public void Demo()
        {
            Console.WriteLine("test1");
            //1. Create a new server container.
            ServerConnectionContainer serverConnectionContainer = ConnectionFactory.CreateServerConnectionContainer(remotePort, false);
            //2. Apply some settings
            //Console.WriteLine("test2");
            serverConnectionContainer.AllowUDPConnections = true;
            //3. Set a delegate which will be called if we receive a connection
            //Console.WriteLine("test3");
            serverConnectionContainer.ConnectionEstablished += ServerConnectionContainer_ConnectionEstablished;
            //4. Set a delegate which will be called if we lose a connection
            //Console.WriteLine("test4");
            serverConnectionContainer.ConnectionLost += ServerConnectionContainer_ConnectionLost;
            //4. Start listening on port 1234
            //Console.WriteLine("test5");
            serverConnectionContainer.StartTCPListener();
            //Console.ReadLine();
        }


        private static void ServerConnectionContainer_ConnectionLost(Connection connection, ConnectionType connectionType, CloseReason closeReason)
        {
            Console.WriteLine("Connection server lost");
            Console.WriteLine($"Connection {connection.IPRemoteEndPoint} {connectionType} lost. {closeReason}");
        }

        private static void ServerConnectionContainer_ConnectionEstablished(Connection connection, ConnectionType connectionType)
        {
            while (true)
            {
                Console.WriteLine("Connection client established");
                Console.WriteLine($"{connectionType} Connection received {connection.IPRemoteEndPoint}.");
                connection.RegisterStaticPacketHandler<CalculationRequest>(calculationReceived);
                //connection.Send(new CalculationResponse(4, 4));
            }
        }

        private static void calculationReceived(CalculationRequest packet, Connection connection)
        {
            //4. Handle incoming packets.
            connection.Send(new CalculationResponse(4, 4)); 
            Console.WriteLine($"{packet.X} est la valeur x");
            
        }
    }
}
