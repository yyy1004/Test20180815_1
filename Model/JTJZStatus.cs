using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 集团工作状态
    /// </summary>
    public class JTJZStatus
    {
        public string JTName { get; set; }
        public string GCName { get; set; }
        public int Total { get; set; }
        public int Work { get; set; }
        public int Warn { get; set; }
        public int Stop { get; set; }
    }
}
