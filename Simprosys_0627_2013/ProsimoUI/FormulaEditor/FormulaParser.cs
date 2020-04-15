using System;

namespace ProsimoUI.FormulaEditor
{
	/// <summary>
	/// Summary description for FormulaParser.
	/// </summary>
	public class FormulaParser
	{
      private static FormulaParser formulaParser;

      private FormulaParser()
      {
      }

      public static FormulaParser GetInstance() 
      {
         if (formulaParser == null) 
         {
            formulaParser = new FormulaParser();
         }
         return formulaParser;
      }

      public bool Validate(string formula)
      {
         return this.IsFirstCharacterOk(formula) &&
            this.IsLastCharacterOk(formula) &&
            this.ContainsAllowedCharacters(formula) &&
            this.HasSameNoOfParenthesis(formula);
      }

      private bool IsFirstCharacterOk(string formula)
      {
         bool ok = true;
         if (formula.StartsWith("*") || formula.StartsWith(")") || formula.StartsWith("^") ||
             formula.StartsWith(".") || formula.StartsWith("/"))
            ok = false;
         return ok;
      }

      private bool IsLastCharacterOk(string formula)
      {
         bool ok = true;
         if (formula.EndsWith("v") || formula.EndsWith("(") || formula.EndsWith("*") ||
            formula.EndsWith("/") || formula.EndsWith("^") || formula.EndsWith("+") ||
            formula.EndsWith("-") || formula.EndsWith("."))
            ok = false;
         return ok;
      }

      private bool ContainsAllowedCharacters(string formula)
      {
         bool ok = true;
         for (int i=0; i<formula.Length; i++)
         {
            char c = formula[i];
            if (!this.IsAllowedCharacter(c))
            {
               ok = false;
               break;
            }
         }
         return ok;
      }

      private bool IsAllowedCharacter(char c)
      {
         bool allowed = false;
         if (c == '0' || c == '1' || c == '2' || c == '3' || c == '4' || c == '5' ||
             c == '6' || c == '7' || c == '8' || c == '9' || c == '.' || c == '+' ||
             c == '-' || c == '*' || c == '/' || c == '^' || c == '(' || c == ')' ||
             c == 'v' || c == 'e' || c == 'p' || c == 'i')
         {
            allowed = true;
         }
         return allowed;
      }

      private bool HasSameNoOfParenthesis(string formula)
      {
         int count1 = this.CountChar(formula, '(');
         int count2 = this.CountChar(formula, ')');
         return count1 == count2;
      }

      private int CountChar(string formula, char c)
      {
         int count = 0;
         for (int i=0; i<formula.Length; i++)
         {
            char ch = formula[i];
            if (ch == c)
               count++;
         }
         return count;
      }
	}
}
