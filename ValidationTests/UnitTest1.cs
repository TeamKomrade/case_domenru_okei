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
            "��#��.7659679���1@��������-����.��",
            "unna.95679med1@��������-����.��",
            "�����@�����.��"
        };

        string[] emails_fail = new string[]
        {
            "�����5678�1@��������-������",
            "unn@668med1@��������-����.��",
            "�����@��",
            "�����@",
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
