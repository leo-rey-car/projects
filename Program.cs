using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoPrices
{
    internal class project
    {
        static void Main()
        {
            //Setting "dots" as decimal separators instead of "commas" for this program
            CultureInfo culture = new CultureInfo("en-US");
            culture.NumberFormat.NumberDecimalSeparator = ".";
            CultureInfo.CurrentCulture = culture;
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            string decimalSeparator = currentCulture.NumberFormat.NumberDecimalSeparator;
            Console.WriteLine("Current decimal separator (en-US): " + decimalSeparator);

            //Initialise new object to Preprocess the dataset
            preprocessing.prepro.prepro_methods prep = new preprocessing.prepro.prepro_methods();
            string filepath = @"C:\Users\leore\Downloads\aave-usd-clean.csv";
            List<string[]> table = prep.reader(filepath);
            prep.clean(table);

            //Splitting the dataset into Train and Test set
            (List<string[]> train_set, List<string[]> test_set) = prep.random_split(table);

            //Splitting both sets into input-matrix and output-array
            (double[,] i_train_set, double[] o_train_set) = prep.input_output(train_set);
            (double[,] i_test_set, double[] o_test_set) = prep.input_output(test_set);

            //Initialise new object to do the Linear Regression
            linear_regression.regression_class reg = new linear_regression.regression_class();
            Console.WriteLine("\nLinear Regression with features: Open (x0), High (x1), Low (x2) & Volume (x3)\n");
            reg.myregression(i_train_set, o_train_set, i_test_set, o_test_set);
        }
    }
}
