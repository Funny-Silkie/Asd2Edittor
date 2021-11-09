using Altseed2;
using System.ComponentModel;

namespace Asd2UI.Altseed2
{
    /// <summary>
    /// UI用のノードの基底クラス
    /// </summary>
    public abstract class UINode : Node, INotifyPropertyChanged
    {
        /// <summary>
        /// 内部の<see cref="TransitionNode"/>を取得する
        /// </summary>
        protected internal TransformNode InnerTransformNode { get; }
        /// <summary>
        /// 角度（度数法）を取得または設定する
        /// </summary>
        public float Angle
        {
            get => InnerTransformNode.Angle; set
            {
                if (Angle == value) return;
                InnerTransformNode.Angle = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Angle)));
            }
        }
        /// <summary>
        /// カメラグループを取得または設定する
        /// </summary>
        public abstract ulong CameraGroup { get; set; }
        /// <summary>
        /// 回転の中心座標を取得または設定する
        /// </summary>
        public Vector2F CenterPosition
        {
            get => InnerTransformNode.CenterPosition;
            set
            {
                if (CenterPosition == value) return;
                InnerTransformNode.CenterPosition = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(CenterPosition)));
            }
        }
        /// <summary>
        /// 大きさを取得する
        /// </summary>
        public abstract Vector2F ContentSize { get; }
        /// <summary>
        /// 左右方向の反転をするかどうかを取得または設定する
        /// </summary>
        public bool HorizontalFlip
        {
            get => HorizontalFlip;
            set
            {
                if (InnerTransformNode.HorizontalFlip == value) return;
                InnerTransformNode.HorizontalFlip = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(HorizontalFlip)));
            }
        }
        /// <summary>
        /// 描画されるかどうかを取得または設定する
        /// </summary>
        public abstract bool IsDrawn { get; set; }
        /// <summary>
        /// 座標を取得または設定する
        /// </summary>
        public Vector2F Position
        {
            get => InnerTransformNode.Position;
            set
            {
                if (Position == value) return;
                InnerTransformNode.Position = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Position)));
            }
        }
        /// <summary>
        /// 拡大率を取得または設定する
        /// </summary>
        public Vector2F Scale
        {
            get => InnerTransformNode.Scale;
            set
            {
                if (Scale == value) return;
                InnerTransformNode.Scale = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Scale)));
            }
        }
        /// <summary>
        /// 上下方向の反転をするかどうかを取得または設定する
        /// </summary>
        public bool VerticalFlip
        {
            get => InnerTransformNode.VerticalFlip;
            set
            {
                if (VerticalFlip == value) return;
                InnerTransformNode.VerticalFlip = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(VerticalFlip)));
            }
        }
        /// <summary>
        /// <see cref="UINode"/>の新しいインスタンスを初期化する
        /// </summary>
        protected UINode()
        {
            InnerTransformNode = CreateInnerNode();
            AddChildNode(InnerTransformNode);
        }
        /// <summary>
        /// プロパティが変更されたときに実行される
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// <see cref="PropertyChanged"/>を実行する
        /// </summary>
        /// <param name="e">イベントの情報</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e) => PropertyChanged?.Invoke(this, e);
        /// <summary>
        /// <see cref="InnerTransformNode"/>を生成する
        /// </summary>
        /// <returns><see cref="InnerTransformNode"/>用のインスタンス</returns>
        protected abstract TransformNode CreateInnerNode();
    }
}
