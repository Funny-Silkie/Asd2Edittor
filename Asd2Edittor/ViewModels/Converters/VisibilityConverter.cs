using fslib3.WPF;
using System.Windows;

namespace Asd2Edittor.ViewModels
{
    public class VisibilityConverter : GenericValueConverterBase<string, Visibility>
    {
        public VisibilityConverter() { }
        public override bool TryConvert(string value, object parameter, out Visibility result)
        {
            result = string.IsNullOrEmpty(value) ? Visibility.Hidden : Visibility.Visible;
            return true;
        }
        public override bool TryConvertBack(Visibility value, object parameter, out string result)
        {
            result = default;
            return false;
        }
    }
}
