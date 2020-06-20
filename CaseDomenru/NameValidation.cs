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
            string tmp_email = email;
            if (!email.Contains('@')) return false;
            string[] content = email.Split('@');
            if (content.Length > 2 && !content[0].Any(x => x > 21)) return false;
            return ValidateDomain(content[1]);
        }

        public static bool ValidateDomain(string domain)
        {
            if (domain.Length > 255) return false;
            string[] content = domain.Split('.');
            if (content.Length < 2) return false;
            foreach (string tmp_domain in content)  if (tmp_domain.Length == 0) return false;
            foreach (string domainTocheck in Domains) if (domainTocheck.ToLower() == idn.GetAscii(content[^1].ToLower())) return true;
            return false;
        }
    }
}
