using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDOSecurityPortalModel
{
    /// <summary>
    /// 用户角色对应表
    /// </summary>
    public class UserRoleMapping
    {
        public int? Id { get; set; }        //主键  
        public int? UserId { get; set; }    //用户ID
        public int? RoleId { get; set; }    //角色ID
    }
}
