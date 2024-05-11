//using NUnit.Framework;
//using SanyaVsZondB.Model;
//using System.Collections.Generic;
//using System.Drawing;
//using static System.Net.Mime.MediaTypeNames;

//namespace SanyaVsZondBTests
//{
//    [TestFixture]
//    public class ZondBTests
//    {
//        private ZondB _zondB;
//        private Player _player;
//        private List<ZondB> _zondBS;
//        private List<Flower> _flowers;
//        private Map _map;

//        [SetUp]
//        public void Setup()
//        {
//            _player = new Player(100, new Point(0, 0), 5.0, 10.0, new Point(100, 0), new Pistol(1, 1, 1, 1), new List<Bullet>(), new List<ZondB>());
//            _zondBS = new List<ZondB>();
//            _flowers = new List<Flower>();
//            _flowers.Add(new Flower(10, new Point(100, 0), 5.0, 10.0, new Point(100, 0), false, _flowers));
//            _map = new Map(1, 1, new Level(1, 1, 1, 1, _player));

//            _zondB = new ZondB(
//                100,
//                5.0,
//                10.0,
//                new Point(0, 0),
//                5,
//                _player,
//                _zondBS,
//                _flowers,
//                _map);
//        }

//        [Test]
//        public void ZondB_Hit_DamagesFlower()
//        {
//            var initialFlowerHp = _flowers[0].Hp;
//            _zondB.Hit();
//            Assert.AreEqual(initialFlowerHp - _zondB.Damage, _flowers[0].Hp);
//        }

//        [Test]
//        public void ZondB_Hit_DamagesPlayer_WhenNoFlowers()
//        {
//            _flowers.Clear();
//            var initialPlayerHp = _player.Hp;
//            _zondB.Hit();
//            Assert.AreEqual(initialPlayerHp - _zondB.Damage, _player.Hp);
//        }

//        [Test]
//        public void ZondB_MoveAlongX_UpdatesPosition()
//        {
//            _zondB.Position = new Point(90, 0);
//            var initialPosition = new Point(_zondB.Position);
//            _zondB.Move(MoveDirection.Calculated);
//            Assert.AreEqual(initialPosition.X + 5, _zondB.Position.X);
//        }

//        [Test]
//        public void ZondB_MoveAgainstX_UpdatesPosition()
//        {
//            _zondB.Position = new Point(110, 0);   
//            var initialPosition = new Point(_zondB.Position);
//            _zondB.Move(MoveDirection.Calculated);
//            Assert.AreEqual(initialPosition.X - 5, _zondB.Position.X);
//        }

//        [Test]
//        public void ZondB_MoveAlongY_UpdatesPosition()
//        {
//            _zondB.Position = new Point(100, -10);
//            var initialPosition = new Point(_zondB.Position);
//            _zondB.Move(MoveDirection.Calculated);
//            Assert.AreEqual(initialPosition.Y + 5, _zondB.Position.Y);
//        }

//        [Test]
//        public void ZondB_MoveAgainstY_UpdatesPosition()
//        {
//            _zondB.Position = new Point(100, 10);
//            var initialPosition = new Point(_zondB.Position);
//            _zondB.Move(MoveDirection.Calculated);
//            Assert.AreEqual(initialPosition.Y - 5, _zondB.Position.Y);
//        }

//        [Test]
//        public void ZondB_MoveWhenNoFlowers_UpdatesPosition()
//        {
//            _flowers.Clear();
//            _zondB.Position = new Point(90, 0);
//            var initialPosition = new Point(_zondB.Position);
//            _zondB.Move(MoveDirection.Calculated);
//            Assert.AreEqual(initialPosition.X + 5, _zondB.Position.X);
//        }

//        [Test]
//        public void ZondB_TakeDamage_DecreasesHp()
//        {
//            var initialHp = _zondB.Hp;
//            var damage = 50;
//            _zondB.TakeDamage(damage);
//            Assert.AreEqual(initialHp - damage, _zondB.Hp);
//        }

//        [Test]
//        public void ZondB_Die_RemovesFromZondBS()
//        {
//            _zondBS.Add(_zondB);
//            _zondB.Die();
//            Assert.IsFalse(_zondBS.Contains(_zondB));
//        }
//    }
//}