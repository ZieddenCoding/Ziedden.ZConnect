using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ziedden.ZConnect.ServerDatas
{
 
    
       [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
        public class ServerAttribute : Attribute
        {

            private string vName;

            public ServerAttribute(string PacketName)
            {
                this.vName = PacketName;
            }

            public virtual string PacketName
            {
                get { return vName; }
                set { vName = value; }
            }

        

    }
}
