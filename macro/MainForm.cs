using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PInvoke;

namespace macro
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            ConsoleFunctions.AllocConsole();
            Console.CursorVisible = false;
            Console.Title = "macro console";
        }

        private void Start_Click(object sender, EventArgs e)
        {
            Macro macro = new Macro(new bool[] { Slot1.Checked, Slot2.Checked, Slot3.Checked, Slot4.Checked });
            macro.Start();
        }
    }
}
