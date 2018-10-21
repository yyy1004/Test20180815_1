using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 工厂信息
    /// </summary>
    public class GCInfo
    {
        /// <summary>
        /// 工厂编号
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 工厂名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 工厂地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 工厂邮编
        /// </summary>
        public string PostAddress { get; set; }
        /// <summary>
        /// 工厂介绍
        /// </summary>
        public string Introduce { get; set; }
        /// <summary>
        /// 工厂电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 工厂电子邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 集团名称
        /// </summary>
        public string JTName { get; set; }
        /// <summary>
        /// 工厂经纬度
        /// </summary>
        public string JW { get; set; }

        public override string ToString()
        {
            return this.ID + "," + this.Name + "," + this.Address + "," + this.PostAddress + "," + this.Introduce + "," + this.Phone + "," + this.Email + "," + this.JTName + "," + this.JW + ";";
        }
    }
}
