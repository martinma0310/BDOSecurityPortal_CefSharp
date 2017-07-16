using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BDOSecurityPortalModel
{
    /// <summary>
    /// 系统表
    /// </summary>
    public class Link2System
    {
        private int? id;                    //主键
        private string keyword;             //关键字
        private string name;                //名称
        private string link;                //链接地址
        private int isShowClient;           //是否显示在客户端
        private int defaultIndex;           //默认排序
        private string remark;              //备注
        public string systemIconURL;        //子系统图标网络路径 
        private int isUseIE;                //是否使用IE打开子系统

        public Link2System(int? _id = null)
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
        /// 关键字
        /// </summary>
        public string Keyword
        {
            get { return keyword; }
            set { keyword = value; }
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// 链接地址
        /// </summary>
        public string Link
        {
            get { return link; }
            set { link = value; }
        }

        /// <summary>
        /// 是否显示在客户端
        /// </summary>
        public int IsShowClient
        {
            get { return isShowClient; }
            set { isShowClient = value; }
        }

        /// <summary>
        /// 默认排序
        /// </summary>
        public int SortIndex
        {
            get { return defaultIndex; }
            set { defaultIndex = value; }
        }


        /// <summary>
        /// 链接地址
        /// </summary>
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        /// <summary>
        /// 子系统图标网络路径
        /// </summary>
        public string SystemIconURL
        {
            get { return systemIconURL; }
            set { systemIconURL = value; }
        }


        /// <summary>
        /// 是否使用IE打开子系统
        /// </summary>
        public int IsUseIE
        {
            get { return isUseIE; }
            set { isUseIE = value; }
        }

    }
}
