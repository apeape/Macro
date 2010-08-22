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

            Macro macro = new Macro();
            macro.Start();
        }
    }
}
