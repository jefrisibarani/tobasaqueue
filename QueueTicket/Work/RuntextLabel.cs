using System;
using System.Windows.Forms;
using System.Drawing;

namespace Tobasa
{
    class RuntextLabel : Label
    {
        private int CurrentPosition { get; set; }
        private Timer Timer { get; set; }
        SizeF stringSize = new SizeF();
        float initialWidth = 0;
        float leftLimit = 0;
        public Timer timer = null;

        public RuntextLabel()
        {
            UseCompatibleTextRendering = true;
            CurrentPosition = 0;

            timer = new Timer();
            timer.Interval = 25;
            timer.Tick += new EventHandler(Timer_Tick);
            timer.Start();
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            if (CurrentPosition <= (int)leftLimit)
            {
                CurrentPosition = Parent.Width;
            }
            else
            {
                CurrentPosition -= 1;
            }

            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (initialWidth == 0)
            {
                // stringSize = e.Graphics.MeasureString(this.Text, this.Font);
                stringSize = TextRenderer.MeasureText(Text, Font);
                initialWidth = stringSize.Width;
                leftLimit = -initialWidth;
            }

            e.Graphics.TranslateTransform(CurrentPosition, 0);
            base.OnPaint(e);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Timer != null)
                    Timer.Dispose();
            }
            Timer = null;
        }
    }
}
