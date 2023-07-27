using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace preprocessing
{
    internal class prepro
    {
        public class prepro_methods
        {
            //Method to upload and collect Data
            public List<string[]> reader(string filepath)
            {
                List<string[]> table = new List<string[]>();
                StreamReader stream = new StreamReader(filepath);
                while (!stream.EndOfStream)
                {
                    string[] line = stream.ReadLine().Split(',');
                    //Convert each element of the line to the appropriate data type
                    for (int i = 0; i < line.Length; i++)
                    {
                        double value;
                        if (double.TryParse(line[i], out value))
                        {
                            line[i] = value.ToString();
                        }
                    }
                    table.Add(line);
                }
                return table;
            }

            // For each Data point check every feature
            // Store the index of invalid data points (e.g. missing or negative values)
            public void clean(List<string[]> table)
            {
                string issue = "\nThe following rows have missing or negative values: ";
                int i = 0;
                List<int> faulty_data = new List<int>();
                foreach (var item in table)
                {
                    if (double.Parse(item[0]) < 0 || item[0] == null)
                    {
                        issue = issue + " " + i;
                        faulty_data.Add(i);
                    }

                    if (double.Parse(item[1]) < 0 || item[1] == null)
                    {
                        issue = issue + " " + i;
                        if (!faulty_data.Contains(i))
                        {
                            faulty_data.Add(i);
                        }
                    }

                    if (double.Parse(item[2]) < 0 || item[2] == null)
                    {
                        issue = issue + " " + i;
                        if (!faulty_data.Contains(i))
                        {
                            faulty_data.Add(i);
                        }
                    }

                    if (double.Parse(item[3]) < 0 || item[3] == null)
                    {
                        issue = issue + " " + i;
                        if (!faulty_data.Contains(i))
                        {
                            faulty_data.Add(i);
                        }
                    }

                    if (double.Parse(item[4]) < 0 || item[4] == null)
                    {
                        issue = issue + " " + i;
                        if (!faulty_data.Contains(i))
                        {
                            faulty_data.Add(i);
                        }
                    }

                    if (double.Parse(item[5]) < 0 || item[5] == null)
                    {
                        issue = issue + " " + i;
                        if (!faulty_data.Contains(i))
                        {
                            faulty_data.Add(i);
                        }
                    }

                    i++;
                }

                //Remove rows with negative or missing data
                int j = 0;
                foreach (int k in faulty_data)
                {
                    table.RemoveAt(k - j);
                    j++;
                }

                Console.WriteLine(issue);
            }

            //Splitting in Training set (80% of the Data) and Test set (20% of the Data)
            public (List<string[]>, List<string[]>) random_split(List<string[]> tab)
            {

                var train_list = new List<string[]>();
                var test_list = new List<string[]>();

                //Initialisation Random object
                var random = new Random();
                int max = tab.Count;

                //Generation of the Training set and saving it as CSV file
                for (int i = max; i > Math.Ceiling(0.2 * max); i--)
                {
                    var random_j = random.Next(tab.Count);
                    var random_item = tab[random_j];
                    train_list.Add(random_item);
                    tab.RemoveAt(random_j);
                }
                string train_path = @"C:\Users\leore\Downloads\train_list.csv";
                using (var file = File.CreateText(train_path))
                {
                    foreach (var arr in train_list)
                    {
                        file.WriteLine(string.Join(",", arr));
                    }
                }

                //Generation of the Test set and saving it as CSV file
                int newtop = tab.Count;
                for (int i = newtop; i > 0; i--)
                {
                    var random_j = random.Next(tab.Count);
                    var random_item = tab[random_j];
                    test_list.Add(random_item);
                    tab.RemoveAt(random_j);
                }
                string test_path = @"C:\Users\leore\Downloads\test_list.csv";
                using (var file = File.CreateText(test_path))
                {
                    foreach (var arr in test_list)
                    {
                        file.WriteLine(string.Join(",", arr));
                    }
                }

                return (train_list, test_list);

            }

            //Organise the input and output sets to procede with the Linear Regression, which:
            //will try to predict (output) the value of feature 2
            //will NOT take into consideration (input) the feature 0
            public (double[,], double[]) input_output(List<string[]> table)
            {
                double[,] i_set = new double[table.Count,4];
                double[] o_set = new double[table.Count];
                for (int i = 0; i < table.Count; i++)
                {
                    for (int k = 1; k < 6; k++)
                    {
                        if (k != 2)
                        {
                            for (int j = 0; j < 4; j++)
                            {
                                i_set[i, j] = double.Parse(table[i][k]);
                            };
                        }
                    }
                    o_set[i] = double.Parse(table[i][2]);
                }
                return (i_set, o_set);
            }
        }
    }
}
