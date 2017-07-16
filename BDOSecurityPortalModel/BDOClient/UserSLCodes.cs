using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDOSecurityPortalModel
{
    /// <summary>
    /// 密锁资产对应表
    /// </summary>
    public class UserSLCodes
    {
        public string id;               //主键  
        public string propertyId;       //资产ID
        public string slCode;           //密锁
        public bool isNewRecord;        //
        public string remarks;          //
        public string createDate;       //
        public string updateDate;       //
        public string deltaFlag;        //
    }
}
