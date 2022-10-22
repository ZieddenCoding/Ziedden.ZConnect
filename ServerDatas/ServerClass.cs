using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ziedden.ZConnect.ServerDatas
{
    public class ServerClass
    {
        public string PacketName { get; set; }
        public object PacketObject { get; set; }

        public ServerClass(string packetName, object packetObject)
        {
            PacketName = packetName;
            PacketObject = packetObject;
        }
    }

    public class Enc {
        public string DATA { get; set; }
        public string DATAB { get; set; }

        public Enc(string DATA,string DATAB)
        {
            this.DATA = DATA;
            this.DATAB = DATAB;
        }
    }
}
