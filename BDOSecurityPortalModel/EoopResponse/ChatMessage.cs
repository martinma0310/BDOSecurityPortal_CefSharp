using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDOSecurityPortalModel
{
    public class ChatMessage
    {
        public string uuid;
        public string sessionJID;
        public string from;
        public string to;
        public string detail;
        public string timestamp;
        public string content;  //Message
        public string type;
        public int length;
        public int unreadCount;
        public string fromName;
        public string fromPic;
        public string toName;
        public string toPic;
    }
}
