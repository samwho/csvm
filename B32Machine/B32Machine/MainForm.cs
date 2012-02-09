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
    public partial class MainForm : Form
    {
        private byte[] B32Memory;
        private ushort StartAddr;
        private ushort ExecAddr;
        private ushort InstructionPointer;
        private byte Register_A;
        private byte Register_B;
        private ushort Register_X;
        private ushort Register_Y;
        private ushort Register_D;
        private byte CompareFlag;
        private ushort SpeedMS;
        private byte ProcessorFlags;

        private System.Threading.Thread prog;
        private System.Threading.ManualResetEvent PauseEvent;

        delegate void SetTextCallback(string text);
        delegate void PokeCallBack(ushort addr, byte value);

        public MainForm()
        {
            InitializeComponent();
            prog = null;
            B32Memory = new byte[65535];
            StartAddr = 0;
            ExecAddr = 0;
            Register_A = 0;
            Register_B = 0;
            Register_D = 0;
            Register_X = 0;
            Register_Y = 0;
            CompareFlag = 0;
            SpeedMS = 0;
            ProcessorFlags = 0;
            realTimeNoDelayToolStripMenuItem.Checked = true;
            resumeProgramToolStripMenuItem.Enabled = false;
            pauseProgramToolStripMenuItem.Enabled = true;
            UpdateRegisterStatus();
        }

        private void UpdateRegisterStatus()
        {
            string strRegisters = "";

            strRegisters =    "Register A      = $" + Register_A.ToString("X").PadLeft(2, '0');
            strRegisters += "\nRegister B      = $" + Register_B.ToString("X").PadLeft(2, '0');
            strRegisters += "\nRegister D      = $" + Register_D.ToString("X").PadLeft(4, '0');
            strRegisters += "\nRegister X      = $" + Register_X.ToString("X").PadLeft(4, '0');
            strRegisters += "\nRegister Y      = $" + Register_Y.ToString("X").PadLeft(4, '0');
            strRegisters += "\nInstr Ptr       = $" + InstructionPointer.ToString("X").PadLeft(4, '0');
            strRegisters += "\nCompare Flags   = b" + Convert.ToString(CompareFlag, 2).PadLeft(8, '0');
            strRegisters += "\nProcessor Flags = b" + Convert.ToString(ProcessorFlags, 2).PadLeft(8, '0');

            //this.lblRegisters.Text = strRegisters;
            if (lblRegisters.InvokeRequired)
            {
                SetTextCallback z = new SetTextCallback(SetRegisterText);
                this.Invoke(z, new object[] { strRegisters });
            }
            else
            {
                SetRegisterText(strRegisters);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            byte Magic1;
            byte Magic2;
            byte Magic3;

            DialogResult dr = openFileDialog1.ShowDialog();
            if (dr == DialogResult.Cancel) return;

            lock (b32Screen1)
            {
                b32Screen1.Reset();
            }

            System.IO.BinaryReader br;
            System.IO.FileStream fs = new System.IO.FileStream(openFileDialog1.FileName, System.IO.FileMode.Open);

            br = new System.IO.BinaryReader(fs);

            Magic1 = br.ReadByte();
            Magic2 = br.ReadByte();
            Magic3 = br.ReadByte();

            if (Magic1 != 'B' && Magic2 != '3' && Magic3 != '2')
            {
                MessageBox.Show("This is not a valid B32 file!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            StartAddr = br.ReadUInt16();
            ExecAddr = br.ReadUInt16();
            ushort Counter = 0;
            while ((br.PeekChar() != -1))
            {
                B32Memory[(StartAddr + Counter)] = br.ReadByte();
                Counter++;
            }

            br.Close();
            fs.Close();

            InstructionPointer = ExecAddr;

            pauseProgramToolStripMenuItem.Enabled = true;
            resumeProgramToolStripMenuItem.Enabled = false;

            //ExecuteProgram(ExecAddr, Counter);
            //System.Threading.Thread prog = new System.Threading.Thread(delegate() { ExecuteProgram(ExecAddr, Counter); });
            prog = new System.Threading.Thread(delegate() { ExecuteProgram(ExecAddr, Counter); });
            PauseEvent = new System.Threading.ManualResetEvent(true);
            prog.Start();
        }

        private void ThreadPoke(ushort Addr, byte value)
        {
            if (b32Screen1.InvokeRequired)
            {
                PokeCallBack pcb = new PokeCallBack(Poke);
                this.Invoke(pcb, new object[] { Addr, value });
            }
            else
            {
                Poke(Addr, value);
            }
        }

        private void Poke(ushort Addr, byte value)
        {
            lock (b32Screen1)
            {
                b32Screen1.Poke(Addr, value);
            }
        }

        private void SetRegisterText(string text)
        {
            lblRegisters.Text = text;
        }

        private void ExecuteProgram(ushort ExecAddr, ushort ProgLength)
        {
            ProgLength = 64000;
            while (ProgLength > 0)
            {
                byte Instruction = B32Memory[InstructionPointer];
                ProgLength--;
                System.Threading.Thread.Sleep(SpeedMS);
                PauseEvent.WaitOne(System.Threading.Timeout.Infinite);

                if (Instruction == 0x02) // LDX #<value>
                {
                    Register_X = (ushort)((B32Memory[(InstructionPointer + 2)]) << 8);

                    Register_X += B32Memory[(InstructionPointer + 1)];
                    ProgLength -= 2;
                    InstructionPointer += 3;

                    UpdateRegisterStatus();

                    continue;
                }

                if (Instruction == 0x01) // LDA #<value>
                {
                    Register_A = B32Memory[(InstructionPointer + 1)];
                    SetRegisterD();
                    ProgLength -= 1;
                    InstructionPointer += 2;

                    UpdateRegisterStatus();

                    continue;
                }

                if (Instruction == 0x22) // LDB #<value>
                {
                    Register_B = B32Memory[(InstructionPointer + 1)];
                    SetRegisterD();
                    ProgLength -= 1;
                    InstructionPointer += 2;

                    UpdateRegisterStatus();

                    continue;
                }

                if (Instruction == 0x23) // LDY #<value>
                {
                    Register_Y = (ushort)((B32Memory[(InstructionPointer + 2)]) << 8);

                    Register_Y += B32Memory[(InstructionPointer + 1)];
                    ProgLength -= 2;
                    InstructionPointer += 3;

                    UpdateRegisterStatus();

                    continue;

                }
                if (Instruction == 0x03) // STA ,X
                {
                    B32Memory[Register_X] = Register_A;
                    //b32Screen1.Poke(Register_X, Register_A);
                    ThreadPoke(Register_X, Register_A);
                    InstructionPointer++;
                    UpdateRegisterStatus();

                    continue;
                }
                if (Instruction == 0x04) // END
                {
                    InstructionPointer++;
                    UpdateRegisterStatus();
                    break;
                }
                if (Instruction == 0x05) // CMPA
                {
                    byte CompValue = B32Memory[InstructionPointer + 1];

                    CompareFlag = 0;

                    if (Register_A == CompValue) CompareFlag = (byte)(CompareFlag | 1);
                    if (Register_A != CompValue) CompareFlag = (byte)(CompareFlag | 2);
                    if (Register_A < CompValue) CompareFlag = (byte)(CompareFlag | 4);
                    if (Register_A > CompValue) CompareFlag = (byte)(CompareFlag | 8);

                    InstructionPointer += 2;
                    UpdateRegisterStatus();
                    continue;
                }
                if (Instruction == 0x06) // CMPB
                {
                    byte CompValue = B32Memory[InstructionPointer + 1];

                    CompareFlag = 0;

                    if (Register_B == CompValue) CompareFlag = (byte)(CompareFlag | 1);
                    if (Register_B != CompValue) CompareFlag = (byte)(CompareFlag | 2);
                    if (Register_B < CompValue) CompareFlag = (byte)(CompareFlag | 4);
                    if (Register_B > CompValue) CompareFlag = (byte)(CompareFlag | 8);

                    InstructionPointer += 2;
                    UpdateRegisterStatus();
                    continue;
                }
                if (Instruction == 0x07) //CMPX
                {
                    ushort CompValue = (ushort)((B32Memory[(InstructionPointer + 2)]) << 8);
                    CompValue += B32Memory[(InstructionPointer + 1)];

                    CompareFlag = 0;

                    if (Register_X == CompValue) CompareFlag = (byte)(CompareFlag | 1);
                    if (Register_X != CompValue) CompareFlag = (byte)(CompareFlag | 2);
                    if (Register_X < CompValue) CompareFlag = (byte)(CompareFlag | 4);
                    if (Register_X > CompValue) CompareFlag = (byte)(CompareFlag | 8);

                    InstructionPointer += 3;
                    UpdateRegisterStatus();
                    continue;
                }
                if (Instruction == 0x08) //CMPY
                {
                    ushort CompValue =
                    (ushort)((B32Memory[(InstructionPointer + 2)]) << 8);
                    CompValue += B32Memory[(InstructionPointer + 1)];

                    CompareFlag = 0;

                    if (Register_Y == CompValue) CompareFlag = (byte)(CompareFlag | 1);
                    if (Register_Y != CompValue) CompareFlag = (byte)(CompareFlag | 2);
                    if (Register_Y < CompValue) CompareFlag = (byte)(CompareFlag | 4);
                    if (Register_Y > CompValue) CompareFlag = (byte)(CompareFlag | 8);

                    InstructionPointer += 3;
                    UpdateRegisterStatus();
                    continue;
                }
                if (Instruction == 0x09) //CMPD
                {
                    ushort CompValue =
                    (ushort)((B32Memory[(InstructionPointer + 2)]) << 8);
                    CompValue += B32Memory[(InstructionPointer + 1)];

                    CompareFlag = 0;

                    if (Register_D == CompValue) CompareFlag = (byte)(CompareFlag | 1);
                    if (Register_D != CompValue) CompareFlag = (byte)(CompareFlag | 2);
                    if (Register_D < CompValue) CompareFlag = (byte)(CompareFlag | 4);
                    if (Register_D > CompValue) CompareFlag = (byte)(CompareFlag | 8);

                    InstructionPointer += 3;
                    UpdateRegisterStatus();
                    continue;
                }

                if (Instruction == 0x0A) // JMP
                {
                    ushort JmpValue = (ushort)((B32Memory[(InstructionPointer + 2)]) << 8);

                    JmpValue += B32Memory[(InstructionPointer + 1)];

                    InstructionPointer = JmpValue;

                    UpdateRegisterStatus();

                    continue;
                }
                if (Instruction == 0x0B) // JEQ
                {
                    ushort JmpValue = (ushort)((B32Memory[(InstructionPointer + 2)]) << 8);

                    JmpValue += B32Memory[(InstructionPointer + 1)];

                    if ((CompareFlag & 1) == 1)
                    {
                        InstructionPointer = JmpValue;
                    }
                    else
                    {
                        InstructionPointer += 3;
                    }
                    UpdateRegisterStatus();

                    continue;
                }
                if (Instruction == 0x0C) // JNE
                {
                    ushort JmpValue = (ushort)((B32Memory[(InstructionPointer + 2)]) << 8);

                    JmpValue += B32Memory[(InstructionPointer + 1)];

                    if ((CompareFlag & 2) == 2)
                    {
                        InstructionPointer = JmpValue;
                    }
                    else
                    {
                        InstructionPointer += 3;
                    }
                    UpdateRegisterStatus();

                    continue;
                }
                if (Instruction == 0x0D) // JGT
                {
                    ushort JmpValue = (ushort)((B32Memory[(InstructionPointer + 2)]) << 8);

                    JmpValue += B32Memory[(InstructionPointer + 1)];

                    if ((CompareFlag & 8) == 8)
                    {
                        InstructionPointer = JmpValue;
                    }
                    else
                    {
                        InstructionPointer += 3;
                    }
                    UpdateRegisterStatus();

                    continue;
                }
                if (Instruction == 0x0E) // JLT
                {
                    ushort JmpValue = (ushort)((B32Memory[(InstructionPointer + 2)]) << 8);

                    JmpValue += B32Memory[(InstructionPointer + 1)];

                    if ((CompareFlag & 4) == 4)
                    {
                        InstructionPointer = JmpValue;
                    }
                    else
                    {
                        InstructionPointer += 3;
                    }
                    UpdateRegisterStatus();

                    continue;

                }
                if (Instruction == 0x0F) // INCA
                {
                    if (Register_A == 0xFF)
                    {
                        ProcessorFlags = (byte)(ProcessorFlags | 1);
                    }
                    else
                    {
                        ProcessorFlags = (byte)(ProcessorFlags & 0xFE);
                    }

                    unchecked { Register_A++; }
                    SetRegisterD();
                    InstructionPointer++;
                    UpdateRegisterStatus();
                    continue;

                }

                if (Instruction == 0x10) // INCB
                {
                    if (Register_B == 0xFF)
                    {
                        ProcessorFlags = (byte)(ProcessorFlags | 1);
                    }
                    else
                    {
                        ProcessorFlags = (byte)(ProcessorFlags & 0xFE);
                    }

                    unchecked { Register_B++; }
                    SetRegisterD();
                    InstructionPointer++;
                    UpdateRegisterStatus();
                    continue;

                }

                if (Instruction == 0x11) // INCX
                {
                    if (Register_X == 0xFFFF)
                    {
                        ProcessorFlags = (byte)(ProcessorFlags | 1);
                    }
                    else
                    {
                        ProcessorFlags = (byte)(ProcessorFlags & 0xFE);
                    }

                    unchecked { Register_X++; }
                    InstructionPointer++;
                    UpdateRegisterStatus();
                    continue;

                }

                if (Instruction == 0x12) // INCY
                {
                    if (Register_Y == 0xFFFF)
                    {
                        ProcessorFlags = (byte)(ProcessorFlags | 1);
                    }
                    else
                    {
                        ProcessorFlags = (byte)(ProcessorFlags & 0xFE);
                    }

                    unchecked { Register_Y++; }
                    InstructionPointer++;
                    UpdateRegisterStatus();
                    continue;

                }

                if (Instruction == 0x13) // INCD
                {
                    if (Register_D == 0xFFFF)
                    {
                        ProcessorFlags = (byte)(ProcessorFlags | 1);
                    }
                    else
                    {
                        ProcessorFlags = (byte)(ProcessorFlags & 0xFE);
                    }

                    unchecked
                    {
                        Register_D++;
                        Register_A = (byte)(Register_D >> 8);
                        Register_B = (byte)(Register_D & 255);
                    }

                    InstructionPointer++;
                    UpdateRegisterStatus();
                    continue;

                }

                if (Instruction == 0x14) // DECA
                {
                    ProcessorFlags = (byte)(ProcessorFlags & 0xFE);

                    unchecked { Register_A--; }
                    SetRegisterD();
                    InstructionPointer++;
                    UpdateRegisterStatus();
                    continue;

                }

                if (Instruction == 0x15) // DECB
                {
                    ProcessorFlags = (byte)(ProcessorFlags & 0xFE);

                    unchecked { Register_B--; }
                    SetRegisterD();
                    InstructionPointer++;
                    UpdateRegisterStatus();
                    continue;

                }

                if (Instruction == 0x16) // DECX
                {
                    ProcessorFlags = (byte)(ProcessorFlags & 0xFE);

                    unchecked { Register_X--; }
                    InstructionPointer++;
                    UpdateRegisterStatus();
                    continue;

                }

                if (Instruction == 0x17) // DECY
                {
                    ProcessorFlags = (byte)(ProcessorFlags & 0xFE);
                    unchecked { Register_Y--; }
                    InstructionPointer++;
                    UpdateRegisterStatus();
                    continue;

                }

                if (Instruction == 0x18) // DECD
                {
                    ProcessorFlags = (byte)(ProcessorFlags & 0xFD);

                    unchecked
                    {
                        Register_D--;
                        Register_A = (byte)(Register_D >> 8);
                        Register_B = (byte)(Register_D & 255);
                    }

                    InstructionPointer++;
                    UpdateRegisterStatus();
                    continue;

                }

                if (Instruction == 0x19) // ROLA
                {
                    byte OldCarryFlag = (byte)(ProcessorFlags & 2);

                    if ((Register_A & 128) == 128)
                    {
                        ProcessorFlags = (byte)(ProcessorFlags | 2);
                    }
                    else
                    {
                        ProcessorFlags = (byte)(ProcessorFlags & 0xFD);
                    }
                    Register_A = (byte)(Register_A << 1);

                    if (OldCarryFlag > 0)
                    {
                        Register_A = (byte)(Register_A | 1);
                    }

                    SetRegisterD();
                    InstructionPointer++;
                    UpdateRegisterStatus();
                    continue;

                }

                if (Instruction == 0x1A) // ROLB
                {
                    byte OldCarryFlag = (byte)(ProcessorFlags & 2);
                    if ((Register_B & 128) == 128)
                    {
                        ProcessorFlags = (byte)(ProcessorFlags | 2);
                    }
                    else
                    {
                        ProcessorFlags = (byte)(ProcessorFlags & 0xFD);
                    }
                    Register_B = (byte)(Register_B << 1);

                    if (OldCarryFlag > 0)
                    {
                        Register_B = (byte)(Register_B | 1);
                    }

                    SetRegisterD();
                    InstructionPointer++;
                    UpdateRegisterStatus();
                    continue;

                }

                if (Instruction == 0x1B) // RORA
                {
                    byte OldCarryFlag = (byte)(ProcessorFlags & 2);

                    if ((Register_A & 1) == 1)
                    {
                        ProcessorFlags = (byte)(ProcessorFlags | 2);
                    }
                    else
                    {
                        ProcessorFlags = (byte)(ProcessorFlags & 0xFD);
                    }
                    Register_A = (byte)(Register_A >> 1);

                    if (OldCarryFlag > 0)
                    {
                        Register_A = (byte)(Register_A | 128);
                    }

                    SetRegisterD();
                    InstructionPointer++;
                    UpdateRegisterStatus();
                    continue;

                }

                if (Instruction == 0x1C) // RORB
                {
                    byte OldCarryFlag = (byte)(ProcessorFlags & 2);

                    if ((Register_B & 1) == 1)
                    {
                        ProcessorFlags = (byte)(ProcessorFlags | 2);
                    }
                    else
                    {
                        ProcessorFlags = (byte)(ProcessorFlags & 0xFD);
                    }
                    Register_B = (byte)(Register_B >> 1);

                    if (OldCarryFlag > 0)
                    {
                        Register_B = (byte)(Register_B | 128);
                    }

                    SetRegisterD();
                    InstructionPointer++;
                    UpdateRegisterStatus();
                    continue;

                }

                if (Instruction == 0x1D) // ADCA
                {
                    if ((byte)(ProcessorFlags & 2) == 2)
                    {
                        if (Register_A == 0xFF)
                        {
                            ProcessorFlags = (byte)(ProcessorFlags | 1);
                        }
                        else
                        {
                            ProcessorFlags = (byte)(ProcessorFlags & 0xFE);
                        }

                        unchecked { Register_A++; }
                        SetRegisterD();

                    }
                    InstructionPointer++;
                    UpdateRegisterStatus();
                    continue;

                }

                if (Instruction == 0x1E) // ADCB
                {
                    if ((byte)(ProcessorFlags & 2) == 2)
                    {
                        if (Register_B == 0xFF)
                        {
                            ProcessorFlags = (byte)(ProcessorFlags | 1);
                        }
                        else
                        {
                            ProcessorFlags = (byte)(ProcessorFlags & 0xFE);
                        }
                        unchecked { Register_B++; }
                        SetRegisterD();

                    }
                    InstructionPointer++;
                    UpdateRegisterStatus();
                    continue;

                }

                if (Instruction == 0x1F) // ADDA
                {
                    byte val = B32Memory[(InstructionPointer + 1)];

                    if (Register_A == 0xFF && val > 0)
                    {
                        ProcessorFlags = (byte)(ProcessorFlags | 1);
                    }
                    else
                    {
                        ProcessorFlags = (byte)(ProcessorFlags & 0xFE);
                    }

                    unchecked { Register_A += val; }
                    SetRegisterD();

                    InstructionPointer += 2;
                    UpdateRegisterStatus();
                    continue;

                }

                if (Instruction == 0x20) // ADDB
                {
                    byte val = B32Memory[(InstructionPointer + 1)];

                    if (Register_B == 0xFF && val > 0)
                    {
                        ProcessorFlags = (byte)(ProcessorFlags | 1);
                    }
                    else
                    {
                        ProcessorFlags = (byte)(ProcessorFlags & 0xFE);
                    }

                    unchecked { Register_B += val; }
                    SetRegisterD();

                    InstructionPointer += 2;
                    UpdateRegisterStatus();
                    continue;

                }

                if (Instruction == 0x21) // ADDAB
                {
                    if ((255 - Register_A) > (Register_B))
                    {
                        ProcessorFlags = (byte)(ProcessorFlags | 1);
                    }

                    ProcessorFlags = (byte)(ProcessorFlags & 0xFE);

                    unchecked
                    {
                        Register_D = (ushort)(((ushort)Register_B) +
                            ((ushort)Register_A));
                    }

                    Register_A = (byte)(Register_D >> 8);
                    Register_B = (byte)(Register_D & 255);

                    InstructionPointer++;
                    UpdateRegisterStatus();
                    continue;
                }
            }
        }

        private void SetRegisterD()
        {
            Register_D = (ushort)(Register_A << 8 + Register_B);
        }

        private void mS14SecondToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UncheckAll();
            ((ToolStripMenuItem)sender).Checked = true;
            SpeedMS = 250;
        }

        private void mS12SecondToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UncheckAll();
            ((ToolStripMenuItem)sender).Checked = true;
            SpeedMS = 500;
        }

        private void mS1SecondToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UncheckAll();
            ((ToolStripMenuItem)sender).Checked = true;
            SpeedMS = 1000;
        }

        private void mS2SecondToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UncheckAll();
            ((ToolStripMenuItem)sender).Checked = true;
            SpeedMS = 2000;
        }

        private void mS3SecondToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UncheckAll();
            ((ToolStripMenuItem)sender).Checked = true;
            SpeedMS = 3000;
        }

        private void mS4SecondToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UncheckAll();
            ((ToolStripMenuItem)sender).Checked = true;
            SpeedMS = 4000;
        }

        private void mS5SecondToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UncheckAll();
            ((ToolStripMenuItem)sender).Checked = true;
            SpeedMS = 5000;
        }

        private void realTimeNoDelayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UncheckAll();
            ((ToolStripMenuItem)sender).Checked = true;
            SpeedMS = 0;
        }

        private void UncheckAll()
        {
            mS12SecondToolStripMenuItem.Checked = false;
            mS14SecondToolStripMenuItem.Checked = false;
            mS1SecondToolStripMenuItem.Checked = false;
            mS2SecondToolStripMenuItem.Checked = false;
            mS3SecondToolStripMenuItem.Checked = false;
            mS4SecondToolStripMenuItem.Checked = false;
            mS5SecondToolStripMenuItem.Checked = false;
            realTimeNoDelayToolStripMenuItem.Checked = false;
        }

        private void pauseProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            resumeProgramToolStripMenuItem.Enabled = true;
            pauseProgramToolStripMenuItem.Enabled = false;
            PauseEvent.Reset();
        }

        private void resumeProgramToolStripMenuItem_Click(object sender, EventArgs e)
        {
            resumeProgramToolStripMenuItem.Enabled = false;
            pauseProgramToolStripMenuItem.Enabled = true;
            PauseEvent.Set();
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (prog != null)
            {
                prog.Abort();
                prog = null;

            }
            InstructionPointer = ExecAddr;

            resumeProgramToolStripMenuItem.Enabled = false;
            pauseProgramToolStripMenuItem.Enabled = true;

            prog = new System.Threading.Thread(delegate() { ExecuteProgram(ExecAddr, 64000); });
            PauseEvent = new System.Threading.ManualResetEvent(true);
            b32Screen1.Reset();
            prog.Start();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Kill the program running thread when closing the window.
            this.prog.Abort();
        }
    }
}