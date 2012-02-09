using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace B32Machine
{
    public partial class B32Screen : UserControl
    {
        private ushort m_ScreenMemoryLocation;
        private byte[] m_ScreenMemory;
        public ushort ScreenMemoryLocation
        {
            get
            {
                return m_ScreenMemoryLocation;
            }
            set
            {
                m_ScreenMemoryLocation = value;
            }
        }

        public B32Screen()
        {
            InitializeComponent();
            m_ScreenMemoryLocation = 0xA000;
            m_ScreenMemory = new byte[4000];

            Reset();
        }

        public void Reset()
        {
            for (int i = 0; i < 4000; i += 2)
            {
                m_ScreenMemory[i] = 32;
                m_ScreenMemory[i + 1] = 7;
            }
            Refresh();
        }

        public void Poke(ushort Address, byte Value)
        {
            ushort MemLoc;

            try
            {
                MemLoc = (ushort)(Address - m_ScreenMemoryLocation);
            }
            catch (Exception)
            {
                return;
            }

            if (MemLoc < 0 || MemLoc > 3999)
                return;

            m_ScreenMemory[MemLoc] = Value;
            Refresh();

        }

        public byte Peek(ushort Address)
        {
            ushort MemLoc;

            try
            {
                MemLoc = (ushort)(Address - m_ScreenMemoryLocation);
            }
            catch (Exception)
            {
                return (byte)0;
            }

            if (MemLoc < 0 || MemLoc > 3999)
                return (byte)0;

            return m_ScreenMemory[MemLoc];

        }

        private void B32Screen_Paint(object sender, PaintEventArgs e)
        {
            Bitmap bmp = new Bitmap(this.Width, this.Height);
            Graphics bmpGraphics = Graphics.FromImage(bmp);
            Font f = new Font("Courier New", 10f, FontStyle.Bold);
            int xLoc = 0;
            int yLoc = 0;

            for (int i = 0; i < 4000; i += 2)
            {
                SolidBrush bgBrush = null;
                SolidBrush fgBrush = null;

                ushort comparator = (ushort)Convert.ToInt16("1110000", 2);

                if ((m_ScreenMemory[i + 1] & comparator) == 112)
                {
                    bgBrush = new SolidBrush(Color.Gray);
                }
                if ((m_ScreenMemory[i + 1] & comparator) == 96)
                {
                    bgBrush = new SolidBrush(Color.Brown);
                }
                if ((m_ScreenMemory[i + 1] & comparator) == 80)
                {
                    bgBrush = new SolidBrush(Color.Magenta);
                }
                if ((m_ScreenMemory[i + 1] & comparator) == 64)
                {
                    bgBrush = new SolidBrush(Color.Red);
                }
                if ((m_ScreenMemory[i + 1] & comparator) == 48)
                {
                    bgBrush = new SolidBrush(Color.Cyan);
                }
                if ((m_ScreenMemory[i + 1] & comparator) == 32)
                {
                    bgBrush = new SolidBrush(Color.Green);
                }
                if ((m_ScreenMemory[i + 1] & comparator) == 16)
                {
                    bgBrush = new SolidBrush(Color.Blue);
                }
                if ((m_ScreenMemory[i + 1] & comparator) == 0)
                {
                    bgBrush = new SolidBrush(Color.Black);
                }

                if ((m_ScreenMemory[i + 1] & 7) == 0)
                {
                    if ((m_ScreenMemory[i + 1] & 8) == 8)
                    {
                        fgBrush = new SolidBrush(Color.Gray);
                    }
                    else
                    {
                        fgBrush = new SolidBrush(Color.Black);
                    }
                }

                if ((m_ScreenMemory[i + 1] & 7) == 1)
                {
                    if ((m_ScreenMemory[i + 1] & 8) == 8)
                    {
                        fgBrush = new SolidBrush(Color.LightBlue);
                    }
                    else
                    {
                        fgBrush = new SolidBrush(Color.Blue);

                    }

                }

                if ((m_ScreenMemory[i + 1] & 7) == 2)
                {
                    if ((m_ScreenMemory[i + 1] & 8) == 8)
                    {
                        fgBrush = new SolidBrush(Color.LightGreen);
                    }
                    else
                    {
                        fgBrush = new SolidBrush(Color.Green);
                    }
                }

                if ((m_ScreenMemory[i + 1] & 7) == 3)
                {
                    if ((m_ScreenMemory[i + 1] & 8) == 8)
                    {
                        fgBrush = new SolidBrush(Color.LightCyan);
                    }
                    else
                    {
                        fgBrush = new SolidBrush(Color.Cyan);
                    }
                }

                if ((m_ScreenMemory[i + 1] & 7) == 4)
                {
                    if ((m_ScreenMemory[i + 1] & 8) == 8)
                    {
                        fgBrush = new SolidBrush(Color.Pink);
                    }
                    else
                    {
                        fgBrush = new SolidBrush(Color.Red);
                    }
                }

                if ((m_ScreenMemory[i + 1] & 7) == 5)
                {
                    if ((m_ScreenMemory[i + 1] & 8) == 8)
                    {
                        fgBrush = new SolidBrush(Color.Fuchsia);
                    }
                    else
                    {
                        fgBrush = new SolidBrush(Color.Magenta);
                    }
                }

                if ((m_ScreenMemory[i + 1] & 7) == 6)
                {
                    if ((m_ScreenMemory[i + 1] & 8) == 8)
                    {
                        fgBrush = new SolidBrush(Color.Yellow);
                    }
                    else
                    {

                    }

                }

                if ((m_ScreenMemory[i + 1] & 7) == 7)
                {
                    if ((m_ScreenMemory[i + 1] & 8) == 8)
                    {
                        fgBrush = new SolidBrush(Color.White);
                    }
                    else
                    {
                        fgBrush = new SolidBrush(Color.Gray);
                    }
                }
                if (bgBrush == null)
                    bgBrush = new SolidBrush(Color.Black);
                if (fgBrush == null)
                    fgBrush = new SolidBrush(Color.White);

                if (((xLoc % 640) == 0) && (xLoc != 0))
                {
                    yLoc += 11;
                    xLoc = 0;
                }

                string s = System.Text.Encoding.ASCII.GetString(m_ScreenMemory, i, 1);
                PointF pf = new PointF(xLoc, yLoc);

                bmpGraphics.FillRectangle(bgBrush, xLoc + 2, yLoc + 2, 8f, 11f);
                bmpGraphics.DrawString(s, f, fgBrush, pf);
                xLoc += 8;
            }

            e.Graphics.DrawImage(bmp, new Point(0, 0));
            bmpGraphics.Dispose();
            bmp.Dispose();
        }
    }
}
