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
    public partial class FirstToSecond : Form
    {
        public Game Game {  get; private set; }
        private Button buttonMakeChanges1;
        private int _randomImprovementIndex;
        public FirstToSecond(Game game)
        {
            var rnd = new Random();
            Game = game;
            _randomImprovementIndex = rnd.Next(0, game.Improvements.Count);

            InitializeComponent();

            this.Controls.Add(buttonMakeChanges1);

            buttonMakeChanges1.Click += new EventHandler(this.ButtonMakeChanges1_Click);
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
        }

        private void ButtonMakeChanges1_Click(object sender, EventArgs e)
        {
            var level = new SecondLevelForm(Game);
            var improvement = Game.Improvements[_randomImprovementIndex];
            improvement.action();
            SwitchForm(level);
        }

        private void SwitchForm(Form newForm)
        {
            this.Hide();
            newForm.Show();
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
