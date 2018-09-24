using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Soleil
{
    //Key types
    enum Key : int
    {
        Left, Up, Right, Down, A, B, C, D, Start, TotalKeys,
    }
    enum Direction : int
    {
        N, L, LU, U, RU, R, RD, D, LD,
        CL, CLU, CU, CRU, CR, CRD, CD, CLD, //溜め技用 多分使わない
    }
    enum Controller:int //操縦者
    {
        KeyBoard=0, Computer, Training,
    }

    static class KeyInput
    {
        const int PlayerCount = 1;
        static int[,] input;
        static Controller[] controller;
        static KeyboardState keyState;
        static Keys[,] keyPosition;

        //Current stick input direction, Previous stick input direction
        static Direction[] currentDirection, preDirection;
        //
        public const int TotalDirections = 8;

        //Converter of Directions
        static readonly int[] ConvertToTenKey =
            {
                    5,4,7,8,9,6,3,2,1,
            };
        public static readonly Key[] ArrowKey =
            {
                    Key.Left, Key.Up, Key.Right, Key.Down,
            };

        public static readonly Key[][] DirectionToKey =
        {
            new Key[] { }, //0
            new Key[] { Key.Left}, //1
            new Key[] { Key.Left, Key.Up }, //2
            new Key[] { Key.Up }, //3
            new Key[] { Key.Up, Key.Right }, //4
            new Key[] { Key.Right }, //5
            new Key[] { Key.Right, Key.Down }, //6
            new Key[] { Key.Down }, //7
            new Key[] { Key.Down, Key.Left }, //8
        };

        //Direction when ArrowKey isnot down.
        const Direction defaultDirection = Direction.L;


        public static void Init()
        {
            //input is the time bottun is pushing [keytype, playerCount]
            input = new int[(int)Key.TotalKeys, PlayerCount];

            controller = new Controller[PlayerCount];

            //keyconfig will be perserved in config file.
            //keyPosition will read from this.
            keyPosition = new Keys[,] // [playerCount, keyType]
            {
                {
                    Keys.Left,
                    Keys.Up,
                    Keys.Right,
                    Keys.Down,
                    Keys.Z,
                    Keys.X,
                    Keys.C,
                    Keys.V,
                    Keys.Enter,
                },
                {
                    Keys.OemPeriod,
                    Keys.OemPlus,
                    Keys.OemBackslash,
                    Keys.OemQuestion,
                    Keys.B,
                    Keys.N,
                    Keys.M,
                    Keys.OemComma,
                    Keys.Escape,
                }
            };

            currentDirection = new Direction[PlayerCount];
            preDirection = new Direction[PlayerCount];

            InitGamePad();
        }

        static bool[] UseGamepad;
        static List<PlayerIndex> gamepad;
        static List<GamePadState> gamepadState;
        static Buttons[,] gamepadButtons;
        static List<Buttons> allButtons = new List<Buttons>()
        {
            Buttons.A,
            Buttons.B,
            Buttons.X,
            Buttons.Y,
            Buttons.Start,
            Buttons.DPadLeft,
            Buttons.DPadUp,
            Buttons.DPadRight,
            Buttons.DPadDown,
            Buttons.LeftShoulder,
            Buttons.RightShoulder,
            Buttons.LeftTrigger,
            Buttons.RightTrigger,
            Buttons.Back,
            Buttons.BigButton,
            Buttons.LeftStick,
            Buttons.RightStick,
            Buttons.LeftThumbstickLeft,
            Buttons.LeftThumbstickUp,
            Buttons.LeftThumbstickRight,
            Buttons.LeftThumbstickDown,
            Buttons.RightThumbstickLeft,
            Buttons.RightThumbstickUp,
            Buttons.RightThumbstickRight,
            Buttons.RightThumbstickDown,
        };

        //static GamePadThumbSticks[,] gamepadStick;
        const float StickIncliningDistance = 0.6f;

        static IEnumerable<PlayerIndex> GetConnectedGamePad()
            => Enum.GetValues(typeof(PlayerIndex)).Cast<PlayerIndex>()
                .Where(p => GamePad.GetCapabilities(p).IsConnected);
        public static void InitGamePad()
        {
            var pads = GetConnectedGamePad().ToList();

            for (int i = 0; i < pads.Count - 2; i++)
                pads.RemoveAt(pads.Count - 1);

            gamepad = new List<PlayerIndex>();
            if(pads.Count==0)
            {
                gamepad.Add(0);
                gamepad.Add(0);
            }
            else if (pads.Count==1)
            {
                gamepad.Add(0);
                gamepad.Add(pads[0]);
            }
            else
            {
                gamepad.Add(pads[0]);
                gamepad.Add(pads[1]);
            }


            UseGamepad = new bool[PlayerCount];
            for (int i = 0; i < pads.Count; i++)
            {
                //Gamepadは2P優先
                UseGamepad[PlayerCount-1-i] = true;
            }


            gamepadState = new List<GamePadState>(2);

            gamepadButtons = new Buttons[,]
            {
                {
                    Buttons.DPadLeft,
                    Buttons.DPadUp,
                    Buttons.DPadRight,
                    Buttons.DPadDown,
                    Buttons.A,
                    Buttons.B,
                    Buttons.X,
                    Buttons.Y,
                    Buttons.Start,
                },
                {
                    Buttons.DPadLeft,
                    Buttons.DPadUp,
                    Buttons.DPadRight,
                    Buttons.DPadDown,
                    Buttons.A,
                    Buttons.B,
                    Buttons.X,
                    Buttons.Y,
                    Buttons.Start,
                }
            };

        }

        public static void Move()
        {
            gamepadState = gamepad.Select(p=>GamePad.GetState(p)).ToList();
            keyState = Keyboard.GetState();

            for (int i = 0; i < PlayerCount; i++)
                switch (controller[i])
                {
                    case Controller.KeyBoard:
                        if (UseGamepad[i])
                        {
                            //When Controller is Gamepad
                            UpdateInputFromGamePad(i);
                        }
                        else
                        {
                            //When Controller is Keyboard
                            UpdateInputFromKeyBoard(i);
                        }
                        break;
                    case Controller.Training:
                        break;
                }

            /*if (Game1.frame > 600)
            {
                input[(int)Key.A, 1] = Game1.frame % 120;
                input[(int)Key.B, 1] = Game1.frame % 120 + 1;
            }*/

            for (int i = 0; i < PlayerCount; i++)
            {
                preDirection[i] = currentDirection[i];
                currentDirection[i] = GetStickInput(i + 1);
            }
        }


        static void UpdateInputFromGamePad(int player)
        {
            var inclined = GetInputFromGamePadStick(player);
            for (int i = 0; i < (int)Key.TotalKeys; i++)
                if (gamepadState[player].IsButtonDown(gamepadButtons[player, i]) 
                    || (i < ArrowKey.Count() && inclined[i]))   //スティックの傾きでも十字入力ができる
                    input[i, player]++;
                else
                    input[i, player] = 0;
        }
        static bool[] GetInputFromGamePadStick(int player)
        {
            var vec = gamepadState[player].ThumbSticks.Left;
            bool[] inclined = new bool[4];
            if (vec.X < -StickIncliningDistance)
                inclined[(int)Key.Left] = true;
            if (vec.Y > StickIncliningDistance)
                inclined[(int)Key.Up] = true;
            if (vec.X > StickIncliningDistance)
                inclined[(int)Key.Right] = true;
            if (vec.Y < -StickIncliningDistance)
                inclined[(int)Key.Down] = true;
            return inclined;

        }

        static void UpdateInputFromKeyBoard(int player)
        {
            for (int i = 0; i < (int)Key.TotalKeys; i++)
                if (keyState.IsKeyDown(keyPosition[player, i]))
                    input[i, player]++;
                else
                    input[i, player] = 0;
        }

        public static void UpdateInput(bool[] down, int player)
        {
            for (int i = 0; i < (int)Key.TotalKeys; i++)
                if (down[i])
                    input[i, player]++;
                else
                    input[i, player] = 0;

            preDirection[player] = currentDirection[player];
            currentDirection[player] = GetStickInput(player + 1);
        }
        
        public static bool IsKeysDown(Keys keys) => keyState.IsKeyDown(keys);

        public static int GetKeyState(Key key, int player)
        {
            return input[(int)key, player - 1];
        }
        
        //If One of Players Push
        public static bool GetKeyPush(Key key)
        {
            bool flag = false;
            for (int i = 0; i < PlayerCount; i++)
                flag |= input[(int)key, i] == 1;
            return flag;
        }

        //Is Key Pushed
        public static bool GetKeyPush(Key key, int player)
        {
            return input[(int)key, player - 1] == 1;
        }

        public static bool GetKeyDown(Key key)
        {
            bool flag = false;
            for (int i = 0; i < PlayerCount; i++)
                flag |= input[(int)key, i] > 0;
            return flag;
        }

        //Is Key Down
        public static bool GetKeyDown(Key key, int player)
        {
            return input[(int)key, player - 1] > 0;
        }



        /// <summary>
        /// 何かしらキーが押されていれば、それをひとつだけ返す
        /// </summary>
        public static Nullable<Keys> GetAnyKeyPush()
        {
            var keys = keyState.GetPressedKeys();
            if (keys.Any()) return keys.First();
            else return null;
        }

        /// <summary>
        /// 押されているボタンをすべて取得する
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public static IEnumerable<Buttons> GetAllButtonDown(GamePadState state)
        {
            var buttons = allButtons.Where(e => state.IsButtonDown(e));
            return buttons;
        }
        /// <summary>
        /// 押されているボタンのうち一つを返す
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static Nullable<Buttons> GetAnyButtonPush(int player)
        {
            var buttons = GetAllButtonDown(gamepadState[player]);
            if (buttons.Any()) return buttons.First();
            else return null;
        }
        /// <summary>
        /// すべてのゲームパッドのボタン入力状態を取得
        /// 最大接続数4の配列を返す
        /// </summary>
        /// <returns></returns>
        public static List<IEnumerable<Buttons>> GetAllGamePadState()
        {
            var playerIndex = GetConnectedGamePad();
            var list = new List<IEnumerable<Buttons>>();
            for (int i = 0; i < Enum.GetValues(typeof(PlayerIndex)).Length; i++)
            {
                if (!playerIndex.Contains((PlayerIndex)i))
                {
                    list.Add(null);
                }
                else
                {
                    var state = GamePad.GetState((PlayerIndex)i);
                    list.Add(GetAllButtonDown(state));
                }
            }
            return list;
        }



        public static void SetKey(int player, Key key, Keys keys)
        {
            keyPosition[player, (int)key] = keys;
            input[(int)key, player]++;
        }
        public static Keys GetKey(int player, Key key) => keyPosition[player, (int)key];

        public static void SetButton(int player, Key key, Buttons keys)
        {
            gamepadButtons[player, (int)key] = keys;
            input[(int)key, player]++;
        }
        public static Buttons GetButton(int player, Key key) => gamepadButtons[player, (int)key];
        /// <summary>
        /// 1Pと2PのゲームパッドのPlayerIndexを設定する
        /// 長さ2のリストを渡す
        /// </summary>
        /// <param name="p"></param>
        public static void SetGamepad(List<PlayerIndex?> p)
        {
            for (int i = 0; i < PlayerCount; i++)
            {
                if (p[i].HasValue)
                {
                    gamepad[i] = p[i].Value;
                    UseGamepad[i] = true;
                }
                else
                {
                    UseGamepad[i] = false;
                }
            }
        }

        public static bool IsGamePad(int player) => UseGamepad[player];
        public static PlayerIndex GetGamePadIndex(int player) => gamepad[player];
        public static string GetKeyName(int player, Key key)
        {
            if (IsGamePad(player))
            {
                return gamepadButtons[player, (int)key].ToString();
            }
            else
            {
                return keyPosition[player, (int)key].ToString();
            }
        }

        


        //Degree Measure Of Stick Direction
        // * If stick is neutral or left, return 0
        public static double GetDegreeDirection(int player)
        {
            return MathEx.DegreeNormalize(
                Math.Atan2((GetKeyDown(Key.Up, player) ? 1 : 0) - (GetKeyDown(Key.Down, player) ? 1 : 0),
                (GetKeyDown(Key.Left, player) ? 1 : 0) - (GetKeyDown(Key.Right, player) ? 1 : 0)) / (Math.PI / (MathEx.FullCircle / 2 /*180 degrees*/))
                );
        }

        //360 Degrees To 8 Directions
        static Direction ConvertDegreeToDirection(double degree)
        {
            return (Direction)((int)((degree + MathEx.FullCircle / TotalDirections / 2 /*22.5 degrees*/  + MathEx.Eps)
                / (MathEx.FullCircle / TotalDirections)/*45 degrees*/)
                % TotalDirections + 1);
        }

        //If direction is 1 and Key.Left is not down, direction should be 0
        static Direction GetStickInput(int player)
        {
            var direction = ConvertDegreeToDirection(GetDegreeDirection(player));

            var noPush = from e in DirectionToKey[(int)defaultDirection]
                         select GetKeyDown(e, player);

            if (direction == defaultDirection && !noPush.Aggregate((i, j) => i || j))
                return Direction.N;
            else
                return direction;
        }

        //<Direction, time>
        public static Tuple<Direction, int> GetStickState(int player)
        {
            var direction = GetStickInput(player);
            var isDown = new List<int>();
            foreach (var e in DirectionToKey[(int)direction])
                isDown.Add(GetKeyState(e, player));
            var minTime = direction == 0 ? 0 : isDown.Aggregate((i, j) => j == 0 ? i : (i == 0 ? j : Math.Min(i, j)));

            return Tuple.Create(minTime > 0 ? direction : Direction.N, minTime);

            ///
            /// 
            ///234
            ///105
            ///876
            ///
        }

        public static Direction GetStickInclineDirection(int player)
        {
            return currentDirection[player - 1];

            ///
            /// 
            ///234
            ///105
            ///876
            ///
        }

        public static Direction GetStickFlickDirection(int player)
        {
            return currentDirection[player - 1] != preDirection[player - 1] ? currentDirection[player - 1] : Direction.N;

            /*var isPushed = new List<bool>();
            foreach (var e in DirectionToKey[(int)direction])
                isPushed.Add(GetKeyPush(e, player));

            //レバーの方向に倒した瞬間であるか
            //斜めに入力されたとき、どちらかのキーが押された瞬間だったらtrueを返してほしいよね

            return isPushed.Aggregate(false, (i, j) => i || j) ? direction : Direction.N;*/

            ///
            /// 
            ///234
            ///105
            ///876
            ///
        }

        public static int ConvertToTenKeyDirection(Direction direction)
        {
            return ConvertToTenKey[(int)direction];

            ///TenKey
            /// 
            ///789
            ///456
            ///123
            ///
        }
    }
}
