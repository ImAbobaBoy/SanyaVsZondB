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
    public class Player_Should
    {
        private Player _player;

        [SetUp]
        public void Setup()
        {
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
        public void Player_InitialState_IsAlive()
        {
            Assert.IsTrue(_player.IsAlive);
        }

        [Test]
        public void Player_MoveUp_UpdatesPosition()
        {
            var initialPosition = new Point(_player.Position);
            _player.Move(MoveDirection.Up);
            Assert.AreEqual(initialPosition.Y - _player.Speed, _player.Position.Y);
        }

        [Test]
        public void Player_MoveDown_UpdatesPosition()
        {
            var initialPosition = new Point(_player.Position);
            _player.Move(MoveDirection.Down);
            Assert.AreEqual(initialPosition.Y + _player.Speed, _player.Position.Y);
        }

        [Test]
        public void Player_MoveLeft_UpdatesPosition()
        {
            var initialPosition = new Point(_player.Position);
            _player.Move(MoveDirection.Left);
            Assert.AreEqual(initialPosition.X - _player.Speed, _player.Position.X);
        }

        [Test]
        public void Player_MoveRight_UpdatesPosition()
        {
            var initialPosition = new Point(_player.Position);
            _player.Move(MoveDirection.Right);
            Assert.AreEqual(initialPosition.X + _player.Speed, _player.Position.X);
        }

        [Test]
        public void Player_TakeDamage_DecreasesHp()
        {
            var initialHp = _player.Hp;
            var damage = 50;
            _player.TakeDamage(damage);
            Assert.AreEqual(initialHp - damage, _player.Hp);
        }

        [Test]
        public void Player_TakeDamage_KillsPlayer_WhenHpIsZero()
        {
            _player.TakeDamage(100);
            Assert.IsFalse(_player.IsAlive);
        }
    }
}
