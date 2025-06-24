using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;

namespace JungleMonkey
{
    public interface ICharacter
    {
        void Jump();
        void ApplyGravity();
        void Update();
        bool IsOnGround { get; }
        PictureBox Sprite { get; }
    }
}