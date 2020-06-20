using Microsoft.VisualStudio.TestTools.UnitTesting;
using CaseDomenru;
using static CaseDomenru.NameValidation;

namespace ValidationTests
{
    [TestClass]
    public class EmailTests
    {
        string[] emails = new string[]
        {
            "‏ם#םו.7659679ילה1@עוסעמגאÿ-חמםא.נפ",
            "unna.95679med1@עוסעמגאÿ-חמםא.נפ",
            "ןמקעא@המלום.ru",
            "(..)text@המל.נפ",
            "(text)..@פו.נפ"
        };

        string[] emails_fail = new string[]
        {
            "‏םםיל5678ה1@עוסעמגאÿ-חמםאנפ",
            "unn@668med1@עוסעמגאÿ-חמםא.נפ",
            "ןמקעא@נפ",
            "ןמקעא@",
            "..@..",
            "..@.ru",
            "(..)..@נפ.נפ",
            "(text)..@.נפ"
        };

        [TestMethod]
        public void EmailPassSimple()
        {
            foreach (string email in emails)
            {
                Assert.IsTrue(ValidateEmail(email));
            }
            
        }

        [TestMethod]
        public void EmailFailSimple()
        {
            foreach (string email in emails_fail)
            {
                Assert.IsFalse(ValidateEmail(email));
            }
        }


    }

    public class DomainTests
    {
        
    }
}
