using Altseed2;
using System.ComponentModel;

namespace Asd2UI.Altseed2
{
    /// <summary>
    /// 三角形のUIノード
    /// </summary>
    public class Triangle : UINode
    {
        private readonly TriangleNode triangleNode;
        /// <summary>
        /// アルファブレンドを取得または設定する
        /// </summary>
        public AlphaBlend AlphaBlend
        {
            get => triangleNode.AlphaBlend;
            set
            {
                if (AlphaBlend == value) return;
                triangleNode.AlphaBlend = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(AlphaBlend)));
            }
        }
        /// <inheritdoc/>
        public override ulong CameraGroup
        {
            get => triangleNode.CameraGroup;
            set
            {
                if (CameraGroup == value) return;
                triangleNode.CameraGroup = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(CameraGroup)));
            }
        }
        /// <summary>
        /// 色を取得または設定する
        /// </summary>
        public Color Color
        {
            get => triangleNode.Color;
            set
            {
                if (Color == value) return;
                triangleNode.Color = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Color)));
            }
        }
        /// <inheritdoc/>
        public override Vector2F ContentSize => triangleNode.ContentSize;
        /// <inheritdoc/>
        public override bool IsDrawn
        {
            get => triangleNode.IsDrawn;
            set
            {
                if (IsDrawn == value) return;
                triangleNode.IsDrawn = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsDrawn)));
            }
        }
        /// <summary>
        /// 頂点1の座標を取得または設定する
        /// </summary>
        public Vector2F Point1
        {
            get => triangleNode.Point1;
            set
            {
                if (Point1 == value) return;
                triangleNode.Point1 = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Point1)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(ContentSize)));
            }
        }
        /// <summary>
        /// 頂点2の座標を取得または設定する
        /// </summary>
        public Vector2F Point2
        {
            get => triangleNode.Point2;
            set
            {
                if (Point2 == value) return;
                triangleNode.Point2 = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Point2)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(ContentSize)));
            }
        }
        /// <summary>
        /// 頂点3の座標を取得または設定する
        /// </summary>
        public Vector2F Point3
        {
            get => triangleNode.Point3;
            set
            {
                if (Point3 == value) return;
                triangleNode.Point3 = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Point3)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(ContentSize)));
            }
        }
        /// <summary>
        /// 重ね順を取得または設定する
        /// </summary>
        public int ZOrder
        {
            get => triangleNode.ZOrder;
            set
            {
                if (ZOrder == value) return;
                triangleNode.ZOrder = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(ZOrder)));
            }
        }
        /// <summary>
        /// <see cref="Triangle"/>の新しいインスタンスを初期化する
        /// </summary>
        public Triangle()
        {
            triangleNode = new TriangleNode();
            InnerTransformNode.AddChildNode(triangleNode);
        }
    }
}
