using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Text;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using System.IO;

namespace CaseDomenru
{
    public static class NameValidation
    {
        public static List<string> Domains { get; set; }
        public static IdnMapping idn = new IdnMapping();
        public static bool ValidateEmail(string email)
        {
            //проверяем на наличие символа @
            if (!email.Contains('@')) return false;
            //разбиваем по частям
            string[] content = email.Split('@');
            //если получаем три части, то адрес неправильный
            if (content.Length > 2) return false;
            //убираем комментарии в адресе эл. ящика
            while (content[0].Contains('(') 
                && content[0].Contains(')'))
            {
                int index = content[0].IndexOf('(');
                int length = content[0].IndexOf(')') - index;
                content[0] = content[0].Remove(index, length);
            }
            //две точки подряд запрещены
            if (content[0].Contains("..")) return false;
            //проверяем, содержатся ли спец. символы ascii с кодом 0-21
            if (content[0].Any(x => x < 22)) return false;
            //подтвеврждаем доменную часть адреса
            return ValidateDomain(content[1]);
        }

        public static bool ValidateDomain(string domain)
        {
            //если доменная часть длиннее 255 символов, возвращем ошибку
            if (domain.Length > 255) return false;
            //разделяем на уровни
            string[] content = domain.Split('.');
            //если уровень один, то домен написан неправильно
            if (content.Length < 2) return false;
            //проверяем, нет ли пустых доменов
            foreach (string tmp_domain in content)  if (tmp_domain.Length == 0) return false;
            //ищем домен верхнего уровня среди списка существующих доменов, если есть -- адрес правильный
            foreach (string domainTocheck in Domains) if (domainTocheck.ToLower() == idn.GetAscii(content[^1].ToLower())) return true;
            return false;
        }
    }
}
