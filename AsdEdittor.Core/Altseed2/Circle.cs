using Altseed2;
using System.ComponentModel;

namespace Asd2UI.Altseed2
{
    /// <summary>
    /// 円のUIノード
    /// </summary>
    public class Circle : UINode
    {
        private readonly CircleNode circleNode;
        /// <summary>
        /// アルファブレンドを取得または設定する
        /// </summary>
        public AlphaBlend AlphaBlend
        {
            get => circleNode.AlphaBlend;
            set
            {
                if (AlphaBlend == value) return;
                circleNode.AlphaBlend = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(AlphaBlend)));
            }
        }
        /// <inheritdoc/>
        public override ulong CameraGroup
        {
            get => circleNode.CameraGroup;
            set
            {
                if (CameraGroup == value) return;
                circleNode.CameraGroup = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(CameraGroup)));
            }
        }
        /// <summary>
        /// 色を取得または設定する
        /// </summary>
        public Color Color
        {
            get => circleNode.Color;
            set
            {
                if (Color == value) return;
                circleNode.Color = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Color)));
            }
        }
        /// <inheritdoc/>
        public override Vector2F ContentSize => circleNode.ContentSize;
        /// <inheritdoc/>
        public override bool IsDrawn
        {
            get => circleNode.IsDrawn;
            set
            {
                if (IsDrawn == value) return;
                circleNode.IsDrawn = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsDrawn)));
            }
        }
        /// <summary>
        /// 半径を取得または設定する
        /// </summary>
        public float Radius
        {
            get => circleNode.Radius;
            set
            {
                if (Radius == value) return;
                circleNode.Radius = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Radius)));
            }
        }
        /// <summary>
        /// 頂点数を取得または設定する
        /// </summary>
        public int VertNum
        {
            get => circleNode.VertNum;
            set
            {
                if (VertNum == value) return;
                circleNode.VertNum = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(VertNum)));
            }
        }
        /// <summary>
        /// <see cref="Circle"/>の新しいインスタンスを初期化する
        /// </summary>
        public Circle()
        {
            circleNode = new CircleNode();
            InnerTransformNode.AddChildNode(circleNode);
        }
        /// <inheritdoc/>
        protected override TransformNode CreateInnerNode() => new CircleNode();
    }
}
