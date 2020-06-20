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
            "юн#не.7659679ймд1@тестовая-зона.рф",
            "unna.95679med1@тестовая-зона.рф",
            "почта@домен.ру"
        };

        string[] emails_fail = new string[]
        {
            "юннйм5678д1@тестовая-зонарф",
            "unn@668med1@тестовая-зона.рф",
            "почта@рф",
            "почта@",
        };

        [TestMethod]
        public void EmailSimple()
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
