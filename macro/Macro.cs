using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;
using WindowsInput;
using WindowsInput.Native;

namespace macro
{
    public class Macro
    {
        const int LoadingTime = 30000; // milliseconds
        const int PreBattleCountdownTime = 32000; // milliseconds
        const int NoTanksDelay = 30000; // milliseconds

        const string WindowTitle = "W.o.T. Client";

        private Point MenuLogo = new Point(134, 67);
        private Color MenuLogoColor = Color.FromArgb(187, 83, 23);

        private Point BattleButton = new Point(447, 40);
        private Color BattleButtonRed = Color.FromArgb(148, 21, 19);
        private Color BattleButtonGrey = Color.FromArgb(48, 46, 35);

        private Point TankInBattle = new Point(417, 318);
        private Color TankInBattleColor = Color.FromArgb(255, 0, 0);

        private Point TankKnockedOut = new Point(559, 396);
        private Color TankKnockedOutColor = Color.FromArgb(255, 0, 0);

        private Point RepairButton = new Point(505, 204);
        private Point ResupplyButton = new Point(512, 514);

        private Point MiniMap = new Point(1018, 762);
        private Color MiniMapColor = Color.FromArgb(149, 149, 132);

        private Point Scoreboard = new Point(941, 163);
        private Color ScoreboardColor = Color.FromArgb(82, 76, 61);

        private Point HealthBar = new Point(169, 560);

        private Point ExitBattleButton = new Point(503, 370);
        private Point ExitYesButton = new Point(558, 437);

        private Point[] SlotCoordinates = {
            new Point(180, 660), // slot 1
            new Point(345, 660), // slot 2
            new Point(500, 660), // slot 3
            new Point(700, 660), // slot 4
            new Point(855, 660), // slot 5
        };

        private WindowsInput.Native.VirtualKeyCode[] MovementKeysForwardReverse = {
            VirtualKeyCode.VK_W,
            VirtualKeyCode.VK_S,
        };

        private WindowsInput.Native.VirtualKeyCode[] MovementKeysLeftRight = {
            VirtualKeyCode.VK_A,
            VirtualKeyCode.VK_D,
        };

        private WindowsInput.Native.VirtualKeyCode[] RadioKeys = {
            VirtualKeyCode.F2,
            VirtualKeyCode.F3,
            VirtualKeyCode.F4,
            VirtualKeyCode.F5,
            VirtualKeyCode.F6,
            VirtualKeyCode.F7,
        };

        private IntPtr hWnd = IntPtr.Zero;
        private Point GameCorner = new Point();
        private Win32.RECT GameRect;
        private MouseSimulator Mouse = new MouseSimulator();
        private KeyboardSimulator Keyboard = new KeyboardSimulator();
        private Random Random = new Random();
        private int TotalSlots;

        public bool[] EnabledSlots { get; set; }

        /// <summary>
        /// Set which slots you want to enable the macro to use
        /// </summary>
        /// <param name="EnabledSlots"></param>
        public Macro(bool[] EnabledSlots, int TotalSlots)
        {
            this.EnabledSlots = EnabledSlots;
            this.TotalSlots = TotalSlots;
        }

        public bool Start()
        {
            if (!FindGame(WindowTitle))
                return false;
            else
            {
                if (GameRect.Width != 1024)
                {
                    Console.WriteLine("Found game. Incorrect window dimensions, set the game to 1024x768");
                    return false;
                }

                Console.WriteLine("Found game, window size: " + GameRect.Width + "x" + GameRect.Height);

                // get focus since we need it
                Win32.SetForegroundWindow(hWnd);


                //MoveMouseTo(GameRect.Width - 50, 25); // "menu" button at 1680x1050 resolution
                //mouse.LeftButtonClick();

                Thread.Sleep(1000);

                //for (int i = 0; i < 500; i++)
                    //Console.WriteLine(GetPixelColor(HealthBar));
                //while(true)
                    //RandomMovement();
                //return true;

                if (!AtMenu())
                {
                    Console.WriteLine("Not at the tank selection screen!");
                    return false;
                }

                while (true)
                {
                    if (FindTank())
                    {
                        Thread.Sleep(1000);
                        DoBattle();
                    }
                    else Thread.Sleep(NoTanksDelay); // no tanks available, sleep a bit
                }
                return true;
            }
        }

        public void DoBattle()
        {
            // click "battle" button
            Console.WriteLine("Entering battle");
            LeftClick(BattleButton);

            Console.WriteLine("Loading...");
            while (!InBattle())
                Thread.Sleep(100);

            Console.WriteLine("Waiting for countdown...");
            Thread.Sleep(PreBattleCountdownTime);

            Console.WriteLine("Battle started, moving around like a wanker");
            Mouse.MoveMouseBy(0, -170); // line turret up horizontally

            while (!AtMenu() && !AtScoreboard())
            {
                RandomMovement();
                // check if we're dead
                for (int i = 0; i < 100; i++)
                {
                    if (Dead())
                    {
                        Console.WriteLine("Tank has been knocked out, exiting battle");
                        ExitBattle();
                        Thread.Sleep(1000);
                        return;
                    }
                    Thread.Sleep(1);
                }
                if (!AtScoreboard())
                    Thread.Sleep(Random.Next(500, 5000));
            }

            Thread.Sleep(1000);
            if (AtScoreboard())
                KeyPress(VirtualKeyCode.ESCAPE);

            Console.WriteLine("Battle finished");
            Thread.Sleep(1000);
        }

        public bool InBattle()
        {
            if (GetPixelColor(MiniMap) == MiniMapColor)
                return true;
            else return false;
        }

        public void ExitBattle()
        {
            KeyPress(VirtualKeyCode.ESCAPE);
            Thread.Sleep(500);
            LeftClick(ExitBattleButton);
            Thread.Sleep(500);
            LeftClick(ExitYesButton);
        }

        public bool Dead()
        {
            if (GetPixelColor(HealthBar).R >= 245)
                return true;
            else return false;
        }

        public bool AtScoreboard()
        {
            if (GetPixelColor(Scoreboard) == ScoreboardColor)
            {
                Console.WriteLine("At the scoreboard screen");
                return true;
            }
            else return false;
        }

        public bool AtMenu()
        {
            if (GetPixelColor(MenuLogo) == MenuLogoColor)
                return true;
            else return false;
        }

        /// <summary>
        /// Random movement to avoid suspicion during battles (this is depressingly stupid)
        /// </summary>
        public void RandomMovement()
        {
            bool move = RandomBool();
            bool turn = RandomBool();

            var direction1 = MovementKeysForwardReverse[Random.Next(0, 2)];

            // do random radio chat
            if (Random.NextDouble() >= 0.98)
            {
                KeyPress(RadioKeys[Random.Next(0, RadioKeys.Length)]);
            }

            // turn the turret
            Mouse.MoveMouseBy(Random.Next(-700, 700), 0);

            if (move)
            {
                // move forward or reverse
                KeyDown(direction1);
            }

            if (turn)
            {
                // turn left or right
                var direction2 = MovementKeysLeftRight[Random.Next(0, 2)];
                KeyDown(direction2);
                Thread.Sleep(Random.Next(500, 7000));
                KeyUp(direction2);
            }

            if (move)
            {
                // continue going straight for a bit
                Thread.Sleep(Random.Next(500, 15000));
                KeyUp(direction1); // release W
            }
        }

        /// <summary>
        /// Finds an available tank
        /// </summary>
        /// <returns></returns>
        public bool FindTank()
        {
            // any slots enabled?
            if (EnabledSlots.Count(q => q == true) == 0) return false;

            // check each one
            for (int i = 0; i < EnabledSlots.Length; i++)
                if (EnabledSlots[i] == true)
                {
                    // offset slot coordinates if more than 4 slots exist (due to the stupid crap that appears on the left)
                    var coords = SlotCoordinates[i + (TotalSlots >= 5 ? 1 : 0)];
                    // click multiple times cause sometimes it fails :(
                    LeftClick(coords);
                    LeftClick(coords);
                    LeftClick(coords);

                    Thread.Sleep(2000); // loading

                    // check if this tank is in battle
                    if (GetPixelColor(TankInBattle) == TankInBattleColor)
                    {
                        Console.WriteLine("Tank " + i + " is in battle");
                    }

                    // check if this tank needs repair, if so do it
                    else if (GetPixelColor(TankKnockedOut) == TankKnockedOutColor)
                    {
                        Console.WriteLine("Tank " + i + " is knocked out, repairing...");
                        LeftClick(RepairButton);
                        Thread.Sleep(1000);
                    }

                    if (GetPixelColor(BattleButton) == BattleButtonRed)
                    {
                        Console.WriteLine("Tank " + i + " is ready to fight");
                        return true;
                    }
                }

            return false;
        }

        /// <summary>
        /// Gets the color of a pixel in the game window
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Color GetPixelColor(int x, int y)
        {
            return Win32.GetPixelColor(GameCorner.X + x, GameCorner.Y + y);
        }

        public Color GetPixelColor(Point coords)
        {
            return GetPixelColor(coords.X, coords.Y);
        }

        public void LeftClick(int x, int y)
        {
            MoveMouseTo(x, y);
            Mouse.LeftButtonClick();
        }

        public void LeftClick(Point coords)
        {
            LeftClick(coords.X, coords.Y);
        }

        public void KeyDown(VirtualKeyCode key)
        {
            Win32.keybd_event((byte)key, (byte)Win32.MapVirtualKey((byte)key, Win32.MAPVK_VK_TO_VSC), 0, 0);
        }

        public void KeyUp(VirtualKeyCode key)
        {
            Win32.keybd_event((byte)key, (byte)Win32.MapVirtualKey((byte)key, Win32.MAPVK_VK_TO_VSC), (uint)KeyboardFlag.KeyUp, 0);
        }

        public void KeyPress(VirtualKeyCode key)
        {
            Win32.keybd_event((byte)key, (byte)Win32.MapVirtualKey((byte)key, Win32.MAPVK_VK_TO_VSC), 0, 0);
            Thread.Sleep(10);
            Win32.keybd_event((byte)key, (byte)Win32.MapVirtualKey((byte)key, Win32.MAPVK_VK_TO_VSC), (uint)KeyboardFlag.KeyUp, 0);
        }

        /// <summary>
        /// Moves the mouse to a point within the game window (in pixels)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void MoveMouseTo(int x, int y)
        {
            Point newCoords = GameToMouse(x, y);
            Mouse.MoveMouseTo(newCoords.X, newCoords.Y);
        }

        public void MoveMouseTo(Point coords)
        {
            MoveMouseTo(coords.X, coords.Y);
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
                Console.WriteLine("Couldn't find the game window");
                return false; // shit doesn't exist
            }

            //Console.WriteLine("found the game window");
            Win32.WINDOWPLACEMENT placement = new Win32.WINDOWPLACEMENT();
            placement.length = Marshal.SizeOf(placement);
            Win32.GetWindowPlacement(hWnd, out placement);
            if (placement.showCmd == Win32.SW_SHOWMINIMIZED)
            {
                Console.WriteLine("Game window is minimized, restoring");
                placement.showCmd = (int)Win32.SW_RESTORE;
                Win32.SetWindowPlacement(hWnd, ref placement);
            }

            GameRect = Win32.GetClientRect(hWnd);

            GameCorner.X = GameRect.Left;
            GameCorner.Y = GameRect.Top;
            bool foundCorner = Win32.ClientToScreen(hWnd, ref GameCorner);

            if (!foundCorner)
            {
                Console.WriteLine("Couldn't get coordinates of game window");
                return false;
            }
            else return true;
        }

        public bool RandomBool()
        {
            return (Random.NextDouble() > 0.5);
        }
    }
}
