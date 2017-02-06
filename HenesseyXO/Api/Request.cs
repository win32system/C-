using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HenesseyXO
{
    public class Request
    {
        public Client Wanted { set; get; }
        public Client Wanter { set; get; }

        public Request(Client wanted, Client wanter)
        {
            this.Wanter = wanter;
            this.Wanted = wanted;
        }
    }
}
