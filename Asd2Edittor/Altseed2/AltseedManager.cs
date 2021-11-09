using Altseed2;
using Asd2UI.Altseed2;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Asd2Edittor.Altseed2
{
    public class AltseedManager
    {
        public static AltseedManager Current { get; } = new AltseedManager();
        private readonly ConcurrentQueue<Action> actions = new ConcurrentQueue<Action>();
        private UINode uINode;
        private AltseedManager() { }
        public void Initialize(int width, int height)
        {
            if (!Engine.Initialize("", width, height)) return;
            Engine.ClearColor = new Color(200, 200, 200);
        }
        public async void Loop()
        {
            await Task.Run(() =>
            {
                while (Engine.DoEvents())
                {
                    while (actions.TryDequeue(out var action)) action?.Invoke();
                    Engine.Update();
                }
                Engine.Terminate();
            });
        }
        public void Post(Action action) => actions.Enqueue(action);
        public void SetNode(UINode value)
        {
            actions.Enqueue(() =>
            {
                if (uINode != null) Engine.RemoveNode(uINode);
                if (value != null) Engine.AddNode(value);
                uINode = value;
            });
        }
    }
}