using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;

namespace JungleMonkey
{
    public class Banana : IGameObject
    {
        public PictureBox Sprite { get; private set; }

        public Banana(Image image, int groundTop, int formWidth)
        {
            Sprite = new PictureBox
            {
                Image = image,
                Size = new Size(18, 28),
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent,
                Left = formWidth + new Random().Next(100, 300),
                Top = groundTop - 28 - new Random().Next(20, 80)
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