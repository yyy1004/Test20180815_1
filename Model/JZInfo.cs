using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 机组信息
    /// </summary>
    public class JZInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Introduce { get; set; }
        public string JTName { get; set; }
        public string GCName { get; set; }
        public string Boss { get; set; }
        public string JW { get; set; }
        public int Status { get; set; }//1:正常 2：报警 3：故障 4：断网

        public override string ToString()
        {
            return this.Id + "," + this.Name + "," + this.Introduce + "," + this.JTName + "," + this.GCName + "," + this.Boss + "," + this.JW + ";";
        }
    }
}
