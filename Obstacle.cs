using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;

namespace JungleMonkey
{
    public class Obstacle : IGameObject
    {
        public PictureBox Sprite { get; private set; }

        public Obstacle(Image image, int groundTop, int formWidth)
        {
            Sprite = new PictureBox
            {
                Image = image,
                Size = new Size(20, 25),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent,
                Top = groundTop - 25 + 5,
                Left = formWidth + new Random().Next(100, 300)
            };
        }

        public void Move()
        {
            Sprite.Left -= 5;
        }

        public bool CheckCollision(PictureBox other)
        {
            return Sprite.Bounds.IntersectsWith(other.Bounds);
        }

        public void Destroy()
        {
            Sprite.Dispose();
        }
    }
}