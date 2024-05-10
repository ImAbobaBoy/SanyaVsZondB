using SanyaVsZondB.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SanyaVsZondB.View
{
    public partial class MovingToAnotherLevel : Form
    {
        public Game Game {  get; private set; }
        private Button buttonMakeChanges1;
        private Button buttonMakeChanges2;
        private Button buttonMakeChanges3;
        private int _randomPlayerImprovementIndexFirst;
        private int _randomPlayerImprovementIndexSecond;
        private int _randomPlayerImprovementIndexThird;
        private int _randomZondBImprovementIndexFirst;
        private int _randomZondBImprovementIndexSecond;
        private int _randomZondBImprovementIndexThird;
        private int _whichOneToSwitch;
        public MovingToAnotherLevel(Game game, int whichOneToSwitch)
        {
            var rnd = new Random();
            Game = game;
            _randomPlayerImprovementIndexFirst = rnd.Next(0, game.PlayerImprovements.Count);
            _randomZondBImprovementIndexFirst = rnd.Next(0, game.ZondBImprovements.Count);

            _randomPlayerImprovementIndexSecond = rnd.Next(0, game.PlayerImprovements.Count);
            _randomZondBImprovementIndexSecond = rnd.Next(0, game.ZondBImprovements.Count);

            _randomPlayerImprovementIndexThird = rnd.Next(0, game.PlayerImprovements.Count);
            _randomZondBImprovementIndexThird = rnd.Next(0, game.ZondBImprovements.Count);

            _whichOneToSwitch = whichOneToSwitch;

            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;

            this.Controls.Add(buttonMakeChanges1);
            this.Controls.Add(buttonMakeChanges2);
            this.Controls.Add(buttonMakeChanges3);

            buttonMakeChanges1.Click += new EventHandler(this.ButtonMakeChanges1_Click);
            buttonMakeChanges2.Click += new EventHandler(this.ButtonMakeChanges2_Click);
            buttonMakeChanges3.Click += new EventHandler(this.ButtonMakeChanges3_Click);
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
        }

        private void ButtonMakeChanges1_Click(object sender, EventArgs e)
        { 
            var playerImprovement = Game.PlayerImprovements[_randomPlayerImprovementIndexFirst];
            var zondBImprovement = Game.ZondBImprovements[_randomZondBImprovementIndexFirst];
            playerImprovement.action();
            zondBImprovement.action();
            SwitchForm(_whichOneToSwitch);
        }

        private void ButtonMakeChanges2_Click(object sender, EventArgs e)
        {
            var playerImprovement = Game.PlayerImprovements[_randomPlayerImprovementIndexSecond];
            var zondBImprovement = Game.ZondBImprovements[_randomZondBImprovementIndexSecond];
            playerImprovement.action();
            zondBImprovement.action();
            SwitchForm(_whichOneToSwitch);
        }

        private void ButtonMakeChanges3_Click(object sender, EventArgs e)
        {
            var playerImprovement = Game.PlayerImprovements[_randomPlayerImprovementIndexThird];
            var zondBImprovement = Game.ZondBImprovements[_randomZondBImprovementIndexThird];
            playerImprovement.action();
            zondBImprovement.action();
            SwitchForm(_whichOneToSwitch);
        }

        private void SwitchForm(int nextLevel)
        {
            this.Hide();
            Form nextForm;
            switch (nextLevel)
            {
                case 2:
                    nextForm = new LevelForm(Game, 2);
                    break;
                case 3:
                    nextForm = new LevelForm(Game, 3);
                    break;
                case 4:
                    nextForm = new LevelForm(Game, 4);
                    break;
                case 5:
                    nextForm = new LevelForm(Game, 5);
                    break;
                default:
                    nextForm = null;
                    break;
            }
            nextForm.Show();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void FirstToSecond_Load(object sender, EventArgs e)
        {

        }
    }
}
