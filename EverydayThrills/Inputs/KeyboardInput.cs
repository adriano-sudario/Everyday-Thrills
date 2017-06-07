using EverydayThrills.Inputs.Interface;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverydayThrills.Inputs
{
    class KeyboardInput : IInput
    {
        KeyboardState currentKeyboardState;
        KeyboardState previousKeyboardState;

        Keys leftKey;
        Keys rightKey;
        Keys upKey;
        Keys downKey;

        Keys selectKey;

        Keys backKey;
        Keys startKey;

        public KeyboardInput()
        {
            SetDefaultKeysByIndex();
        }

        public void SetDefaultKeysByIndex()
        {
            leftKey = Keys.Left;
            rightKey = Keys.Right;
            upKey = Keys.Up;
            downKey = Keys.Down;

            selectKey = Keys.Space;

            backKey = Keys.Escape;
            startKey = Keys.Enter;
        }

        public void GetInputs()
        {
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
        }

        public float MoveX()
        {
            if (currentKeyboardState.IsKeyDown(leftKey))
            {
                return -1;
            }

            if (currentKeyboardState.IsKeyDown(rightKey))
            {
                return 1;
            }

            return 0;
        }

        public float MoveY()
        {
            if (currentKeyboardState.IsKeyDown(upKey))
            {
                return -1;
            }

            if (currentKeyboardState.IsKeyDown(downKey))
            {
                return 1;
            }

            return 0;
        }

        public bool Select()
        {
            throw new NotImplementedException();
        }

        public bool Start()
        {
            throw new NotImplementedException();
        }

        public bool Back()
        {
            throw new NotImplementedException();
        }
    }
}
