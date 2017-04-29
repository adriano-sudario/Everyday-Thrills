using EverydayThrills.Inputs.Interface;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EverydayThrills.Inputs
{
    class GamePadInput : IInput
    {
        public GamePadState currentGamePadState;
        public GamePadState previousGamePadState;

        public void GetInputs()
        {
            previousGamePadState = currentGamePadState;
            currentGamePadState = GamePad.GetState(PlayerIndex.One);
        }

        public float MoveX()
        {
            if (currentGamePadState.DPad.Left == ButtonState.Pressed ||
                currentGamePadState.IsButtonDown(Buttons.LeftThumbstickLeft))
            {
                return -1;
            }

            if (currentGamePadState.DPad.Right == ButtonState.Pressed ||
                currentGamePadState.IsButtonDown(Buttons.LeftThumbstickRight))
            {
                return 1;
            }

            return 0;
        }

        public float MoveY()
        {
            if (currentGamePadState.DPad.Up == ButtonState.Pressed ||
                currentGamePadState.IsButtonDown(Buttons.LeftThumbstickUp))
            {
                return -1;
            }

            if (currentGamePadState.DPad.Down == ButtonState.Pressed ||
                currentGamePadState.IsButtonDown(Buttons.LeftThumbstickDown))
            {
                return 1;
            }

            return 0;
        }

        public bool Select()
        {
            if (currentGamePadState.Buttons.A == ButtonState.Pressed && previousGamePadState.Buttons.A == ButtonState.Released)
            {
                return true;
            }

            return false;
        }

        public bool Start()
        {
            if (currentGamePadState.IsButtonDown(Buttons.Start) && previousGamePadState.IsButtonUp(Buttons.Start))
            {
                return true;
            }

            return false;
        }

        public bool Back()
        {
            if (currentGamePadState.Buttons.B == ButtonState.Pressed && previousGamePadState.Buttons.B == ButtonState.Released)
            {
                return true;
            }

            return false;
        }
    }
}
