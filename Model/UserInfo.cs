using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class UserInfo
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phonenumber { get; set; }
        public string JT { get; set; }
        public string GC { get; set; }
        public string Role { get; set; }
        public string Note { get; set; }

        public override string ToString()
        {
            return this.Id + "," + this.Name + "," + this.Password + "," + this.Email + "," + this.Phonenumber + "," + this.JT + "," + this.GC + "," + this.Role + "," + this.Note + ";";
        }
    }
}
