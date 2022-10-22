using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Ziedden.ZConnect
{
    public class Client
    {
        private string Bind;
        public Client(string IP,int Port)
        {
            Bind = $"{IP}:{Port}";
        }


        public T SendPacket<T>(string PacketName,object PacketObject)
        {
            ServerDatas.ServerClass sc = new ServerDatas.ServerClass(PacketName, PacketObject);
            HttpClient hc = new HttpClient();
            var data = new StringContent(JsonConvert.SerializeObject(sc), Encoding.UTF8, "application/json");
            HttpResponseMessage kp = hc.PostAsync($"http://{Bind}/ZConnect/sendDatas", data).Result;
            try
            {
                return JsonConvert.DeserializeObject<T>(kp.Content.ReadAsStringAsync().Result);
            }
            catch { }

            object _null = null;
            return (T)_null;
        }

        public T SendPacketEncrype<T>(string PacketName, object PacketObject)
        {
            ServerDatas.ServerClass sc = new ServerDatas.ServerClass(PacketName, PacketObject);
            HttpClient hc = new HttpClient();
            string Enc = DateTime.Now.ToString("hhmmssyyyyMMdd");
            var data = new StringContent(JsonConvert.SerializeObject(new ServerDatas.Enc(ServerDatas.Funcs.Encrypt(JsonConvert.SerializeObject(sc), $"ZC{Enc}"),ServerDatas.Funcs.Data(Enc))), Encoding.ASCII, "application/json");
            HttpResponseMessage kp = hc.PostAsync($"http://{Bind}/ZConnect/sendDatasE", data).Result;
            try
            {
                return JsonConvert.DeserializeObject<T>(ServerDatas.Funcs.Decrypt(JsonConvert.DeserializeObject<ServerDatas.Enc>(kp.Content.ReadAsStringAsync().Result).DATA, $"ZC{Enc}"));
            }
            catch(Exception ex) { Console.WriteLine(ex.ToString()); }

            object _null = null;
            return (T)_null;
        }


    }
}
