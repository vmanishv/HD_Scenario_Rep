using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HD_Scenario
{
    public class SplitFunction
    {
        public static String SplitFunctionValueDollar(String inputdata)
        {
            string s = inputdata;
            string[] words = s.Split('$');
            foreach (string word in words)
            {
                //Console.WriteLine(word);
            }
            return (words[1]);
        }

    }
}
