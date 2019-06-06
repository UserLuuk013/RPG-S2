using Microsoft.VisualStudio.TestTools.UnitTesting;
using RPG_IB2.Datalayer.Repositories;
using RPG_IB2.Models;
using RPG_IB2_WebApplication2.Converters;
using RPG_IB2_WebApplication2.Datalayer.Repositories;
using RPG_IB2_WebApplication2.Datalayer.TestContexts;
using RPG_IB2_WebApplication2.Models;

namespace UnitTests_RPG
{
    [TestClass]
    public class UnitTest1
    {
        private ShopRepository shoprepo;
        private PersonageRepository personagerepo;
        private AccountTestContext accountTestContext;
        private ItemViewModelConverter itemcvt;
        [TestInitialize]
        public void TestInitialize()
        {
            shoprepo = new ShopRepository(new ShopTestContext());
            personagerepo = new PersonageRepository(new PersonageTestContext());
            accountTestContext = new AccountTestContext();
            itemcvt = new ItemViewModelConverter();
        }
        //TestCases 01 en 02
        //Deze TestMethod test of het gevecht nog steeds bezig is of al voorbij is. Als de HP van Speler
        //of van CPU gelijk of lager is dan 0 is het gevecht voorbij. Als dit niet het geval is, is het
        //gevecht nog steeds bezig.
        [TestMethod]
        public void ControleerGevecht()
        {
            //Niet van toepassing
        }

        //TestCases 03 en 04
        //Deze TestMethod test de hoeveelheid XP op basis van het resultaat van het gevecht. Als de Speler
        //het gevecht wint is het aantal XP maximaal. Als dit niet het geval is, is het aantal XP minimaal.
        [TestMethod]
        public void BeloingenXP()
        {
            //Niet van toepassing
        }

        //TestCases 05 en 06
        //Deze TestMethod test de hoeveelheid geld op basis van het resultaat van het gevecht. Als de Speler
        //het gevecht wint is de hoeveelheid geld maximaal. Als dit niet het geval is, is de hoeveelheid geld
        //minimaal.
        [TestMethod]
        public void BeloningenGeld()
        {
            //Niet van toepassing
        }

        //TestCases 07, 08 en 09
        //Deze TestMethod test of er aan de vereisten wordt voldaan om een item te kopen. Als er een item is
        //geselecteerd en er is voldoende geld aanwezig voor de aankoop is de aankoop voltooid. Als een van
        //deze vereisten ontbreekt of beide niet aanwezig zijn mislukt de aankoop.
        [TestMethod]
        public void KoopItem()
        {
            bool x = shoprepo.KoopItem(1, "Wapen", 1000, 1);
            bool y = shoprepo.KoopItem(0, "Wapen", 1000, 1);
            bool z = shoprepo.KoopItem(1, "", 1000, 1);
            bool v = shoprepo.KoopItem(1, "Wapen", 250, 1);
            bool w = shoprepo.KoopItem(1, "Wapen", 1000, 0);
            Assert.AreEqual(true, x);
            Assert.AreEqual(false, y);
            Assert.AreEqual(false, z);
            Assert.AreEqual(false, v);
            Assert.AreEqual(false, w);
        }

        //TestCases 10 en 11
        //Deze TestMethod test of de Speler voldoende XP heeft voor een upgrade van het personage. Als de Speler
        //voldoende XP heeft voor een volgende upgrade is de upgrade voltooid. Als dit niet het geval is
        //mislukt de upgrade.
        [TestMethod]
        public void UpgradePersonage()
        {
            bool x = personagerepo.UpgradePersonage(1, 1500, 1);
            bool y = personagerepo.UpgradePersonage(0, 1500, 1);
            bool z = personagerepo.UpgradePersonage(1, 500, 1);
            bool w = personagerepo.UpgradePersonage(1, 1500, 0);
            Assert.AreEqual(true, x);
            Assert.AreEqual(false, y);
            Assert.AreEqual(false, z);
            Assert.AreEqual(false, w);
        }

        //TestCases 12 en 13
        //Deze TestMethod test of de Speler een item heeft geselecteerd om te verkopen. Als de Speler een item
        //heeft geselecteerd om te verkopen is de verkoop van het geselecteerde item gelukt. Als dit niet het
        //geval is, is de verkoop van het item mislukt.
        [TestMethod]
        public void VerkoopItem()
        {
            bool x = shoprepo.VerkoopItem(1, "Wapen", 1000, 1);
            bool y = shoprepo.VerkoopItem(0, "Wapen", 1000, 1);
            bool z = shoprepo.VerkoopItem(1, "", 1000, 1);
            bool w = shoprepo.VerkoopItem(1, "Wapen", 1000, 0);
            Assert.AreEqual(true, x);
            Assert.AreEqual(false, y);
            Assert.AreEqual(false, z);
            Assert.AreEqual(false, w);
        }

        //TestCases 14 en 15
        //Deze TestMethod test of de Speler een personage heeft geselecteerd om mee te spelen. Als de Speler een
        //personage heeft geselecteerd om mee te mee te spelen wordt het spel gestart. Als dit niet het geval is
        //wordt het spel niet gestart.
        [TestMethod]
        public void SelecteerPersonage()
        {
            bool x = personagerepo.SelecteerPersonage(1, 1);
            bool y = personagerepo.SelecteerPersonage(0, 1);
            Assert.AreEqual(true, x);
            Assert.AreEqual(false, y);
        }

        //TestCases 16 en 17
        //Deze TestMethod test of de Speler de vereiste gegevens heeft ingevuld om een account mee aan te maken.
        //Als de Speler de vereiste en juiste gegevens heeft ingevoerd om een account mee aan te maken is het
        //registreren voltooid. Als dit niet het geval is, is het registreren mislukt.
        [TestMethod]
        public void Registreren()
        {
            User user1 = new User(1, "UserLuuk013", "luckyluuk.6@gmail.com", "UserLuuk013!");
            User user2 = new User(1, "", "luckyluuk.6@gmail.com", "UserLuuk013!");
            User user3 = new User(1, "UserLuuk013", "luckyluuk.6gmail.com", "UserLuuk013!");
            User user4 = new User(1, "UserLuuk013", "luckyluuk.6@gmail.com", "UserLuuk!");
            bool x = accountTestContext.Registreren(user1);
            bool y = accountTestContext.Registreren(user2);
            bool z = accountTestContext.Registreren(user3);
            bool w = accountTestContext.Registreren(user4);
            Assert.AreEqual(true, x);
            Assert.AreEqual(true, y);
            Assert.AreEqual(false, z);
            Assert.AreEqual(false, w);
        }

        //TestCases 18, 19 en 20
        //Deze TestMethod test of de Speler de vereiste en juiste gegevens heeft ingevuld om mee in te loggen.
        //Als dit het geval is, is het inloggen voltooid. Als dit niet het geval is, is het inloggen mislukt.
        [TestMethod]
        public void Inloggen()
        {
            User user1 = new User(1, "UserLuuk013", "luckyluuk.6@gmail.com", "UserLuuk013!");
            User user2 = new User(1, "UserLuuk013", "luckyluuk.6gmail.com", "UserLuuk013!");
            User user3 = new User(1, "UserLuuk013", "luckyluuk.6@gmail.com", "UserLuuk013");
            bool x = accountTestContext.Inloggen(user1);
            bool y = accountTestContext.Inloggen(user2);
            bool z = accountTestContext.Inloggen(user3);
            Assert.AreEqual(true, x);
            Assert.AreEqual(false, y);
            Assert.AreEqual(false, z);
        }

        //TestCases 21 en 22
        [TestMethod]
        public void SpelSelecteren()
        {
            //Niet van toepassing
        }

        //TestCases 23 en 24
        //Deze TestMethod test of de Speler een personage heeft geselecteerd om mee te spelen. Als de Speler een
        //personage heeft geselecteerd om mee te mee te spelen wordt het spel gestart. Als dit niet het geval is
        //wordt het spel niet gestart.
        [TestMethod]
        public void SelecteerPersonage2()
        {
            bool x = personagerepo.SelecteerPersonage(1, 1);
            bool y = personagerepo.SelecteerPersonage(0, 1);
            Assert.AreEqual(true, x);
            Assert.AreEqual(false, y);
        }

        //TestCases 25 en 26
        [TestMethod]
        public void PersonaliseerItem()
        {
            //Niet van toepassing
        }
    }
}
