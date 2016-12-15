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
            GameModel = gameModel;

            InitilizateMainPanel();
            InitilizateTopPanel();
            InitilizateCanvas();
            
            this.Resize += (sender, args) =>
            {
                Canvas.Invalidate();
            };
        }
        
        public GameModel GameModel { get; }

        public TableLayoutPanel MainPanel { get; private set; }
        public TableLayoutPanel TopPanel { get; private set; }
        public PictureBox Canvas { get; private set; }

        private void InitilizateMainPanel()
        {
            MainPanel = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill
            };

            MainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 20f));
            MainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 80f));

            this.Controls.Add(MainPanel);
        }

        private void InitilizateTopPanel()
        {
            TopPanel = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill
            };
            TopPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            TopPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));

            var buttonRun = new Button()
            {
                Dock = DockStyle.Fill,
                Text = "Запуск"
            };
            buttonRun.Click += (sender, args) =>
            {
                GameModel.Robot.Processor.Reset();

                var timer = new Timer()
                {
                    Interval = 500
                };
                timer.Tick += (s, a) =>
                {
                    if (GameModel.Robot.Processor.IsFinish)
                        timer.Stop();

                    GameModel.Robot.Processor.RunNext();
                    Canvas.Invalidate();
                };
                timer.Start();
            };
            TopPanel.Controls.Add(buttonRun, 0, 0);

            var buttonEdit = new Button()
            {
                Dock = DockStyle.Fill,
                Text = "Редактировать"
            };
            TopPanel.Controls.Add(buttonEdit, 1, 0);

            MainPanel.Controls.Add(TopPanel, 0, 0);
        }

        private void InitilizateCanvas()
        {
            Canvas = new PictureBox()
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            Canvas.Paint += (sender, args) =>
            {
                var g = args.Graphics;

                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                g.TranslateTransform(0, Canvas.Height);
                g.ScaleTransform(1, -1);

                var sellSize = new Size(Canvas.Width / GameModel.Field.Colums, Canvas.Height / GameModel.Field.Rows);
                foreach (var rowSells in GameModel.Field.Sells)
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

                g.TranslateTransform(GameModel.Robot.Transform.Location.X * sellSize.Width + sellSize.Width / 2,
                    GameModel.Robot.Transform.Location.Y * sellSize.Height + sellSize.Height / 2);
                g.RotateTransform((int)GameModel.Robot.Transform.Rotation.Angle + 90);
                g.DrawImage(Properties.Resources.Robot,
                    GameModel.Robot.Transform.Rotation.Angle == Angles.Left
                    || GameModel.Robot.Transform.Rotation.Angle == Angles.Right
                        ? new Rectangle(-sellSize.Height / 2, -sellSize.Width / 2, sellSize.Height, sellSize.Width)
                        : new Rectangle(-sellSize.Width / 2, -sellSize.Height / 2, sellSize.Width, sellSize.Height));

                g.ResetTransform();
            };

            MainPanel.Controls.Add(Canvas, 0, 1);
        }
    }
}
