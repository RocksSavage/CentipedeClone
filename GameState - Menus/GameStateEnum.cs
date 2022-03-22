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
        public static int Left;
        public static int Right;
        public static int Bottom;
        public static int Top;
        public static int CellHeight;
        public static int CellWidth;
        public static int Height;
        public static int Width;
        public static int PlayerBarrier;
        public static int Columns;
        public static int HalfCellWidth;
        public static int HalfCellHeight;
        public static int ShroomRows;
        public static int ShroomRowSpace;
    }
}
