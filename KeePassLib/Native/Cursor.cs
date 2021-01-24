using System;
using System.Drawing;

namespace System.Windows.Forms
{
    public static class Cursor
    {
        public static Point Position
        {
            get
            {
                var p = new Random();
                return new Point(p.Next(), p.Next());
            }
        } 
    }
}