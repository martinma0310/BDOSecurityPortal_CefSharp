using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDOSecurityPortalModel
{
    /// <summary>
    /// 角色权限表
    /// </summary>
    public class RolesAuthority
    {
        private int? id;        //主键  
        private int? roleId;    //角色ID
        private int? sysId;     //系统ID

        public RolesAuthority(int? _id = null)
        {
            id = _id;
        }

        /// <summary>
        /// 主键
        /// </summary>
        public int? ID
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// 角色ID
        /// </summary>
        public int? RoleId
        {
            get { return roleId; }
            set { roleId = value; }
        }

        /// <summary>
        /// 系统ID
        /// </summary>
        public int? SystemId
        {
            get { return sysId; }
            set { sysId = value; }
        }
    }
}
