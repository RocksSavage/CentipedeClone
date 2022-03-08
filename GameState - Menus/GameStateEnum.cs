using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace CS5410
{
    public enum GameStateEnum
    {
        MainMenu,
        GamePlay,
        HighScores,
        Settings,
        About,
        Exit
    }

    public enum ControllerStateEnum
    {
        MoveLeft,
        MoveRight,
        MoveDown,
        MoveUp,
        Fire
    }

    public struct ControllerState
    {
        public static Keys MoveLeft = Keys.Left;
        public static Keys MoveRight = Keys.Right;
        public static Keys MoveDown = Keys.Down;
        public static Keys MoveUp = Keys.Up;
        public static Keys Fire = Keys.Space;
    }
}
