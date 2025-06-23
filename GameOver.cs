using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;

namespace JungleMonkey
{
    public class GameOver : Form
    {
        private Image BgGameOver;
        private int Score;
        private PictureBox GameOverText;
        private PictureBox RetryButton;
        private PictureBox ExitButton;

        public GameOver(int score)
        {
            this.ClientSize = new Size(500, 300);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            BgGameOver = Image.FromFile("C:\\JungleMonkey\\AssetsJungleMonkey\\BackGround\\bg.png");
            this.BackgroundImage = BgGameOver;
            this.BackgroundImageLayout = ImageLayout.Stretch;

            GameOverText = new PictureBox();
            GameOverText.Image = Image.FromFile("C:\\JungleMonkey\\AssetsJungleMonkey\\Collection\\Over.png");
            GameOverText.SizeMode = PictureBoxSizeMode.AutoSize;
            GameOverText.BackColor = Color.Transparent;
            GameOverText.Location = new Point((this.ClientSize.Width - GameOverText.Width) / 2, 30);
            this.Controls.Add(GameOverText);

            this.Score = score;
            Label labelScore = new Label();
            labelScore.Text = "Score: " + Score.ToString();
            labelScore.Font = new Font("Times New Roman", 20, FontStyle.Bold);
            labelScore.ForeColor = Color.Black;
            labelScore.AutoSize = true;
            labelScore.BackColor = Color.Transparent;
            labelScore.Location = new Point((this.ClientSize.Width - labelScore.Width) / 2, GameOverText.Bottom + 10);
            this.Controls.Add(labelScore);

            RetryButton = new PictureBox();
            RetryButton.Image = Image.FromFile("C:\\JungleMonkey\\AssetsJungleMonkey\\Collection\\Retry.png");
            RetryButton.SizeMode = PictureBoxSizeMode.AutoSize;
            RetryButton.BackColor = Color.Transparent;
            RetryButton.Location = new Point((this.ClientSize.Width / 2) - RetryButton.Width - 10, labelScore.Bottom + 10);
            RetryButton.Cursor = Cursors.Hand;
            RetryButton.Click += Retry_Click;
            this.Controls.Add(RetryButton);

            ExitButton = new PictureBox();
            ExitButton.Image = Image.FromFile("C:\\JungleMonkey\\AssetsJungleMonkey\\Collection\\Exit.png");
            ExitButton.SizeMode = PictureBoxSizeMode.AutoSize;
            ExitButton.BackColor = Color.Transparent;
            ExitButton.Location = new Point((this.ClientSize.Width / 2) + 10, labelScore.Bottom + 10);
            ExitButton.Cursor = Cursors.Hand;
            ExitButton.Click += Exit_Click;
            this.Controls.Add(ExitButton);
        }

        private void Retry_Click(object sender, EventArgs e)
        {
            Games games = new Games();
            games.Show();
            this.Close();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}