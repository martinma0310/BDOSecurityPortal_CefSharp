using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDOSecurityPortalModel
{
    /// <summary>
    /// 角色表
    /// </summary>
    public class Roles
    {
        private int? id;            //主键  
        private string roleName;    //角色名
        private string roleType;    //角色类型
        private bool isDefault;     //是否默认权限

        public Roles(int? _id = null)
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
        /// 角色名
        /// </summary>
        public string RoleName
        {
            get { return roleName; }
            set { roleName = value; }
        }

        /// <summary>
        /// 角色类型
        /// </summary>
        public string RoleType
        {
            get { return roleType; }
            set { roleType = value; }
        }

        /// <summary>
        /// 角色类型
        /// </summary>
        public bool IsDefault
        {
            get { return isDefault; }
            set { isDefault = value; }
        }
    }
}
