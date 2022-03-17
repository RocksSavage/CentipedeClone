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
        None, // I added this property to overcome bug,
              // preventing the "enter" keystroke dropping
              // us into the Settings View from being caught
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
        public static bool dirty = false;
    }

    public struct gameBoard
    {
        public int Left;
        public int Right;
        public int Bottom;
        public int Top;
        public int CellHeight;
        public int CellWidth;

    }
}
