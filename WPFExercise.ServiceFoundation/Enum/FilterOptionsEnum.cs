using System.ComponentModel;

namespace WPFExercise.ServiceFoundation.Enum
{
    public enum FilterOptionsEnum
    {
        [Description("Sort input values in increasing order")]
        IncreaseOrder = 1,

        [Description("Sort input values in decreasing order")]
        DecreaseOrder = 2,

        [Description("Sum of the input values")]
        SumInput = 3,

        [Description("Filter odd input values")]
        OddInput = 4,

        [Description("Filter even input values")]
        EvenInput = 5,
    }
    
}

