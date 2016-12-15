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
            GameModel = gameModel;

            InitilizateMainPanel();
            InitilizateLeftPanel();
            InitilizateRightPanel();
        }

        public GameModel GameModel { get; private set; }

        public TableLayoutPanel MainPanel { get; private set; }

        private void InitilizateMainPanel()
        {
            MainPanel = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill
            };

            MainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60f));
            MainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40f));

            this.Controls.Add(MainPanel);
        }

        private void InitilizateLeftPanel()
        {

        }

        private void InitilizateRightPanel()
        {

        }
    }
}
