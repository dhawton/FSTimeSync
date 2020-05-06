using FSUIPC;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace FSTimeSync
{
    public partial class Main : Form
    {
        private int hours;
        private int mins;
        private int secs;

        public Main()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void updateForm()
        {
            if (FSUIPCConnection.IsOpen)
            {
                label1.ForeColor = Color.FromArgb(0, 200, 0);
                timer1.Enabled = false;
                timer2.Enabled = true;
            } else
            {
                label1.ForeColor = Color.FromArgb(200, 0, 0);
                timer1.Enabled = true;
                timer2.Enabled = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                FSUIPCConnection.Open();
            }
            catch
            {
                ;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            String[] clock = label1.Text.Split(':');
            hours = Convert.ToInt32(clock[0]);
            mins = Convert.ToInt32(clock[1]);
            secs = Convert.ToInt32(clock[2]);

            if (secs > 0)
            {
                timer2.Interval = 500;
            }
            else
            {
                FSUIPCConnection.SendControlToFS(FsControl.ZULU_HOURS_SET, hours);
                FSUIPCConnection.SendControlToFS(FsControl.ZULU_MINUTES_SET, mins);
                FSUIPCConnection.SendControlToFS(FsControl.CLOCK_SECONDS_ZERO, 0);
                timer2.Interval = 60000;
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.UtcNow.ToString("HH:mm:ss");
        }
    }
}
