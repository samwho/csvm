using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace B32Machine
{
    public partial class MemoryDisplay : Form
    {
        private ushort bytesPerLine = 16;
        private delegate void SetupScreenCallback();
        delegate void PokeCallBack(ushort addr, byte value);

        public MemoryDisplay()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Reads the initial memory in from a file. This is waaaaay faster than generating a string from the
        /// mewmory array passed in to the constructor. It isn't necessarily scalable. If we were to increase
        /// the amount of memory that the virtual machine has, we would need to update this text file too. I can
        /// live with that for the performance increase, though.
        /// </summary>
        private void SetupScreen()
        {
            this.memoryArea.Text = System.IO.File.ReadAllText("../../initial_memory.txt");
            this.memoryArea.Select(0, 0);
        }

        public void ThreadSetupScreen()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new SetupScreenCallback(SetupScreen));
            }
            else
            {
                this.SetupScreen();
            }
        }

        public void ThreadPoke(ushort Addr, byte value)
        {
            if (this.InvokeRequired)
            {
                PokeCallBack pcb = new PokeCallBack(Poke);
                this.Invoke(pcb, new object[] { Addr, value });
            }
            else
            {
                Poke(Addr, value);
            }
        }

        private void Poke(ushort MemLoc, byte Value)
        {
            if (MemLoc < 0 || MemLoc > 65535)
                return;

            int strLoc = 5 + (MemLoc / bytesPerLine) * 7 + MemLoc * 3;
            string hexVal = Value.ToString("X").PadLeft(2, '0');
            this.memoryArea.Select(strLoc, 2);
            this.memoryArea.SelectedText = hexVal;
        }

        private void Memory_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = saveFileDialog1.ShowDialog();
            if (dr == DialogResult.Cancel)
                return;

            System.IO.File.WriteAllText(saveFileDialog1.FileName, this.memoryArea.Text);
        }
    }
}
