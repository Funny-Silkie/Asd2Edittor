using Altseed2;
using System.Collections.Generic;
using System.ComponentModel;

namespace Asd2UI.Altseed2
{
    /// <summary>
    /// ポリゴンのUIノード
    /// </summary>
    public class Polygon : UINode
    {
        private readonly PolygonNode polygonNode;
        /// <summary>
        /// アルファブレンドを取得または設定する
        /// </summary>
        public AlphaBlend AlphaBlend
        {
            get => polygonNode.AlphaBlend;
            set
            {
                if (AlphaBlend == value) return;
                polygonNode.AlphaBlend = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(AlphaBlend)));
            }
        }
        /// <inheritdoc/>
        public override ulong CameraGroup
        {
            get => polygonNode.CameraGroup;
            set
            {
                if (CameraGroup == value) return;
                polygonNode.CameraGroup = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(CameraGroup)));
            }
        }
        /// <inheritdoc/>
        public override Vector2F ContentSize => polygonNode.ContentSize;
        /// <inheritdoc/>
        public override bool IsDrawn
        {
            get => polygonNode.IsDrawn;
            set
            {
                if (IsDrawn == value) return;
                polygonNode.IsDrawn = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(IsDrawn)));
            }
        }
        /// <summary>
        /// 重ね順を取得または設定する
        /// </summary>
        public IReadOnlyList<Vertex> Vertexes
        {
            get => polygonNode.Vertexes;
            set
            {
                if (Vertexes == value) return;
                polygonNode.Vertexes = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Vertexes)));
            }
        }
        /// <summary>
        /// 重ね順を取得または設定する
        /// </summary>
        public int ZOrder
        {
            get => polygonNode.ZOrder;
            set
            {
                if (ZOrder == value) return;
                polygonNode.ZOrder = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(ZOrder)));
            }
        }
        /// <summary>
        /// <see cref="Polygon"/>の新しいインスタンスを初期化する
        /// </summary>
        public Polygon()
        {
            polygonNode = new PolygonNode();
            InnerTransformNode.AddChildNode(polygonNode);
        }
    }
}
