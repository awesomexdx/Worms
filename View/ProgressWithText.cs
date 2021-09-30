using System;
using System.Drawing;
using System.Windows.Forms;

namespace View
{
    internal class ProgressWithText : Control
    {
        public enum FsProgressTextType
        {
            AsIs, Percent, ShowText
        }

        //Cal when Value was changed
        public event EventHandler ValueChanged;
        //Call when Value == MaxValue
        public event EventHandler ValuesIsMaximum;

        private int minValue;
        public int MinValue
        {
            get => minValue;
            set
            {
                if (value >= MaxValue)
                {
                    throw new Exception("MinValue must be less than MaxValue");
                }
                minValue = value;
                Invalidate();
            }
        }

        private int maxValue;
        public int MaxValue
        {
            get => maxValue;
            set
            {
                if (value <= MinValue)
                {
                    throw new Exception("MaxValue must be more than MinValue");
                }
                maxValue = value;
                Invalidate();
            }
        }

        private int value;
        public int Value
        {
            get => value;
            set
            {
                if (value < MinValue || value > maxValue)
                {
                    throw new Exception("Value must be between MinValue and MaxValue");
                }
                this.value = value;
                if (this.value == MaxValue && ValuesIsMaximum != null)
                {
                    ValuesIsMaximum(this, new EventArgs());
                }
                if (ValueChanged != null)
                {
                    ValueChanged(this, new EventArgs());
                }
                Invalidate();
            }
        }

        private int borderWidth;
        public int BorderWidth
        {
            get => borderWidth;
            set
            {
                if (value < 0)
                {
                    throw new Exception("Border width can not be negative");
                }
                borderWidth = value;
            }
        }

        public System.Drawing.Color BorderColor { get; set; }
        public System.Drawing.Color ProgressColor { get; set; }
        public bool ShowProgressText { get; set; }
        public FsProgressTextType ProgressTextType { get; set; }

        public ProgressWithText()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            minValue = 0;
            maxValue = 100;
            value = 0;
            BorderWidth = 0;
            BorderColor = Color.Black;
            BackColor = SystemColors.Control;
            ForeColor = Color.Black;
            ProgressColor = Color.Yellow;
            ShowProgressText = true;
            Paint += new PaintEventHandler(FsProgressBar_Paint);
            Size = new Size(200, 30);
            ProgressTextType = FsProgressTextType.AsIs;
        }

        protected void FsProgressBar_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(BackColor), new Rectangle(0, 0, Width, Height));
            e.Graphics.FillRectangle(new SolidBrush(ProgressColor), new Rectangle(0, 0, (Value * Width) / MaxValue, Height));
            if (BorderWidth > 0)
            {
                e.Graphics.DrawRectangle(new Pen(BorderColor, BorderWidth), DisplayRectangle);
            }
            if (ShowProgressText)
            {
                string text = string.Empty;
                switch (ProgressTextType)
                {
                    case FsProgressTextType.ShowText:
                        text = Text;
                        break;
                    case FsProgressTextType.AsIs:
                        text = Value + " / " + MaxValue;
                        break;
                    case FsProgressTextType.Percent:
                        text = ((Value * 100) / MaxValue).ToString() + "%";
                        break;
                }
                System.Drawing.SizeF size = e.Graphics.MeasureString(text, Font);
                e.Graphics.DrawString(text, Font, new SolidBrush(ForeColor), new PointF(Width / 2 - size.Width / 2, Height / 2 - size.Height / 2));
            }
        }
    }
}