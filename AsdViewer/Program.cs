using Altseed2;
using System;

namespace AsdViewer
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!Engine.Initialize("", 726, 500)) return;
            Engine.ClearColor = new Color(200, 200, 200);
            var rect = new RectangleNode
            {
                Color = new Color(255, 0, 0),
                Position = new Vector2F(100, 100),
                RectangleSize = new Vector2F(250, 40)
            };
            Engine.AddNode(rect);
            while (Engine.DoEvents())
            {
                rect.Angle++;
                Engine.Update();
            }
            Engine.Terminate();
        }
    }
}
