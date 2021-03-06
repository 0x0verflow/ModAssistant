using System;
using System.Windows;
using System.Windows.Data;

namespace ModAssistant
{
    /// <summary>
    /// Interaction logic for OneClickStatus.xaml
    /// </summary>
    public partial class OneClickStatus : Window
    {
        public static OneClickStatus Instance;

        public string HistoryText
        {
            get
            {
                return HistoryTextBlock.Text;
            }
            set
            {
                Dispatcher.Invoke(new Action(() => { OneClickStatus.Instance.HistoryTextBlock.Text = value; }));
            }
        }
        public string MainText
        {
            get
            {
                return MainTextBlock.Text;
            }
            set
            {
                Dispatcher.Invoke(new Action(() => {
                    OneClickStatus.Instance.MainTextBlock.Text = value;
                    OneClickStatus.Instance.HistoryTextBlock.Text = string.IsNullOrEmpty(MainText) ? $"{value}\n" : $"{HistoryText}{value}\n";
                }));
            }
        }

        public void SetMainTextWithoutNl(string text) 
        {
            Dispatcher.Invoke(new Action(() => {
                OneClickStatus.Instance.MainTextBlock.Text += text;
                OneClickStatus.Instance.HistoryTextBlock.Text = $"{HistoryText}{text}";
            }));
        }

        public void ClearTitle()
        {
            Dispatcher.Invoke(new Action(() =>
            {
                OneClickStatus.Instance.MainTextBlock.Text = "";
            }));
        }

        public OneClickStatus()
        {
            InitializeComponent();
            Instance = App.OCIWindow != "No" ? this : null;
        }

        public void StopRotation()
        {
            Ring.Style = null;
        }
    }

    [ValueConversion(typeof(double), typeof(double))]
    public class DivideDoubleByTwoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(double))
            {
                throw new InvalidOperationException("The target must be a double");
            }
            double d = (double)value;
            return ((double)d) / 2;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
