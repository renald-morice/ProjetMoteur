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
    public class CalculationResponse : ResponsePacket
    {
        public CalculationResponse()
        {

        }

        public CalculationResponse(int X, int Y)
        {
            this.X = X;
            this.Y = Y;
        }

        public int X { get; set; }

        public int Y { get; set; }
        public int Result;

        [PacketIgnoreProperty]
        public int I_WILL_BE_IGNORED { get; set; }

        public override void BeforeSend()
        {
            Y += 5;
            base.BeforeSend();
        }

        public override void BeforeReceive()
        {
            Y -= 5;
            Result = Y;
            base.BeforeReceive();
        }
    }
}
