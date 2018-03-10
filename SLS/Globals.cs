#define PRIVATE

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Terraria;
using Terraria.ID;
using Terraria.Social;

using System.Reflection;

using Microsoft.Xna.Framework.Input;

namespace SLS
{
    class Globals
    {
        public static bool IsPrivate
        {
            get
            {
#if PRIVATE
                return true;
#else
                return false;
#endif
            }
        }

        static string _Name = null;
        static string _Username = null;

        public static string Username
        {
            get
            {
                if (_Username == null)
                    _Username = IsCracked ? "unknown cracked user" : SocialAPI.Friends.GetUsername();
                return _Username;
            }
        }
        
        public static string Name
        {
            get
            {
                if (_Name == null)
                    _Name = "SLSHook v" + System.Reflection.Assembly.GetAssembly(typeof(Hooks)).GetName().Version;
                return _Name;
            }
        }

        public static bool IsCracked;

        public static bool HotkeysEnabled
        {
            get
            {
                return Main.hasFocus &&
                    !Main.blockInput &&
                    !Main.drawingPlayerChat &&
                    Main.chatText == string.Empty &&
                    !Main.editChest && !Main.editSign;
            }
        }

        private class Hotkey
        {
            private bool wasDown = false;

            public Keys Key;
            public Action Func;
            public bool Ctrl, Shift, Alt;

            public bool IsDown()
            {
                return Main.keyState.IsKeyDown(Key);
            }

            public bool WentDown()
            {
                var down = IsDown();

                if (down && !wasDown)
                {
                    wasDown = true;
                    return true;
                }
                else if (!down && wasDown)
                    wasDown = false;

                return false;
            }
        }

        static List<Hotkey> HotKeys = new List<Hotkey>();
        static Hotkey Alt = new Hotkey { Key = Keys.LeftAlt };
        static Hotkey Ctrl = new Hotkey { Key = Keys.LeftControl };
        static Hotkey Shift = new Hotkey { Key = Keys.LeftShift };

        public static void RegisterHotKey(Keys key, Action func, bool ctrl = false, bool shift = false, bool alt = false)
        {
            HotKeys.Add(new Hotkey
                {
                    Key = key,
                    Func = func,
                    Ctrl = ctrl,
                    Shift = shift,
                    Alt = alt
                });
        }

        public static void ExecuteHotKeys()
        {
            if (!HotkeysEnabled)
                return;

            foreach (var x in HotKeys)
            {
                if (x.WentDown() &&
                    x.Alt == Alt.IsDown() &&
                    x.Ctrl == Ctrl.IsDown() &&
                    x.Shift == Shift.IsDown())
                    x.Func();
            }
        }

        public static Player GetLocalPlayer()
        {
            return Main.player[Main.myPlayer];
        }

        public static bool IsLocalPlayer(Player other)
        {
            return other.whoAmI == Main.myPlayer;
        }

        public static void Print(object msg, params object[] format)
        {
            Main.NewText("[SLSHook]" + string.Format(msg.ToString(), format), 0, 255, 255);
        }
    }

    public class FieldHelper
    {
        protected Type _Type;
        protected object _Object;

        public class Field<T>
        {
            protected object _Object;
            protected FieldInfo _Field;

            public bool IsNull
            {
                get
                {
                    return _Field == null;
                }
            }

            public string Name
            {
                get
                {
                    return _Field.Name;
                }
            }

            public T Value
            {
                get
                {
                    return (T)Convert.ChangeType(_Field.GetValue(_Object), typeof(T));
                }
                set
                {
                    _Field.SetValue(_Object, value);
                }
            }

            public Field(object obj, FieldInfo field)
            {
                _Object = obj;
                _Field = field;
            }

            public string GetText(string key)
            {
                return Debug.FindFieldLangname(key + "." + Name);
            }
        }

        public int Count
        {
            get
            {
                return _Type.GetFields().Length;
            }
        }

        public FieldHelper(Type type) : this(type, null)
        {

        }

        public FieldHelper(Type type, object obj)
        {
            _Type = type;
            _Object = obj;
        }

        public Field<T> GetField<T>(string name, bool matchCase = false, BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Static)
        {
            var fld = _Type.GetFields(bindingFlags).FirstOrDefault(x => x.Name.Equals(name, (matchCase ? StringComparison.CurrentCulture : StringComparison.CurrentCultureIgnoreCase)));
            return new Field<T>(_Object, fld);
        }

        public Field<object> GetField(string name, bool matchCase = false)
        {
            return GetField<object>(name, matchCase);
        }

        public Field<T> GetFieldByIndex<T>(int index)
        {
            return GetField<T>(_Type.GetFields().ElementAt(index).Name, true);
        }

        public Field<T> GetFieldByValue<T>(T value)
        {
            var ret = null as Field<T>;

            foreach(var x in _Type.GetFields().Where(x => x.FieldType == typeof(T)))
            {
                if (x.GetValue(_Object).ConvertTo<T>().Equals(value))
                {
                    ret = GetField<T>(x.Name, true);
                    break;
                }
            }

            return ret;
        }

        public bool Contains(string name, bool matchCase = false)
        {
            return !GetField(name, matchCase).IsNull;
        }

        public T GetValue<T>(string fieldName, bool matchCase = false)
        {
            return GetField<T>(fieldName, matchCase).Value;
        }

        public void SetValue<T>(string fieldName, T value, bool matchCase = false)
        {
            GetField<T>(fieldName, matchCase).Value = value;
        }
    }
}
