using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ExpressionMouse
{

    public struct Position
    {
        public int X;
        public int Y;
    }

    public class MouseControl
    {
        [DllImport("user32.dll", EntryPoint = "SendInput", SetLastError = true)]
        static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        [DllImport("user32.dll", EntryPoint = "GetMessageExtraInfo", SetLastError = true)]
        static extern IntPtr GetMessageExtraInfo();

        private enum InputType
        {
            INPUT_MOUSE = 0,
            INPUT_KEYBOARD = 1,
            INPUT_HARDWARE = 2,
        }

        [Flags()]
        private enum MOUSEEVENTF
        {
            MOVE = 0x0001,  // mouse move 
            LEFTDOWN = 0x0002,  // left button down
            LEFTUP = 0x0004,  // left button up
            RIGHTDOWN = 0x0008,  // right button down
            RIGHTUP = 0x0010,  // right button up
            MIDDLEDOWN = 0x0020,  // middle button down
            MIDDLEUP = 0x0040,  // middle button up
            XDOWN = 0x0080,  // x button down 
            XUP = 0x0100,  // x button down
            WHEEL = 0x0800,  // wheel button rolled
            VIRTUALDESK = 0x4000,  // map to entire virtual desktop
            ABSOLUTE = 0x8000,  // absolute move
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public int mouseData;
            public MOUSEEVENTF dwFlags;
            public int time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct INPUT
        {
            public InputType type;
            public MOUSEINPUT mi;
        }
        /// <summary>
        /// Diese Funktion bewegt den Mauscursor an einen bestimmten Punkt.
        /// </summary>
        /// <param name="x">X Koordinate der Position als absoluter Pixelwert</param>
        /// <param name="y">Y Koordinate der Position als absoluter Pixelwert</param>
        /// <returns>Liefert 1 bei Erfolg und 0, wenn der Eingabestream schon blockiert war zurück.</returns>
        public static uint Move(int x, int y)
        {
            // Bildschirm Auflösung
            float ScreenWidth = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            float ScreenHeight = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;

            INPUT input_move = new INPUT();
            input_move.type = InputType.INPUT_MOUSE;

            input_move.mi.dx = (int)Math.Round(x * (65535 / ScreenWidth), 0);
            input_move.mi.dy = (int)Math.Round(y * (65535 / ScreenHeight), 0);
            input_move.mi.mouseData = 0;
            input_move.mi.dwFlags = (MOUSEEVENTF.MOVE | MOUSEEVENTF.ABSOLUTE);
            input_move.mi.time = 0;
            input_move.mi.dwExtraInfo = GetMessageExtraInfo();

            INPUT[] input = { input_move };
            return SendInput(1, input, Marshal.SizeOf(input_move));
        }

        public static void SmoothMove(int x, int y)
        {
            // Bildschirm Auflösung
            float ScreenWidth = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            float ScreenHeight = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;

            Position target = new Position();
            target.X = x;
            target.Y = y;
            Position currentPos = CurrentMousePos();

            Position stepSize = new Position();
            Position stepCount = new Position();
            stepCount.Y = Math.Abs(target.Y - currentPos.Y);
            stepCount.X = Math.Abs(target.X - currentPos.X);
            if (stepCount.X > stepCount.Y)
            {
                stepSize.Y = 1;
                if (stepCount.Y == 0)
                    stepSize.X = 1;
                else
                    stepSize.X = (stepCount.X / stepCount.Y);
            }
            else if (stepCount.Y > stepCount.X)
            {
                stepSize.X = 1;
                if (stepCount.X == 0)
                    stepSize.X = 1;
                else
                    stepSize.Y = (stepCount.Y / stepCount.X);
            }
            else if (stepCount.X == stepCount.Y)
            {
                stepSize.X = 1;
                stepSize.Y = 1;
            }

            while (currentPos.X != target.X && currentPos.Y != target.Y)
            {
                currentPos.X += stepSize.X;
                currentPos.Y += stepSize.Y;

                INPUT input_move = new INPUT();
                input_move.type = InputType.INPUT_MOUSE;
                input_move.mi.dx = (int)Math.Round(currentPos.X * (65535 / ScreenWidth), 0);
                input_move.mi.dy = (int)Math.Round(currentPos.Y * (65535 / ScreenHeight), 0);
                input_move.mi.mouseData = 0;
                input_move.mi.dwFlags = (MOUSEEVENTF.MOVE | MOUSEEVENTF.ABSOLUTE);
                input_move.mi.time = 0;
                input_move.mi.dwExtraInfo = GetMessageExtraInfo();
                INPUT[] input = { input_move };
                SendInput(1, input, Marshal.SizeOf(input_move));
            }
        }

        /// <summary>
        /// Diese Funktion simuliert einen einfach Mausklick mit der linken Maustaste an der aktuellen Cursurposition.
        /// </summary>
        /// <returns>Liefert 2 zurück, wenn beide Aktionen (Maus down und Maus up) erfolgreich waren. Andernfalls 1 oder 0.</returns>
        public static uint Click()
        {
            INPUT input_down = new INPUT();
            input_down.type = InputType.INPUT_MOUSE;

            input_down.mi.dx = 0;
            input_down.mi.dy = 0;
            input_down.mi.mouseData = 0;
            input_down.mi.dwFlags = MOUSEEVENTF.LEFTDOWN;
            input_down.mi.time = 0;
            input_down.mi.dwExtraInfo = GetMessageExtraInfo();

            INPUT input_up = input_down;
            input_up.mi.dwFlags = MOUSEEVENTF.LEFTUP;

            INPUT[] input = { input_down, input_up };
            return SendInput(2, input, Marshal.SizeOf(input_down));
        }

        /// <summary>
        /// Diese Funktion simuliert einen einfach Mausklick mit der linken Maustaste an der aktuellen Cursurposition.
        /// </summary>
        /// <returns>Liefert 2 zurück, wenn beide Aktionen (Maus down und Maus up) erfolgreich waren. Andernfalls 1 oder 0.</returns>
        public static uint RightClick()
        {
            INPUT input_down = new INPUT();
            input_down.type = InputType.INPUT_MOUSE;

            input_down.mi.dx = 0;
            input_down.mi.dy = 0;
            input_down.mi.mouseData = 0;
            input_down.mi.dwFlags = MOUSEEVENTF.RIGHTDOWN;
            input_down.mi.time = 0;
            input_down.mi.dwExtraInfo = GetMessageExtraInfo();

            INPUT input_up = input_down;
            input_up.mi.dwFlags = MOUSEEVENTF.RIGHTUP;

            INPUT[] input = { input_down, input_up };
            return SendInput(2, input, Marshal.SizeOf(input_down));
        }

        public static uint MouseDownLeft()
        {

            INPUT input_down = new INPUT();
            input_down.type = InputType.INPUT_MOUSE;

            input_down.mi.dx = 0;
            input_down.mi.dy = 0;
            input_down.mi.mouseData = 0;
            input_down.mi.dwFlags = MOUSEEVENTF.LEFTDOWN;
            input_down.mi.time = 0;
            input_down.mi.dwExtraInfo = GetMessageExtraInfo();

            INPUT[] input = { input_down };
            return SendInput(1, input, Marshal.SizeOf(input_down));
        }

        public static uint ScrollDown(int amount)
        {

            INPUT input_down = new INPUT();
            input_down.type = InputType.INPUT_MOUSE;

            input_down.mi.dx = 0;
            input_down.mi.dy = 0;
            input_down.mi.mouseData = amount;
            input_down.mi.dwFlags = MOUSEEVENTF.WHEEL;
            input_down.mi.time = 0;
            input_down.mi.dwExtraInfo = GetMessageExtraInfo();

            INPUT[] input = { input_down };
            return SendInput(1, input, Marshal.SizeOf(input_down));
        }

        public static uint MouseUpLeft()
        {

            INPUT input_down = new INPUT();
            input_down.type = InputType.INPUT_MOUSE;

            input_down.mi.dx = 0;
            input_down.mi.dy = 0;
            input_down.mi.mouseData = 0;
            input_down.mi.dwFlags = MOUSEEVENTF.LEFTUP;
            input_down.mi.time = 0;
            input_down.mi.dwExtraInfo = GetMessageExtraInfo();
            INPUT[] input = { input_down };
            return SendInput(1, input, Marshal.SizeOf(input_down));
        }


        public static Position CurrentMousePos()
        {
            int x = System.Windows.Forms.Cursor.Position.X;
            int y = System.Windows.Forms.Cursor.Position.Y;
            Position result = new Position();
            result.X = x;
            result.Y = y;
            return result;
        }

        public static void DeltaMove(int dx, int dy)
        {
            Move(CurrentMousePos().X + dx, CurrentMousePos().Y + dy);
        }

        public static void SmoothDeltaMove(int dx, int dy)
        {
            SmoothMove(CurrentMousePos().X + dx, CurrentMousePos().Y + dy);
        }

    }
}
