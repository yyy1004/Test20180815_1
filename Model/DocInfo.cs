using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 文档信息
    /// </summary>
    public class DocInfo
    {
        public int Id { get; set; }
        public string JZId { get; set; }
        public string Name { get; set; }
        public string UploadTime { get; set; }
        public string Note { get; set; }
        public string Type { get; set; }
    }
}
