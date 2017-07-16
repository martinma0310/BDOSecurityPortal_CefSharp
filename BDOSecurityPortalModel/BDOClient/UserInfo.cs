using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDOSecurityPortalModel
{
    /// <summary>
    /// 用户表
    /// </summary>
    public class UserInfo
    {
        private string userId;  //用户编号
        private bool isAdmin;   //是否管理员
        private bool isValid;   //状态标记

        /// <summary>
        /// 角色名
        /// </summary>
        public string UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        /// <summary>
        /// 是否管理员
        /// </summary>
        public bool IsAdmin
        {
            get { return isAdmin; }
            set { isAdmin = value; }
        }
        /// <summary>
        /// 状态标记
        /// </summary>
        public bool IsValid
        {
            get { return isValid; }
            set { isValid = value; }
        }
    }
}
