using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDOSecurityPortalModel
{
    public class Property
    {
        public int id;
        public string name;             // 资产名
        public string mark;             //资产编号
        public string tinyintfield1;    // 是否绑定
        public string tinyintfield2;    // 是否公用电脑
        public string loginid;          // 用户账号

        public string slCodeId;
        public string slCode;

        public string ComboValue;
        public string ComboDisplay;
    }
}
