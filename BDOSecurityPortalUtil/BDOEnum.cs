using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDOSecurityPortalUtil
{
    /// <summary>
    /// 枚举常量
    /// </summary>
    public class BDOEnum
    {
        /// <summary>
        /// 用户登录验证结果
        /// </summary>
        public enum LoginResult
        {
            /// 
            /// 正常登录
            /// 
            LOGIN_USER_DEFAULT = 0,
            /// 
            /// 正常登录
            /// 
            LOGIN_USER_OK,
            /// 
            /// 用户不存在
            /// 
            LOGIN_USER_DOESNT_EXIST,
            /// 
            /// 用户帐号被禁用
            /// 
            LOGIN_USER_ACCOUNT_INACTIVE,
            /// 
            /// 用户密码不正确
            /// 
            LOGIN_USER_PASSWORD_INCORRECT,
            /// 
            /// 用户登录失败
            /// 
            LOGIN_USER_PASSWORD_FAILED,
        }
    }
}