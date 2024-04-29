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
        private int _randomImprovementIndex;
        private int _whichOneToSwitch;
        public MovingToAnotherLevel(Game game, int whichOneToSwitch)
        {
            var rnd = new Random();
            Game = game;
            _randomImprovementIndex = rnd.Next(0, game.Improvements.Count);
            _whichOneToSwitch = whichOneToSwitch;

            InitializeComponent();

            this.Controls.Add(buttonMakeChanges1);

            buttonMakeChanges1.Click += new EventHandler(this.ButtonMakeChanges1_Click);
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
        }

        private void ButtonMakeChanges1_Click(object sender, EventArgs e)
        { 
            var improvement = Game.Improvements[_randomImprovementIndex];
            improvement.action();
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
