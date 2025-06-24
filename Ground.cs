using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;

namespace JungleMonkey
{
    public class GroundManager
    {
        private List<PictureBox> groundTiles = new List<PictureBox>();
        private int tileWidth;
        private int tileHeight;
        private int tileSpeed;
        private Control parent;

        public GroundManager(Image tileImage, Control parent, int tileWidth = 65, int tileHeight = 62, int tileSpeed = 10)
        {
            this.tileWidth = tileWidth;
            this.tileHeight = tileHeight;
            this.tileSpeed = tileSpeed;
            this.parent = parent;

            int tilesNeeded = (int)Math.Ceiling((double)parent.ClientSize.Width / tileWidth) + 2;

            for (int i = 0; i < tilesNeeded; i++)
            {
                PictureBox tile = new PictureBox
                {
                    Image = tileImage,
                    Size = new Size(tileWidth, tileHeight),
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Location = new Point(i * tileWidth, parent.ClientSize.Height - tileHeight),
                    BackColor = Color.Transparent
                };

                groundTiles.Add(tile);
                parent.Controls.Add(tile);
                tile.BringToFront();
            }
        }

        public void MoveGround()
        {
            foreach (PictureBox tile in groundTiles)
            {
                tile.Left -= tileSpeed;

                if (tile.Right < 0)
                {
                    int maxRight = groundTiles.Max(t => t.Right);
                    tile.Left = maxRight - 10;
                }
            }
        }

        public int GroundTop => groundTiles[0].Top;
    }
}