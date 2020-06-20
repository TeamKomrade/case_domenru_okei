using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Text;

namespace CaseDomenru
{
    public static class NameValidation
    {
        //Encoding Uni = Encoding.Unicode;
        //
        public static bool ValidateEmail(string email)
        {
            string tmp_email = email;
            //1. делим вход, проверяем, является ли входная строка эл. почтой (правильной или нет)
            //if (tmp_email.Contains("(") tmp_email = tmp_email.S
            string[] content = email.Split('@');
            if (!email.Contains('@') || content.Length > 2) return false;
            if (!content[0].Any(x => x > 21)) return false;
            //2. подтверждаем домен
            return ValidateDomain(content[1]);
        }

        public static bool ValidateDomain(string domain)
        {
            //1. проверяем, является
            string[] content = domain.Split('.');
            if (content.Length < 2) return false;
            
            return (content[0].Length > 0) && (content[1].Length > 0);
        }
    }
}
