using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Практическая_номер_18.ModelsDB;

namespace Практическая_номер_18
{
    class DataBaseSession
    {
        public static SessionFirstCourseBt? sessionFirstCourseBt;
        public static bool Login = false;
        public static string UserSurname;
        public static string UserName;
        public static string UserPatronymic;
        public static string Right;
    }
    public static class FlagsForForm
    {
        public static bool FlagAdd { get; set; } 
        public static bool FlagEdit { get; set; } =false;
        public static bool FlagView { get; set; } = false;
    }
    public static class Data
    {

    }
}
