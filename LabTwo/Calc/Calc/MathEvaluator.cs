using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc
{
    class MathEvaluator
    {
        public static string Evaluate(string expression)
        {
            try
            {
                MSScriptControl.ScriptControl sc = new MSScriptControl.ScriptControl();
                sc.Language = "VBScript";
                object result = sc.Eval(expression);
                return result.ToString();
            }
            catch
            {
                return "";
            }
        }
    }
}
