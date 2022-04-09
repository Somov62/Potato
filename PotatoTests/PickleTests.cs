using Microsoft.VisualStudio.TestTools.UnitTesting;
using Potato;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Potato.Tests
{
    [TestClass()]
    public class PickleTests
    {
        [TestMethod()]
        public void Check_ProtectionMoreThanDamage_ReturnsFalse()
        {
            //Arrange
            Pickle pickle1 = new Pickle("testEntity1", 20, 1, 10);
            Pickle pickle2 = new Pickle("testEntity2", 20, 10000, 10);
            bool excepted = false;
            //Act
            bool actual = pickle1.Attack(pickle2).Item1;
            //Assert
            Assert.AreEqual(excepted, actual);
        }

        [TestMethod()]
        public void Check_SuccessAtack_ReturnsTrueAndDamage()
        {
            //Arrange
            Pickle pickle1 = new Pickle("testEntity1", 20, 1, 10);
            Pickle pickle2 = new Pickle("testEntity2", 20, 10000, 10);
            //Act
            (bool, int) actualAtack = pickle2.Attack(pickle1);
            //Assert
            Assert.IsTrue(actualAtack.Item1 && actualAtack.Item2 > 0 && actualAtack.Item2 < 19);
        }
    }
}