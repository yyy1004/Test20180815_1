using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 测点信息
    /// </summary>
    public class CDInfo
    {
        /// <summary>
        /// 测点编号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 机组编号
        /// </summary>
        public string JZId { get; set; }
        /// <summary>
        /// 测点名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 测点描述
        /// </summary>
        public string Introduce { get; set; }
        /// <summary>
        /// 预警值
        /// </summary>
        public int value1 { get; set; }
        /// <summary>
        /// 报警值
        /// </summary>
        public int value2 { get; set; }
    }
}
