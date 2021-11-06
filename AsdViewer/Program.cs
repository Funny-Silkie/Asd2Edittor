using Altseed2;
using System;

namespace AsdViewer
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!Engine.Initialize("", 960, 720)) return;
            Engine.ClearColor = new Color(200, 200, 200);
            while (Engine.DoEvents())
            {
                Engine.Update();
            }
            Engine.Terminate();
        }
    }
}
