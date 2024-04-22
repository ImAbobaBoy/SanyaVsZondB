using NUnit.Framework;
using SanyaVsZondB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanyaVsZondBTests
{
    [TestFixture]
    public class Flower_Should
    {
        private Flower _flower;

        [SetUp]
        public void Setup()
        {
            _flower = new Flower(
                100, // HP
                new Point(0, 0), // Target
                5.0, // Speed
                10.0, // HitboxRadius
                new Point(0, 0), // Position
                true, // isFlowerAlive
                new List<Flower>()); // Flowers
        }

        [Test]
        public void Flower_InitialState_IsAlive()
        {
            Assert.IsTrue(_flower.isFlowerAlive);
        }

        [Test]
        public void Flower_TakeDamage_DecreasesHp()
        {
            var initialHp = _flower.Hp;
            _flower.TakeDamage(50);
            Assert.AreEqual(initialHp - 50, _flower.Hp);
        }

        //propety isn't used
        //[Test]
        //public void Flower_TakeDamage_KillsFlower_WhenHpIsZero()
        //{
        //    _flower.TakeDamage(100);
        //    Assert.IsFalse(_flower.isFlowerAlive);
        //}

        [Test]
        public void Flower_Die_RemovesFlowerFromList()
        {
            var flowerList = new List<Flower> { _flower };
            _flower = new Flower(100, new Point(0, 0), 5.0, 10.0, new Point(0, 0), true, flowerList);
            _flower.Die();
            Assert.IsFalse(flowerList.Contains(_flower));
        }
    }
}
