using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ziedden.ZConnect.ServerDatas
{
    public class ServerPacket
    {
        public ServerAttribute serverAttribute;
        public MethodInfo methodInfo;

        public ServerPacket(ServerAttribute serverAttribute, MethodInfo methodInfo)
        {
            this.serverAttribute = serverAttribute;
            this.methodInfo = methodInfo;
        }
    }

    public class ServerPackets
    {
        public static List<ServerPacket> Packets = new List<ServerPacket>();

        public static void Load()
        {
            int count = 0;
            Type[] types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(p => p.IsClass)
                .ToArray();
            foreach (Type type in types)
            {
                MethodInfo[] mis = type.GetMethods();
                foreach (MethodInfo mi in mis)
                {
                    try
                    {
                        ServerAttribute cc = mi.GetCustomAttribute<ServerAttribute>();
                        if (!(cc == null))
                        {
                            Packets.Add(new ServerPacket(cc, mi));
                            Console.WriteLine($"Found: {cc.PacketName}");
                            count++;
                        }
                    }
                    catch { }
                }
            }
        }
    }
}
