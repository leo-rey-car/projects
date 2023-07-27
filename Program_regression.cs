using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Statistics.Models.Regression.Linear;
using Accord.Math.Optimization.Losses;

namespace linear_regression
{
    internal class regression_class
    {
        public void myregression(double[,] i_trainset, double[] o_trainset, double[,] i_testset, double[] o_testset)
        {
            //OrdinaryLeastSquares used to create a Linear Regression model
            var OLS = new OrdinaryLeastSquares()
            {
                //We impose the model to have an intercept term
                UseIntercept = true,
                IsRobust = true
            };

            //Changing the formats to properly implement the methods
            int rows = i_trainset.GetLength(0);
            int cols = i_trainset.GetLength(1);
            double[][] i_trainset_new = new double[rows][];
            for (int i = 0; i < rows; i++)
            {
                i_trainset_new[i] = new double[cols];
                for (int j = 0; j < cols; j++)
                {
                    i_trainset_new[i][j] = i_trainset[i, j];
                }
            }

            rows = i_testset.GetLength(0);
            cols = i_testset.GetLength(1);
            double[][] i_testset_new = new double[rows][];
            for (int i = 0; i < rows; i++)
            {
                i_testset_new[i] = new double[cols];
                for (int j = 0; j < cols; j++)
                {
                    i_testset_new[i][j] = i_testset[i, j];
                }
            }

            //Learn method is used to estimate the coefficients of the model
            MultipleLinearRegression regression = OLS.Learn(i_trainset_new, o_trainset);

            //Since we will use 4 input variables, the resulting model will have 5 coefficients:
            double c0 = regression.Coefficients[0];
            double c1 = regression.Coefficients[1];
            double c2 = regression.Coefficients[2];
            double c3 = regression.Coefficients[3];
            double ci = regression.Intercept;
            Console.WriteLine("Learned coefficients: " + String.Join(" ", c0, " ", c1, " ", c2, " ", c3) + " " + ci);
            Console.WriteLine("Learned relationship: " + regression.ToString());

            //To compute the predicted values and the Coefficient of Determination R²
            double[] pred_val = regression.Transform(i_testset_new);
            double R2 = new RSquaredLoss(o_testset.Length, o_testset).Loss(pred_val);
            Console.WriteLine("R^2: " + R2);
        }
    }
}