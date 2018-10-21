using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 菜单表
    /// </summary>
    public class MenuInfo
    {
        /// <summary>
        /// 菜单号
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 父菜单号
        /// </summary>
        public int ParentId { get; set; }
        /// <summary>
        /// 菜单序号
        /// </summary>
        public int Num { get; set; }
        /// <summary>
        /// 菜单描述
        /// </summary>
        public string Disc { get; set; }
        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 菜单图标
        /// </summary>
        public string Ico { get; set; }
        /// <summary>
        /// 菜单等级
        /// </summary>
        public int Level { get; set; }

        public override string ToString()
        {
            return this.Id + "," + this.Name + "," + this.ParentId + "," + this.Num + "," + this.Disc + "," + this.Url + "," + this.Ico + "," + this.Level + ";";
        }
    }
}
