﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SanyaVsZondB
{
    public partial class MainMenuForm : Form
    {
        private System.Windows.Forms.Button buttonPlay;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;

        public MainMenuForm()
        {
            InitializeComponent();

            tabControl1 = new TabControl();
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.TabPages.Add(tabPage1 = new TabPage("Экран 1"));
            tabControl1.TabPages.Add(tabPage2 = new TabPage("Экран 2"));
            tabControl1.TabPages.Add(tabPage3 = new TabPage("Экран 3"));

            // Добавление кнопки на форму
            this.Controls.Add(buttonPlay);
            this.Controls.Add(tabControl1);

            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;

            buttonPlay.Click += new EventHandler(this.ButtonPlay_Click);
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
        }

        private void MainMenuForm_Load(object sender, EventArgs e)
        {

        }

        private void ButtonPlay_Click(object sender, EventArgs e)
        {
            var level = new Form1();
            SwitchForm(level);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                var firstTabForm = new Form1();
                firstTabForm.TopLevel = false;
                firstTabForm.FormBorderStyle = FormBorderStyle.None;
                firstTabForm.Dock = DockStyle.Fill;
                tabPage1.Controls.Add(firstTabForm);
                firstTabForm.Show();
            }
        }

        private void SwitchForm(Form newForm)
        {
            this.Hide();
            newForm.Show();
        }

        protected override void WndProc(ref Message m)
        {
            // Перехват сообщения TCM_ADJUSTRECT для скрытия вкладок
            if (m.Msg == 0x1328)
            {
                m.Result = (IntPtr)1;
            }
            else
            {
                base.WndProc(ref m);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
