using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDOSecurityPortalModel
{
    /// <summary>
    /// 历史日志-不存新日志
    /// </summary>
    public class ClientLogs
    {
        private int? id;            //主键
        private DateTime logDate;   //生成日期
        private string logType;     //日志类别
        private string userId;      //人员编号
        private string userSLCode;  //密锁
        private string pcName;      //计算机名
        private string cpuId;       //cpu编号
        private string cpuName;     //cpu名称
        private string diskName;    //硬盘名称
        private string mcAddress;   //MC地址
        private string ip;          //ip
        private string ram;         //内存
        private string logData;     //日志内容

        /// <summary>
        /// 主键
        /// </summary>
        public int? ID
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// 生成日期
        /// </summary>
        public DateTime LogDate
        {
            get { return logDate; }
            set { logDate = value; }
        }

        /// <summary>
        /// 日志类别
        /// </summary>
        public string LogType
        {
            get { return logType; }
            set { logType = value; }
        }

        /// <summary>
        /// 人员编号
        /// </summary>
        public string UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        /// <summary>
        /// 密锁
        /// </summary>
        public string UserSLKeyCode
        {
            get { return userSLCode; }
            set { userSLCode = value; }
        }

        /// <summary>
        /// 计算机名
        /// </summary>
        public string PCName
        {
            get { return pcName; }
            set { pcName = value; }
        }

        /// <summary>
        /// cpu编号
        /// </summary>
        public string CPUID
        {
            get { return cpuId; }
            set { cpuId = value; }
        }

        /// <summary>
        /// cpu名称
        /// </summary>
        public string CPUName
        {
            get { return cpuName; }
            set { cpuName = value; }
        }

        /// <summary>
        /// 硬盘名称
        /// </summary>
        public string DiskName
        {
            get { return diskName; }
            set { diskName = value; }
        }

        /// <summary>
        /// MAC地址
        /// </summary>
        public string MACAddress
        {
            get { return mcAddress; }
            set { mcAddress = value; }
        }

        /// <summary>
        /// ip地址
        /// </summary>
        public string IPAddress
        {
            get { return ip; }
            set { ip = value; }
        }

        /// <summary>
        /// 内存
        /// </summary>
        public string RAM
        {
            get { return ram; }
            set { ram = value; }
        }

        /// <summary>
        /// 日志内容
        /// </summary>
        public string LogData
        {
            get { return logData; }
            set { logData = value; }
        }
    }
}
