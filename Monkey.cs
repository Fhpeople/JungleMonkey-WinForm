using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;

namespace JungleMonkey
{
    public class Monkey : ICharacter
    {
        private PictureBox monkeySprite;
        private int jumpSpeed = 0;
        private int force = 20;
        private int gravity = 2;
        private bool isJump = false;
        private bool isOnGround = true;
        private int groundTop;

        public Monkey(Image monkeyImage, int groundTop)
        {
            this.groundTop = groundTop;

            monkeySprite = new PictureBox
            {
                Image = monkeyImage,
                Size = new Size(35, 48),
                SizeMode = PictureBoxSizeMode.StretchImage,
                Left = 100,
                Top = groundTop - 48 + 1,
                BackColor = Color.Transparent
            };
        }

        public PictureBox Sprite => monkeySprite;
        public bool IsOnGround => isOnGround;

        public void Jump()
        {
            if (isOnGround)
            {
                isJump = true;
                isOnGround = false;
                jumpSpeed = force;
            }
        }

        public void ApplyGravity()
        {
            if (isJump)
            {
                monkeySprite.Top -= jumpSpeed;
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
                monkeySprite.Top = groundTop - monkeySprite.Height + 1;
            }
        }

        public void Update() { }
    }
}