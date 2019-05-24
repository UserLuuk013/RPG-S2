using System;
using RPG_IB2_WebApplication2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RPG_IB2_WebApplication2.Datalayer.TestContexts;
using RPG_IB2.Datalayer.Repositories;
using RPG_IB2_WebApplication2.Models;
using RPG_IB2_WebApplication2.Datalayer.Repositories;

namespace RPG_IB2_WebApplication_UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        ShopRepository shoprepo;
        PersonageRepository personagerepo;
        AccountTestContext accountTestContext;
        [TestInitialize]
        public void TestInitialize()
        {
            shoprepo = new ShopRepository(new ShopTestContext());
            personagerepo = new PersonageRepository(new PersonageTestContext());
            accountTestContext = new AccountTestContext();
        }
        //TestCases 01 en 02
        [TestMethod]
        public void TestMethod1()
        {

        }

        //TestCases 03 en 04
        [TestMethod]
        public void TestMethod2()
        {

        }

        //TestCases 05 en 06
        [TestMethod]
        public void TestMethod3()
        {

        }

        //TestCases 07, 08 en 09
        [TestMethod]
        public void TestMethod4()
        {
            bool x = shoprepo.KoopItem(1, "Wapen", 1000, 1);
            Assert.AreEqual(true, x);
        }

        //TestCases 10 en 11
        [TestMethod]
        public void TestMethod5()
        {
            bool x = personagerepo.UpgradePersonage(1, 1500, 1);
            Assert.AreEqual(true, x);
        }

        //TestCases 12 en 13
        [TestMethod]
        public void TestMethod6()
        {
            bool x = shoprepo.VerkoopItem(1, "Wapen", 1000, 1);
            Assert.AreEqual(true, x);
        }

        //TestCases 14 en 15
        [TestMethod]
        public void TestMethod7()
        {
            bool x = personagerepo.SelecteerPersonage(1, 1);
            Assert.AreEqual(true, x);
        }

        //TestCases 16 en 17
        [TestMethod]
        public void TestMethod8()
        {
            User user = new User(1, "UserLuuk013", "luckyluuk.6@gmail.com", "UserLuuk013!");
            accountTestContext.Registreren(user);
        }

        //TestCases 18, 19 en 20
        [TestMethod]
        public void TestMethod9()
        {
            User user = new User(1, "UserLuuk013", "luckyluuk.6@gmail.com", "UserLuuk013!");
            accountTestContext.Inloggen(user);
        }

        //TestCases 21 en 22
        [TestMethod]
        public void TestMethod10()
        {
            //Niet van toepassing
        }

        //TestCases 23 en 24
        [TestMethod]
        public void TestMethod11()
        {
            bool x = personagerepo.SelecteerPersonage(1, 1);
            Assert.AreEqual(true, x);
        }

        //TestCases 25 en 26
        [TestMethod]
        public void TestMethod12()
        {
            //Niet van toepassing
        }
    }
}
