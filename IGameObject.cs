using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;

namespace JungleMonkey
{
    public interface IGameObject
    {
        PictureBox Sprite { get; }
        void Move();
        bool CheckCollision(PictureBox other);
        void Destroy();
    }
}