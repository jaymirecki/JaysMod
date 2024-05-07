
using JMF.Native;
using System;

namespace JMF
{
    namespace UI
    {
        public class TextComponent
        {
            private const string DEFAULTCOLOR = "~s~";
            private string data;
            public TextComponent(string text, TextColor color)
            {
                data = TextComponent.ColorToString(color) + text + DEFAULTCOLOR;
            }
            public TextComponent(string text)
            {
                data = text;
            }
            public TextComponent(Control control, TextColor color)
            {
                data = TextComponent.ColorToString(color) + TextComponent.ControlToString(control) + DEFAULTCOLOR;
            }
            public TextComponent(Control control)
            {
                data = TextComponent.ControlToString(control);
            }
            public override string ToString()
            {
                return data;
            }
            private static string ColorToString(TextColor color)
            {
                string colorName = color.ToString().ToLower().Substring(0,1);
                switch (color)
                {
                    case TextColor.Gray:
                        colorName = "c";
                        break;
                    case TextColor.Pink:
                        colorName = "q";
                        break;
                    case TextColor.Silver:
                        colorName = "m";
                        break;
                    case TextColor.Black:
                        colorName = "l";
                        break;
                    default:
                        break;
                }


                return "~" + colorName + "~";
            }
            private static string ControlToString(Control control)
            {
                int controlInt = (int)control;
                TextInput input = (TextInput)controlInt;
                return "~" + input.ToString() + "~";
            }
            public static TextComponent operator+(TextComponent a, TextComponent b)
            {
                return new TextComponent(a.data + b.data);
            }
            public static implicit operator TextComponent(string text)
            {
                return new TextComponent(text);
            }
            public static implicit operator TextComponent(Control control)
            {
                return new TextComponent(control);
            }
        }
    }
}
