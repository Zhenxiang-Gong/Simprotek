using System;
using System.Collections;
//using Prosimo.Calculator;

namespace Prosimo.UnitOperations.Calculator
{
	/// <summary>
	/// Summary description for FormulaCalculator.
	/// </summary>
	public class FormulaCalculator
	{
      private static FormulaCalculator formulaCalculator;

      private FormulaCalculator()
      {
      }

      public static FormulaCalculator GetInstance() 
      {
         if (formulaCalculator == null) 
         {
            formulaCalculator = new FormulaCalculator();
         }
         return formulaCalculator;
      }

      public double Calculate(int id, UnitOperationSystem unitOpSystem, Hashtable values)
      {
         string formula = (string)unitOpSystem.FormulaTable[id];

         // the keys is needed to sort the keys in the hashmap
         // and start replacing the longer ids with the values in the formula
         ArrayList keys = new ArrayList();

         IEnumerator e = values.GetEnumerator();
         while (e.MoveNext())
         {
            IDictionaryEnumerator de = (IDictionaryEnumerator)e.Current;
            keys.Add(de.Key);
         }

         keys.Sort(); // numbers in increasing order

         // replace the literals with their values
         for (int i=0; i<keys.Count; i++)
         {
            int key = (int)keys[keys.Count-1-i];
            double val = (double)values[key];
            string keyStr = "v" + key.ToString();
            string valStr = val.ToString();
            formula = formula.Replace(keyStr, valStr);
         }

         // calculate the numerical expression
         return ExpressionCalculator.CalculateExpression(formula);
      }

      public ArrayList GetIndependentVariables(int id, UnitOperationSystem unitOpSystem)
      {
         string formula = (string)unitOpSystem.FormulaTable[id];

         // get a list with indexes of v
         ArrayList vs = new ArrayList();
         for (int i=0; i<formula.Length; i++)
         {
            char c = formula[i];
            if (c == 'v')
               vs.Add(i);
         }

         // for every index (v) find the id and put it on the list
         ArrayList ids = new ArrayList();
         IEnumerator e = vs.GetEnumerator();
         while (e.MoveNext())
         {
            int idx = (int)e.Current;
            int identifier = this.GetId(idx, formula);
            ids.Add(identifier);
         }
         return ids;
      }

      private int GetId(int idx, string formula)
      {
         // we look for vnnn...and return nnn...
         string f1 = formula.Substring(idx+1);
         int i=0;
         for (i=0; i<f1.Length; i++)
         {
            char c = f1[i];
            //if (!this.IsDigit(c))
            if (!Char.IsDigit(c))
               break;
         }
         string f2 = f1.Substring(0, i);
         int id = System.Convert.ToInt32(f2);
         return id;
      }

      /*private bool IsDigit(char c)
      {
         if (c == '0' || c == '1' || c == '2' || c == '3' || c == '4' ||
             c == '5' || c == '6' || c == '7' || c == '8' || c == '9')
            return true;
         else
            return false;
      }*/
	}
}
