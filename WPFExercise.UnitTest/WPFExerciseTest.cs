using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace WPFExercise.Windows
{
    [TestClass]
    public class WPFExerciseTest
    {
        [TestMethod]
        public void IsAbstractBaseClassTest()
        {

            var t = typeof(ViewModel);
            Assert.IsTrue(t.IsAbstract);
        }

        [TestMethod]
        public void IsIDataErrorInfoTest()
        {


            Assert.IsTrue(typeof(IDataErrorInfo).IsAssignableFrom(typeof(ViewModel)));
        }

        [TestMethod]
        public void FilterByIncreaseOrderTest()
        {

            var inputValues = new List<int>() { 1, 19, 3, 16, 19 };
            var filteredData = inputValues.Distinct().OrderBy(x => x);
            var outputValueList = string.Join(",", filteredData);

        }

        [TestMethod]
        public void FilterByDecreaseOrderTest()
        {
            var inputValues = new List<int>() { 1, 19, 3, 16, 19 };
            var filteredData = inputValues.Distinct().OrderByDescending(x => x);
            var outputValueList = string.Join(",", filteredData);

        }

        [TestMethod]
        public void FilterBySumInputTest()
        {
            var inputValues = new List<int>() { 1, 19, 3, 16, 19 };
            var sumData = inputValues.Distinct().Sum();
            var outputValueList = Convert.ToString(sumData);

        }

        [TestMethod]
        public void FilterByOddInputTest()
        {
            var inputValues = new List<int>() { 1, 19, 3, 16, 19 };
            var oddData = inputValues.Distinct().Where(x => x % 2 == 1).OrderBy(t => t);
            var outputValueList = string.Join(",", oddData);

        }

        [TestMethod]
        public void FilterByEvenInputTest()
        {
            var inputValues = new List<int>() { 1, 19, 3, 16, 19 };
            var evenData = inputValues.Distinct().Where(x => x % 2 == 0).OrderBy(t => t);
            var outputValueList = string.Join(",", evenData);

        }

    }
}
