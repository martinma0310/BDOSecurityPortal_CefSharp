using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDOSecurityPortalModel
{
    /// <summary>
    /// 日志表
    /// </summary>
    public class ClientLogsV2
    {
        public int? id;                //主键
        public string company;         //地区公司
        public string department;      //部门
        public string userName;        //用户名
        public string loginId;         //账号
        public string userSlCode;      //密锁
        public string itMark;          //资产编号
        public string pcName;          //计算机名
        public string cpuId;           //cpu编号
        public string cpuName;         //cpu名称
        public string diskName;        //硬盘名称
        public string mcAddress;       //MC地址
        public string ip;              //ip
        public string ram;             //内存
        public string logDate;         //生成日期
        public string logTime;         //日志时间
        public string logType;         //日志类别
        public string logTypeValue;    //日志类型编号
        public string logData;         //日志内容
    }
}
