using Altseed2;
using System.ComponentModel;

namespace Asd2UI.Altseed2
{
    /// <summary>
    /// 直線のUIノード
    /// </summary>
    public class Line : UINode
    {
        private readonly LineNode lineNode;
        /// <summary>
        /// アルファブレンドを取得または設定する
        /// </summary>
        public AlphaBlend AlphaBlend
        {
            get => lineNode.AlphaBlend;
            set
            {
                if (AlphaBlend == value) return;
                lineNode.AlphaBlend = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(AlphaBlend)));
            }
        }
        /// <inheritdoc/>
        public override ulong CameraGroup
        {
            get => lineNode.CameraGroup;
            set
            {
                if (CameraGroup == value) return;
                lineNode.CameraGroup = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(CameraGroup)));
            }
        }
        /// <summary>
        /// 色を取得または設定する
        /// </summary>
        public Color Color
        {
            get => lineNode.Color;
            set
            {
                if (Color == value) return;
                lineNode.Color = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Color)));
            }
        }
        /// <inheritdoc/>
        public override Vector2F ContentSize => lineNode.ContentSize;
        /// <inheritdoc/>
        public override bool IsDrawn
        {
            get => lineNode.IsDrawn;
            set
            {
                if (IsDrawn == value) return;
                lineNode.IsDrawn = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsDrawn)));
            }
        }
        /// <summary>
        /// 頂点1の座標を取得または設定する
        /// </summary>
        public Vector2F Point1
        {
            get => lineNode.Point1;
            set
            {
                if (Point1 == value) return;
                lineNode.Point1 = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Point1)));
            }
        }
        /// <summary>
        /// 頂点2の座標を取得または設定する
        /// </summary>
        public Vector2F Point2
        {
            get => lineNode.Point2;
            set
            {
                if (Point2 == value) return;
                lineNode.Point2 = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Point2)));
            }
        }
        /// <summary>
        /// 幅を取得または設定する
        /// </summary>
        public float Thickness
        {
            get => lineNode.Thickness;
            set
            {
                if (Thickness == value) return;
                lineNode.Thickness = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Thickness)));
            }
        }
        /// <summary>
        /// <see cref="Line"/>の新しいインスタンスを初期化する
        /// </summary>
        public Line()
        {
            lineNode = new LineNode();
            InnerTransformNode.AddChildNode(lineNode);
        }
        /// <inheritdoc/>
        protected override TransformNode CreateInnerNode() => new LineNode();
    }
}
