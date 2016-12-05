using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TeachMe.Domain;

namespace TeachMe.App
{
    public class RedactorWindow : Form
    {
        public RedactorWindow(GameModel gameModel)
        {
            this.GameModel = gameModel;

            InitilizateMainPanel();
            InitilizateLeftPanel();
            InitilizateRightPanel();
        }

        public GameModel GameModel { get; private set; }

        public TableLayoutPanel MainPanel { get; private set; }

        private void InitilizateMainPanel()
        {
            this.MainPanel = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill
            };

            this.MainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60f));
            this.MainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40f));

            this.Controls.Add(this.MainPanel);
        }

        private void InitilizateLeftPanel()
        {

        }

        private void InitilizateRightPanel()
        {

        }
    }
}
