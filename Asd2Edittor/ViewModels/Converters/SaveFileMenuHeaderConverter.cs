using fslib3.WPF;

namespace Asd2Edittor.ViewModels
{
    public class SaveFileMenuHeaderConverter : GenericValueConverterBase<string, string>
    {
        public SaveFileMenuHeaderConverter() { }
        public override bool TryConvert(string value, object parameter, out string result)
        {
            if (string.IsNullOrEmpty(value)) result = "編集中ファイルの保存(_S)";
            else result = $"{value}の保存(_S)";
            return true;
        }
        public override bool TryConvertBack(string value, object parameter, out string result)
        {
            result = default;
            return false;
        }
    }
}
