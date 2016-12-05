using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TeachMe.Domain;
using TeachMe.Infrastructure;

namespace TeachMe.App
{
    public class MainForm : Form
    {
        public MainForm(GameModel gameModel)
        {
            this.GameModel = gameModel;

            InitilizateMainPanel();
            InitilizateTopPanel();
            InitilizateCanvas();
            
            this.Resize += (sender, args) =>
            {
                this.Canvas.Invalidate();
            };
        }
        
        public GameModel GameModel { get; }

        public TableLayoutPanel MainPanel { get; private set; }
        public TableLayoutPanel TopPanel { get; private set; }
        public PictureBox Canvas { get; private set; }

        private void InitilizateMainPanel()
        {
            this.MainPanel = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill
            };

            this.MainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20f));
            this.MainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 80f));

            this.Controls.Add(this.MainPanel);
        }

        private void InitilizateTopPanel()
        {
            this.TopPanel = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill
            };
            this.TopPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            this.TopPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));

            var buttonRun = new Button()
            {
                Dock = DockStyle.Fill,
                Text = "Запуск"
            };
            buttonRun.Click += (sender, args) =>
            {
                this.GameModel.Robot.Processor.Reset();

                var timer = new Timer()
                {
                    Interval = 500
                };
                timer.Tick += (s, a) =>
                {
                    if (this.GameModel.Robot.Processor.IsFinish)
                        timer.Stop();

                    this.GameModel.Robot.Processor.RunNext();
                    this.Canvas.Invalidate();
                };
                timer.Start();
            };
            this.TopPanel.Controls.Add(buttonRun, 0, 0);

            var buttonEdit = new Button()
            {
                Dock = DockStyle.Fill,
                Text = "Редактировать"
            };
            this.TopPanel.Controls.Add(buttonEdit, 1, 0);

            this.MainPanel.Controls.Add(this.TopPanel, 0, 0);
        }

        private void InitilizateCanvas()
        {
            this.Canvas = new PictureBox()
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            this.Canvas.Paint += (sender, args) =>
            {
                var g = args.Graphics;

                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                g.TranslateTransform(0, this.Canvas.Height);
                g.ScaleTransform(1, -1);

                var sellSize = new Size(this.Canvas.Width / this.GameModel.Field.Colums, this.Canvas.Height / this.GameModel.Field.Rows);
                foreach (var rowSells in this.GameModel.Field.Sells)
                {
                    foreach (var sell in rowSells)
                    {
                        g.FillRectangle((sell.Location.X + sell.Location.Y) % 2 == 0 ? Brushes.DarkKhaki : Brushes.Bisque,
                            sell.Location.X * sellSize.Width,
                            sell.Location.Y * sellSize.Height,
                            sellSize.Width,
                            sellSize.Height);
                    }
                }

                g.TranslateTransform(this.GameModel.Robot.Transform.Location.X * sellSize.Width + sellSize.Width / 2,
                    this.GameModel.Robot.Transform.Location.Y * sellSize.Height + sellSize.Height / 2);
                g.RotateTransform((int)this.GameModel.Robot.Transform.Rotation.Angle + 90);
                g.DrawImage(TeachMe.App.Properties.Resources.Robot,
                    this.GameModel.Robot.Transform.Rotation.Angle == Angles.Left
                    || this.GameModel.Robot.Transform.Rotation.Angle == Angles.Right
                        ? new Rectangle(-sellSize.Height / 2, -sellSize.Width / 2, sellSize.Height, sellSize.Width)
                        : new Rectangle(-sellSize.Width / 2, -sellSize.Height / 2, sellSize.Width, sellSize.Height));

                g.ResetTransform();
            };

            this.MainPanel.Controls.Add(this.Canvas, 0, 1);
        }
    }
}
