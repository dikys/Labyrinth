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
            var table = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill
            };
            
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 20f));
            table.RowStyles.Add(new RowStyle(SizeType.Percent, 80f));
            
            var mainPanel = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill
            };
            table.Controls.Add(mainPanel, 0, 0);
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            var buttonRun = new Button()
            {
                Dock = DockStyle.Fill,
                Text = "Запуск"
            };
            var buttonEdit = new Button()
            {
                Dock = DockStyle.Fill,
                Text = "Редактировать"
            };
            mainPanel.Controls.Add(buttonRun, 0, 0);
            mainPanel.Controls.Add(buttonEdit, 1, 0);

            var canvas = new PictureBox()
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            canvas.Paint += (sender, args) =>
            {
                var g = args.Graphics;
                
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                g.TranslateTransform(0, canvas.Height);
                g.ScaleTransform(1, -1);

                var sellSize = new Size(canvas.Width / gameModel.Field.Colums, canvas.Height / gameModel.Field.Rows);
                foreach (var rowSells in gameModel.Field.Sells)
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

                g.TranslateTransform(gameModel.Robot.Transform.Location.X*sellSize.Width + sellSize.Width / 2,
                    gameModel.Robot.Transform.Location.Y*sellSize.Height + sellSize.Height / 2);
                g.RotateTransform((int)gameModel.Robot.Transform.Rotation.Angle + 90);
                g.DrawImage(TeachMe.App.Properties.Resources.Robot,
                    gameModel.Robot.Transform.Rotation.Angle == Angles.Left
                    || gameModel.Robot.Transform.Rotation.Angle == Angles.Right
                        ? new Rectangle(-sellSize.Height / 2, -sellSize.Width / 2, sellSize.Height, sellSize.Width)
                        : new Rectangle(-sellSize.Width / 2, -sellSize.Height/2, sellSize.Width, sellSize.Height));

                g.ResetTransform();
            };
            table.Controls.Add(canvas, 0, 1);

            buttonRun.Click += (sender, args) =>
            {
                gameModel.Robot.Processor.Reset();

                var timer = new Timer()
                {
                    Interval = 500
                };
                timer.Tick += (s, a) =>
                {
                    if (gameModel.Robot.Processor.IsFinish)
                        timer.Stop();

                    gameModel.Robot.Processor.RunNext();
                    canvas.Invalidate();
                };
                timer.Start();
            };
            this.ResizeEnd += (sender, args) =>
            {
                //this.Size = new Size((int)(this.Size.Height * 0.8), this.Size.Height);

                canvas.Invalidate();
            };

            this.Controls.Add(table);
        }
    }
}
