using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 集团信息
    /// </summary>
    public class JTInfo
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PostAddress { get; set; }
        public string Introduce { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string JW { get; set; }

        public override string ToString()
        {
            return this.ID + "," + this.Name + "," + this.Address + "," + this.PostAddress + "," + this.Introduce + "," + this.Phone + "," + this.Email + "," + this.JW + ";";
        }
    }
}
