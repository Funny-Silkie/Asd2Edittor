using Altseed2;
using System.ComponentModel;

namespace Asd2UI.Altseed2
{
    /// <summary>
    /// 短形のUIノード
    /// </summary>
    public class Rectangle : UINode
    {
        private readonly RectangleNode rectangleNode;
        /// <summary>
        /// アルファブレンドを取得または設定する
        /// </summary>
        public AlphaBlend AlphaBlend
        {
            get => rectangleNode.AlphaBlend;
            set
            {
                if (AlphaBlend == value) return;
                rectangleNode.AlphaBlend = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(AlphaBlend)));
            }
        }
        /// <inheritdoc/>
        public override ulong CameraGroup
        {
            get => rectangleNode.CameraGroup;
            set
            {
                if (CameraGroup == value) return;
                rectangleNode.CameraGroup = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(CameraGroup)));
            }
        }
        /// <summary>
        /// 色を取得または設定する
        /// </summary>
        public Color Color
        {
            get => rectangleNode.Color;
            set
            {
                if (Color == value) return;
                rectangleNode.Color = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Color)));
            }
        }
        /// <inheritdoc/>
        public override Vector2F ContentSize => rectangleNode.ContentSize;
        /// <inheritdoc/>
        public override bool IsDrawn
        {
            get => rectangleNode.IsDrawn;
            set
            {
                if (IsDrawn == value) return;
                rectangleNode.IsDrawn = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsDrawn)));
            }
        }
        /// <summary>
        /// サイズを取得または設定する
        /// </summary>
        public Vector2F RectangleSize
        {
            get => rectangleNode.RectangleSize;
            set
            {
                if (RectangleSize == value) return;
                rectangleNode.RectangleSize = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(RectangleSize)));
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(ContentSize)));
            }
        }
        /// <summary>
        /// 重ね順を取得または設定する
        /// </summary>
        public int ZOrder
        {
            get => rectangleNode.ZOrder;
            set
            {
                if (ZOrder == value) return;
                rectangleNode.ZOrder = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(ZOrder)));
            }
        }
        /// <summary>
        /// <see cref="Rectangle"/>の新しいインスタンスを初期化する
        /// </summary>
        public Rectangle()
        {
            rectangleNode = new RectangleNode();
            InnerTransformNode.AddChildNode(rectangleNode);
        }
    }
}
