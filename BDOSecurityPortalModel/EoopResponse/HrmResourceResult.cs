using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDOSecurityPortalModel
{
    public class HrmResourceResult
    {
        public bool ok;
        public string value;
        public List<HrmResource> objValue;
    }

    public class ChatUnreadCountResult
    {
        public bool result;
        public string value;
        public int objValue;
    }
}
