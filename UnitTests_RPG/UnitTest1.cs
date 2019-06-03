using Microsoft.VisualStudio.TestTools.UnitTesting;
using RPG_IB2.Datalayer.Repositories;
using RPG_IB2_WebApplication2.Controllers;
using RPG_IB2_WebApplication2.Datalayer.Repositories;
using RPG_IB2_WebApplication2.Datalayer.TestContexts;
using RPG_IB2_WebApplication2.Models;

namespace UnitTests_RPG
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
        //Deze TestMethod test of het gevecht nog steeds bezig is of al voorbij is. Als de HP van Speler
        //of van CPU gelijk of lager is dan 0 is het gevecht voorbij. Als dit niet het geval is, is het
        //gevecht nog steeds bezig.
        [TestMethod]
        public void TestMethod1()
        {

        }

        //TestCases 03 en 04
        //Deze TestMethod test de hoeveelheid XP op basis van het resultaat van het gevecht. Als de Speler
        //het gevecht wint is het aantal XP maximaal. Als dit niet het geval is, is het aantal XP minimaal.
        [TestMethod]
        public void TestMethod2()
        {
            //Niet van toepassing
        }

        //TestCases 05 en 06
        //Deze TestMethod test de hoeveelheid geld op basis van het resultaat van het gevecht. Als de Speler
        //het gevecht wint is de hoeveelheid geld maximaal. Als dit niet het geval is, is de hoeveelheid geld
        //minimaal.
        [TestMethod]
        public void TestMethod3()
        {

        }

        //TestCases 07, 08 en 09
        //Deze TestMethod test of er aan de vereisten wordt voldaan om een item te kopen. Als er een item is
        //geselecteerd en er is voldoende geld aanwezig voor de aankoop is de aankoop voltooid. Als een van
        //deze vereisten ontbreekt of beide niet aanwezig zijn mislukt de aankoop.
        [TestMethod]
        public void TestMethod4()
        {
            bool x = shoprepo.KoopItem(1, "Wapen", 1000, 1);
            Assert.AreEqual(true, x);
        }

        //TestCases 10 en 11
        //Deze TestMethod test of de Speler voldoende XP heeft voor een upgrade van het personage. Als de Speler
        //voldoende XP heeft voor een volgende upgrade is de upgrade voltooid. Als dit niet het geval is
        //mislukt de upgrade.
        [TestMethod]
        public void TestMethod5()
        {
            bool x = personagerepo.UpgradePersonage(1, 1500, 1);
            Assert.AreEqual(true, x);
        }

        //TestCases 12 en 13
        //Deze TestMethod test of de Speler een item heeft geselecteerd om te verkopen. Als de Speler een item
        //heeft geselecteerd om te verkopen is de verkoop van het geselecteerde item gelukt. Als dit niet het
        //geval is, is de verkoop van het item mislukt.
        [TestMethod]
        public void TestMethod6()
        {
            bool x = shoprepo.VerkoopItem(1, "Wapen", 1000, 1);
            Assert.AreEqual(true, x);
        }

        //TestCases 14 en 15
        //Deze TestMethod test of de Speler een personage heeft geselecteerd om mee te spelen. Als de Speler een
        //personage heeft geselecteerd om mee te mee te spelen wordt het spel gestart. Als dit niet het geval is
        //wordt het spel niet gestart.
        [TestMethod]
        public void TestMethod7()
        {
            bool x = personagerepo.SelecteerPersonage(1, 1);
            Assert.AreEqual(true, x);
        }

        //TestCases 16 en 17
        //Deze TestMethod test of de Speler de vereiste gegevens heeft ingevuld om een account mee aan te maken.
        //Als de Speler de vereiste en juiste gegevens heeft ingevoerd om een account mee aan te maken is het
        //registreren voltooid. Als dit niet het geval is, is het registreren mislukt.
        [TestMethod]
        public void TestMethod8()
        {
            User user = new User(1, "UserLuuk013", "luckyluuk.6@gmail.com", "UserLuuk013!");
            bool x = accountTestContext.Registreren(user);
            Assert.AreEqual(true, x);
        }

        //TestCases 18, 19 en 20
        //Deze TestMethod test of de Speler de vereiste en juiste gegevens heeft ingevuld om mee in te loggen.
        //Als dit het geval is, is het inloggen voltooid. Als dit niet het geval is, is het inloggen mislukt.
        [TestMethod]
        public void TestMethod9()
        {
            User user = new User(1, "UserLuuk013", "luckyluuk.6@gmail.com", "UserLuuk013!");
            bool x = accountTestContext.Inloggen(user);
            Assert.AreEqual(true, x);
        }

        //TestCases 21 en 22
        [TestMethod]
        public void TestMethod10()
        {
            //Niet van toepassing
        }

        //TestCases 23 en 24
        //Deze TestMethod test of de Speler een personage heeft geselecteerd om mee te spelen. Als de Speler een
        //personage heeft geselecteerd om mee te mee te spelen wordt het spel gestart. Als dit niet het geval is
        //wordt het spel niet gestart.
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
