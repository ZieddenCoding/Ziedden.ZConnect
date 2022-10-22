using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmbedIO;
using EmbedIO.WebApi;
using EmbedIO.Actions;
using Newtonsoft.Json;

namespace Ziedden.ZConnect
{
    public class Server
    {

        private string Bind;
        private int ThisNumber = 0;
        
        public Server(string IP,int Port)
        {
            string bind = $"http://{IP}:{Port.ToString()}/";
            Bind = bind;
            GlobalVars.RunningNumber++;
            ThisNumber = GlobalVars.RunningNumber;
            GlobalVars.AllServers.Add(GlobalVars.RunningNumber, this);
        }

        public void Start()
        {
            var server = CreateWebServer(Bind);
            server.Start();
            ServerDatas.ServerPackets.Load();
        }

        private WebServer CreateWebServer(string url)
        {
            var server = new WebServer(o => o
                    .WithUrlPrefix(url)
                    .WithMode(HttpListenerMode.EmbedIO))
                .WithLocalSessionManager()
                .WithWebApi("/ZConnect", m => m
                    .WithController<ServerDatas.ServerBase>())
                .WithModule(new ActionModule("/", HttpVerbs.Any, ctx => ctx.SendDataAsync(new { Message = "Error" })));
            return server;
        }

        public static T Parse<T>(object Datas)
        {
            T result;
            result = JsonConvert.DeserializeObject<T>(Datas.ToString());
            return result;
        }

    }
}
