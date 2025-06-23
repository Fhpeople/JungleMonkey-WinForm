using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace JungleMonkey
{
    public class MainMenu : Form
    {
        private Image BgMainMenu;
        private PictureBox PlayButton;

        public MainMenu()
        {
            this.ClientSize = new Size(500, 300);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            BgMainMenu = Image.FromFile("C:\\JungleMonkey\\AssetsJungleMonkey\\Tileset\\Previewx3.png");
            this.BackgroundImage = BgMainMenu;
            this.BackgroundImageLayout = ImageLayout.Stretch;

            PlayButton = new PictureBox();
            PlayButton.Image = Image.FromFile("C:\\JungleMonkey\\AssetsJungleMonkey\\Collection\\play.png");
            PlayButton.SizeMode = PictureBoxSizeMode.AutoSize;
            PlayButton.BackColor = Color.Transparent;
            PlayButton.Location = new Point((this.ClientSize.Width - PlayButton.Width) / 2, 100);
            PlayButton.Cursor = Cursors.Hand;
            PlayButton.Click += Play_Click;
            this.Controls.Add(PlayButton);
        }

        private void Play_Click(object sender, EventArgs e)
        {
            Games games = new Games();
            games.Show();
            this.Hide();
        }
    }
}