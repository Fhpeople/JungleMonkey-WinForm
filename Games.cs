using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace JungleMonkey
{
    public class Games : Form
    {
        private System.Windows.Forms.Timer timer;
        private PictureBox Monkey;
        private Image BgGames;
        private List<PictureBox> tileset = new List<PictureBox>();
        private Image Tiles;
        private Image monkey;
        private int score = 0;
        private Label labelScore;

        public Games()
        {
            this.ClientSize = new Size(500, 300);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            BgGames = Image.FromFile("C:\\JungleMonkey\\AssetsJungleMonkey\\Tileset\\Previewx3.png");
            this.BackgroundImage = BgGames;
            this.BackgroundImageLayout = ImageLayout.Stretch;

            Tiles = Image.FromFile("C:\\JungleMonkey\\AssetsJungleMonkey\\Tileset\\tiles.png");
            monkey = Image.FromFile("C:\\JungleMonkey\\AssetsJungleMonkey\\Monkey\\Run\\Layer 1_sprite_3(2).png");
            AddTileset();
            InitGround();
            AddMonkey();

            score = 0;
            labelScore = new Label();
            labelScore.Text = "Score: 0";
            labelScore.Font = new Font("Times New Roman", 20, FontStyle.Bold);
            labelScore.ForeColor = Color.White;
            labelScore.BackColor = Color.Transparent;
            labelScore.AutoSize = true;
            labelScore.Location = new Point(10, 10);
            this.Controls.Add(labelScore);
            labelScore.BringToFront();

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 20;
            timer.Tick += GameLoop;
            timer.Start();
        }

        private void AddTileset()
        {
            int tilesWidth = 65;
            int tilesNeeded = (this.ClientSize.Width / tilesWidth) + 2;

            for (int i = 0; i < tilesNeeded; i++)
            {
                PictureBox Tile = new PictureBox();
                Tile.Image = Tiles;
                Tile.Size = new Size(tilesWidth, 62);
                Tile.SizeMode = PictureBoxSizeMode.StretchImage;
                Tile.Left = i * 65;
                Tile.Top = this.ClientSize.Height - Tile.Height;
                Tile.BackColor = Color.Transparent;
                this.Controls.Add(Tile);
                tileset.Add(Tile);
            }
        }

        private void AddMonkey()
        {
            Monkey = new PictureBox();
            Monkey.Image = monkey;
            Monkey.Size = new Size(35, 48);
            Monkey.SizeMode = PictureBoxSizeMode.StretchImage;
            Monkey.Left = 100;
            Monkey.Top = tileset[0].Top - Monkey.Height + 1;
            Monkey.BackColor = Color.Transparent;
            this.Controls.Add(Monkey);
        }

        List<PictureBox> Ground = new List<PictureBox>();
        int tileSpeed = 10;

        private void InitGround()
        {
            int tileWidth = 62;
            int tileHeight = 65;

            for (int i = 0; i < 10; i++)
            {
                PictureBox Tiles = new PictureBox();
                Tiles.Image = Image.FromFile("C:\\JungleMonkey\\AssetsJungleMonkey\\Tileset\\tiles.png");
                Tiles.Size = new Size(tileWidth, tileHeight);
                Tiles.SizeMode = PictureBoxSizeMode.StretchImage;
                Tiles.Location = new Point(i * tileWidth, this.ClientSize.Height - tileHeight);
                Tiles.BackColor = Color.Transparent;

                Ground.Add(Tiles);
                this.Controls.Add(Tiles);
                Tiles.BringToFront();
            }
        }

        List<PictureBox> obstacles = new List<PictureBox>();
        Random rand = new Random();

        private void CreateObstacle()
        {
            PictureBox obstacle = new PictureBox ();
            obstacle.Size = new Size(20, 25);
            obstacle.BackColor = Color.Transparent;
            obstacle.Image = Image.FromFile("C:\\JungleMonkey\\AssetsJungleMonkey\\Collection\\Spike.png");
            obstacle.SizeMode = PictureBoxSizeMode.StretchImage;
            obstacle.Top = tileset[0].Top - obstacle.Height + 5;
            obstacle.Left = this.ClientSize.Width + rand.Next(100, 300);

            this.Controls.Add(obstacle);
            obstacles.Add(obstacle);
        }

        List<PictureBox> Banana = new List<PictureBox>();

        private void CreateBanana()
        {
            PictureBox banana = new PictureBox();
            banana.Image = Image.FromFile("C:\\JungleMonkey\\AssetsJungleMonkey\\Collection\\l0_sprite_07.png");
            banana.Size = new Size(18, 28);
            banana.SizeMode = PictureBoxSizeMode.StretchImage;
            banana.BackColor = Color.Transparent;
            banana.Left = this.ClientSize.Width + rand.Next(100, 300);
            banana.Top = tileset[0].Top - banana.Height - rand.Next(20, 80);

            this.Controls.Add(banana);
            Banana.Add(banana);
            banana.BringToFront();
        }

        bool isJump = false;
        bool isOnGround = true;
        int jumpSpeed = 0;
        int force = 20;
        int gravity = 2;
        int coolDown1 = 0;
        int coolDown2 = 0;

        private void GameLoop(object sender, EventArgs e)
        {
            MoveGround();
            HandleJumping();

            coolDown1--;
            if (coolDown1 <= 0)
            {
                CreateObstacle();
                coolDown1 = rand.Next(40, 80);
            }

            coolDown2--;
            if (coolDown2 <= 0)
            {
                CreateBanana();
                coolDown2 = rand.Next(60, 80);
            }

            for (int i = obstacles.Count - 1; i >= 0; i--)
            {
                obstacles[i].Left -= 5;

                if (obstacles[i].Right < 0)
                {
                    this.Controls.Remove(obstacles[i]);
                    obstacles.RemoveAt(i);
                }
                else if (Monkey.Bounds.IntersectsWith(obstacles[i].Bounds))
                {
                    GameOver();
                }
            }

            for (int i = Banana.Count - 1; i >= 0; i--)
            {
                Banana[i].Left -= 5;

                if (Banana[i].Right < 0)
                {
                    this.Controls.Remove(Banana[i]);
                    Banana.RemoveAt(i);
                }
                else if (Monkey.Bounds.IntersectsWith(Banana[i].Bounds))
                {
                    score += 1;
                    labelScore.Text = "Score: " + score.ToString();

                    this.Controls.Remove(Banana[i]);
                    Banana.RemoveAt(i);
                }
            }
        }

        private void MoveGround()
        {
            foreach (PictureBox Tiles in Ground)
            {
                Tiles.Left -= tileSpeed;

                if (Tiles.Right < 0)
                {
                    int maxRight = Ground.Max(t => t.Right);
                    Tiles.Left = maxRight;
                }
            }
        }

        private void HandleJumping()
        {
            if (isJump)
            {
                Monkey.Top -= jumpSpeed;
                jumpSpeed -= gravity;

                if (jumpSpeed <= -force)
                {
                    isJump = false;
                    isOnGround = true;
                    jumpSpeed = 0;
                }
            }
            else
            {
                Monkey.Top = tileset[0].Top - Monkey.Height + 1;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Space && isOnGround)
            {
                isJump = true;
                isOnGround = false;
                jumpSpeed = force;
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void GameOver()
        {
            timer.Stop();
            GameOver Over = new GameOver(score);
            Over.Show();
            this.Hide();
        }
    }
}