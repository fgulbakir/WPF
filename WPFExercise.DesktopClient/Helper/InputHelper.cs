using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPFExercise.DesktopClient.Helper
{
    public class InputHelper : TextBox
    {
        #region Methods

        /// <summary>
        /// InputHelper class checks whether input values are integer.
        /// I defined custom dependency property.Dependency property is a specific type of property which extends the CLR property.
        /// Dependency propery  used for set the style,data binding ,set with a resource (a static or a dynamic resource)
        /// I defined  helper:InputHelper.IsNumeric="True" in MainWindow.xaml.
        ///in this way , IsNumeric method called from  InputHelper class . 
        /// </summary>


        static void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9-]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        static void TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (tb != null && string.IsNullOrWhiteSpace(tb.Text))
            {
                tb.Focus();
                tb.SelectionStart = tb.Text.Length;
            }
        }

        static void PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = e.Key == Key.Space;
        }

        public static bool GetIsNumeric(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsNumericProperty);
        }

        public static void SetIsNumeric(DependencyObject obj, bool value)
        {
            obj.SetValue(IsNumericProperty, value);
        }

        public static readonly DependencyProperty IsNumericProperty 
            = DependencyProperty.RegisterAttached("IsNumeric",
            typeof(bool), typeof(InputHelper), new PropertyMetadata(false, new PropertyChangedCallback((s, e) =>
            {
                TextBox currentTextbox = s as TextBox;
                if (currentTextbox != null)
                {
                    if ((bool)e.OldValue && !((bool)e.NewValue))
                    {
                        currentTextbox.PreviewTextInput -= PreviewTextInput;
                    }

                    if ((bool)e.NewValue)
                    {
                        currentTextbox.PreviewTextInput += PreviewTextInput;
                        currentTextbox.PreviewKeyDown += PreviewKeyDown;
                        currentTextbox.TextChanged += new TextChangedEventHandler(TextChanged);
                    }
                }
            })));

        #endregion
    }
}
