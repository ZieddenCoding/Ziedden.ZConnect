using EmbedIO.Routing;
using EmbedIO.Utilities;
using EmbedIO.WebApi;
using EmbedIO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Ziedden.ZConnect.StringData;

namespace Ziedden.ZConnect.ServerDatas
{
    public class ServerBase : WebApiController
    {

        [Route(HttpVerbs.Post, "/sendDatas")]
        public async Task sendDatas([StringData] string datas)
        {
            using (var writer = HttpContext.OpenResponseText())
            {
                ServerClass sc = JsonConvert.DeserializeObject<ServerClass>(datas);

                ServerPacket serverPacket = null;
                foreach (ServerPacket SP in ServerPackets.Packets)
                {
                    if(SP.serverAttribute.PacketName.Equals(sc.PacketName))
                    {
                        serverPacket = SP;
                        break;
                    }
                }

                if(serverPacket == null)
                {
                    throw new InvalidOperationException("Server Packet wurde nicht gefunden!");
                }

                object result = null;
                try
                {
                    result = serverPacket.methodInfo.Invoke(null, new object[] { (object)sc.PacketObject });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                await writer.WriteAsync(JsonConvert.SerializeObject(result));
            }

        }

        [Route(HttpVerbs.Post, "/sendDatasE")]
        public async Task sendDatasE([StringData] string datas)
        {
            using (var writer = HttpContext.OpenResponseText())
            {
                string key = Funcs.DataBack(JsonConvert.DeserializeObject<Enc>(datas).DATAB);
                ServerClass sc = JsonConvert.DeserializeObject<ServerClass>(Funcs.Decrypt(JsonConvert.DeserializeObject<Enc>(datas).DATA, $"ZC{key}"));
                ServerPacket serverPacket = null;
                foreach (ServerPacket SP in ServerPackets.Packets)
                {
                    if (SP.serverAttribute.PacketName.Equals(sc.PacketName))
                    {
                        serverPacket = SP;
                        break;
                    }
                }

                if (serverPacket == null)
                {
                    throw new InvalidOperationException("Server Packet wurde nicht gefunden!");
                }

                object result = null;
                try
                {
                    result = serverPacket.methodInfo.Invoke(null, new object[] { (object)sc.PacketObject });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                await writer.WriteAsync(JsonConvert.SerializeObject(new Enc(Funcs.Encrypt(JsonConvert.SerializeObject(result), $"ZC{key}"),"")));
            }

        }

    }
}
