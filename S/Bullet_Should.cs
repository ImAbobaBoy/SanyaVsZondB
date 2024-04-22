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
    public class Bullet_Should
    {
        private Bullet _bullet;
        private List<ZondB> _zondBs;
        private List<Bullet> _bullets;
        private Player _player;
        private List<Flower> _flowers;

        [SetUp]
        public void Setup()
        {
            _zondBs = new List<ZondB>();
            _bullets = new List<Bullet>();
            _bullet = new Bullet(
                100, // HP
                new Point(10, 10), // Target
                5.0, // Speed
                10.0, // HitboxRadius
                new Point(0, 0), // Position
                50, // Damage
                _zondBs,
                _bullets);
            _player = new Player(
                100, // HP
                new Point(0, 0), // Target
                5.0, // Speed
                10.0, // HitboxRadius
                new Point(0, 0), // Position
                new Pistol(1, 1, 1, 1), //Weapon
                new List<Bullet>(), // Bullets
                new List<ZondB>()); // ZondBs
        }

        [Test]
        public void Bullet_InitialState_HasCorrectDamage()
        {
            Assert.AreEqual(50, _bullet.Damage);
        }

        [Test]
        public void Bullet_MoveUp_UpdatePosition()
        {
            _bullet = new Bullet(
                100, // HP
                new Point(10, 10), // Target
                5.0, // Speed
                10.0, // HitboxRadius
                new Point(10, 20), // Position
                50, // Damage
                _zondBs,
                _bullets);
            var initialPosition = new Point(_bullet.Position);
            _bullet.Move(MoveDirection.Calculated);
            Assert.AreEqual(initialPosition.Y - _bullet.Speed, _bullet.Position.Y);
        }

        [Test]
        public void Bullet_MoveDown_UpdatePosition()
        {
            _bullet = new Bullet(
                100, // HP
                new Point(10, 10), // Target
                5.0, // Speed
                10.0, // HitboxRadius
                new Point(10, 0), // Position
                50, // Damage
                _zondBs,
                _bullets);
            var initialPosition = new Point(_bullet.Position);
            _bullet.Move(MoveDirection.Calculated);
            Assert.AreEqual(initialPosition.Y + _bullet.Speed, _bullet.Position.Y);
        }

        [Test]
        public void Bullet_MoveLeft_UpdatePosition()
        {
            _bullet = new Bullet(
                100, // HP
                new Point(10, 10), // Target
                5.0, // Speed
                10.0, // HitboxRadius
                new Point(20, 10), // Position
                50, // Damage
                _zondBs,
                _bullets);
            var initialPosition = new Point(_bullet.Position);
            _bullet.Move(MoveDirection.Calculated);
            Assert.AreEqual(initialPosition.X - _bullet.Speed, _bullet.Position.X);
        }

        [Test]
        public void Bullet_MoveRight_UpdatePosition()
        {
            _bullet = new Bullet(
                100, // HP
                new Point(10, 10), // Target
                5.0, // Speed
                10.0, // HitboxRadius
                new Point(0, 10), // Position
                50, // Damage
                _zondBs,
                _bullets);
            var initialPosition = new Point(_bullet.Position);
            _bullet.Move(MoveDirection.Calculated);
            Assert.AreEqual(initialPosition.X + _bullet.Speed, _bullet.Position.X);
        }

        [Test]
        public void Bullet_HitZondB_DamagesZondBAndRemovesBullet()
        {
            var zondB = new ZondB(100, 5, 25, new Point(10, 10), 5, _player, _zondBs, _flowers, new Map(0, 0, new Level(0, 0, 0, 0, 0, 0)));
            _zondBs.Add(zondB);
            _bullet.HitZondB();
            Assert.IsFalse(_bullets.Contains(_bullet));
            Assert.AreEqual(50, zondB.Hp);
        }
    }
}
