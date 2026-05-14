using System.Runtime.InteropServices;

namespace EasyImGui.Core
{
    [StructLayout(LayoutKind.Sequential)]
    public struct WindowBounds
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
        public override bool Equals(object obj)
        {
            if (obj is WindowBounds value)
            {
                return value.Left == Left
                    && value.Right == Right
                    && value.Top == Top
                    && value.Bottom == Bottom;
            }
            else
            {
                return false;
            }
        }
        public bool Equals(WindowBounds value)
        {
            return value.Left == Left
                && value.Right == Right
                && value.Top == Top
                && value.Bottom == Bottom;
        }
        public override int GetHashCode()
        {
            return OverrideHelper.HashCodes(
                Left.GetHashCode(),
                Top.GetHashCode(),
                Right.GetHashCode(),
                Bottom.GetHashCode());
        }

        public override string ToString()
        {
            return OverrideHelper.ToString(
                "Left", Left.ToString(),
                "Top", Top.ToString(),
                "Right", Right.ToString(),
                "Bottom", Bottom.ToString());
        }

        public static bool Equals(WindowBounds left, WindowBounds right)
        {
            return left.Equals(right);
        }
    }
}
