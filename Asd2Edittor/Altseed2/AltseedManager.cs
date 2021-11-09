using Altseed2;
using System.Threading.Tasks;

namespace Asd2Edittor.Altseed2
{
    public class AltseedManager
    {
        public static AltseedManager Current { get; } = new AltseedManager();
        private AltseedManager() { }
        public void Initialize(int width, int height)
        {
            if (!Engine.Initialize("", width, height)) return;
            Engine.ClearColor = new Color(200, 200, 200);
        }
        public async void Loop()
        {
            var rect = new RectangleNode
            {
                Color = new Color(255, 0, 0),
                Position = new Vector2F(100, 100),
                RectangleSize = new Vector2F(250, 40)
            };
            Engine.AddNode(rect);
            await Task.Run(() =>
            {
                while (Engine.DoEvents())
                {
                    rect.Angle++;
                    Engine.Update();
                }
                Engine.Terminate();
            });
        }
    }
}