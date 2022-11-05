using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMS
{
    public class StuObject
    {
        public string Stu_Id { get; set; }
        public string Stu_Name { get; set; }

        public static StuObject _CurrentStu = null;
        //应用单件模式，保存用户登录状态
        public static StuObject CurrentStu
        {
            get
            {
                if (_CurrentStu == null)
                    _CurrentStu = new StuObject();
                return _CurrentStu;
            }
        }
    }

}
