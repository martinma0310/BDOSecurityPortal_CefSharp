using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDOSecurityPortalModel
{
    /// <summary>
    /// 部门角色对应表
    /// </summary>
    public class DepartRoleMapping
    {
        public int? Id { get; set; }            //主键  
        public int? DepartId { get; set; }      //部门ID
        public int? RoleId { get; set; }        //角色ID

        //entity.Role = new Roles();
        //entity.Role.Id = Int32.Parse(dr["roleId"].ToString());
        //entity.Role.RoleName = dr["roleName"].ToString();
        //entity.Role.RoleType = dr["roleType"].ToString();
        //entity.Role.IsDefault = Int32.Parse(dr["isDefault"].ToString());
    }
}
