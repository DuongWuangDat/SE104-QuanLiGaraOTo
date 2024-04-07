using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QuanLiGaraOto.Utils
{
    public class Helper
    {
        public static bool IsPhoneNumber(string phoneNumber)
        {
            string regex = @"^0\d{9-10}$";
            return Regex.Match(phoneNumber,regex).Success;
        }

        public static bool IsEmail(string email)
        {
            string regex = @"^[\w]+@([\w]+\.)+[\w]{2,}$";
            return Regex.Match(email, regex).Success;
        }
    }
}
