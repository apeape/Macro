using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;
using WindowsInput;
using System.Windows.Forms;
using System.Threading;

namespace macro
{
    public class Macro
    {
        const string WindowTitle = "W.o.T. Client";
        private IntPtr hWnd = IntPtr.Zero;
        private Point GameCorner = new Point();
        private Win32.RECT GameRect;
        private MouseSimulator mouse = new MouseSimulator();

        public Macro()
        {
            
        }

        public bool Start()
        {
            if (!FindGame(WindowTitle))
                return false;
            else
            {
                Console.WriteLine("found game, window size: " + GameRect.Width + "x" + GameRect.Height);

                // get focus since we need it
                Win32.SetForegroundWindow(hWnd);
                MoveMouseTo(GameRect.Width - 50, 25); // "menu" button at 1680x1050 resolution
                mouse.LeftButtonClick();

                return true;
            }
        }

        /// <summary>
        /// Moves the mouse to a point within the game window (in pixels)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void MoveMouseTo(int x, int y)
        {
            Point newCoords = GameToMouse(x, y);
            mouse.MoveMouseTo(newCoords.X, newCoords.Y);
        }

        public Point GameToMouse(int x, int y)
        {
            Point newCoords = new Point();
            newCoords.X = x + GameCorner.X;
            newCoords.Y = y + GameCorner.Y;
            return ClientToMouse(newCoords);
        }

        public Point ClientToMouse(Point coords)
        {
            Point newCoords = new Point();
            newCoords.X = coords.X * 65535 / System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            newCoords.Y = coords.Y * 65535 / System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;

            return newCoords;
        }

        public bool FindGame(string WindowTitle)
        {
            hWnd = Win32.FindWindow(null, WindowTitle);

            if (hWnd == IntPtr.Zero)
            {
                Console.WriteLine("couldn't find the game window");
                return false; // shit doesn't exist
            }

            //Console.WriteLine("found the game window");
            Win32.WINDOWPLACEMENT placement = new Win32.WINDOWPLACEMENT();
            placement.length = Marshal.SizeOf(placement);
            Win32.GetWindowPlacement(hWnd, out placement);
            if (placement.showCmd == Win32.SW_SHOWMINIMIZED)
            {
                Console.WriteLine("game window is minimized, restoring");
                placement.showCmd = (int)Win32.SW_RESTORE;
                Win32.SetWindowPlacement(hWnd, ref placement);
            }

            GameRect = Win32.GetClientRect(hWnd);

            GameCorner.X = GameRect.Left;
            GameCorner.Y = GameRect.Top;
            bool foundCorner = Win32.ClientToScreen(hWnd, ref GameCorner);

            if (!foundCorner)
            {
                Console.WriteLine("couldn't get coordinates of game window");
                return false;
            }
            else return true;
        }
    }
}
