using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;

namespace WPFExercise.ServiceFoundation.Converter
{
    /// <summary>
    /// The enum which defined for filter combobox is get description
    /// EnumBindingConverter class is called  from MainWindow.xaml - <converter:EnumBindingConverter x:Key="enumBindingConverter"/>
    /// </summary>
    public class EnumBindingConverter : IValueConverter
    {
        #region ConvertMethods

        private string GetEnumDescription(System.Enum objEnum)
        {
            var fieldInfo = objEnum.GetType().GetField(objEnum.ToString());
            var attributeArray = fieldInfo.GetCustomAttributes(false);

            if (attributeArray.Length == 0)
                return objEnum.ToString();
            else
            {
                DescriptionAttribute descAttribute = null;

                foreach (var atb in attributeArray)
                {
                    if (atb is DescriptionAttribute) descAttribute = atb as DescriptionAttribute;
                }

                if (descAttribute != null) return descAttribute.Description;

                return objEnum.ToString();
            }
        }

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var currentEnum = (System.Enum) value;
            string enumDescription = GetEnumDescription(currentEnum);
            return enumDescription;
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.Empty;
        }

        #endregion
    }
}

