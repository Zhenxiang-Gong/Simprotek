#include <windows.h>
#include <iostream>
#include <strstream>
#include <cstdlib>
#include <string>
#include <cmath>
using namespace std;

BOOL __stdcall DllMain(HINSTANCE hInst, DWORD dwReason, LPVOID lpReserved) {
   return TRUE;
}

extern int strcmp(const char*, const char*);
extern char* strcpy(char*, const char*);

struct name {
   char* string;
   name* next;
   double value;
};

enum token_value
{
   NAME,
   NUMBER,
   END,
   PLUS='+',
   MINUS='-',
   MUL='*',
   DIV='/',
   POW='^',
   PRINT=';',
   ASSIGN='=',
   LP='(',
   RP=')'
};

double number_value;
char name_string[256];
token_value curr_tok;

double term(istrstream& mycin); 
double error(const char* s);
name* look(const char* p, int ins =0);
token_value get_token(istrstream& mycin);
double expr(istrstream& mycin);
double prim(istrstream& mycin);
double power(istrstream& mycin);

const int TBLSZ = 23;
name* table[TBLSZ];
int no_of_errors;

double error(const char* s)
{
   cerr << "error: " << s << '\n';
   no_of_errors++;
   return 1;
}

name* look(const char* p, int ins)
{
   int ii = 0; // hash
   const char* pp = p;
   while (*pp)
      ii = (ii<<1) ^ (*(pp++));
   if (ii < 0)
      ii = -ii;
   ii %= TBLSZ;

   for (name* n=table[ii]; n; n=n->next) // search
      if (strcmp(p,n->string) == 0)
         return n;

   if (ins == 0)
      error("name not found");

   name* nn = new name;
   nn->string = new char[strlen(p)+1];
   strcpy(nn->string,p);
   nn->value = 1;
   nn->next = table[ii];
   table[ii] = nn;
   return nn;
}

inline name* insert(const char* s)
{
   return look(s,1);
}

token_value get_token(istrstream& mycin)
{
   char ch;
   
   do
   { // skip whitespaces exept '\n'
      if (!mycin.get(ch))
         return curr_tok = END;
   } while (ch!='\n' && isspace(ch));

   switch (ch)
   {
   case ';':
   case '\n':
      return curr_tok=PRINT;
   case '^':
   case '*':
   case '/':
   case '+':
   case '-':
   case '(':
   case ')':
   case '=':
      return curr_tok=token_value(ch);
   case '0':
   case '1':
   case '2':
   case '3':
   case '4':
   case '5':
   case '6':
   case '7':
   case '8':
   case '9':
   case '.':
      mycin.putback(ch);
      mycin >> number_value;
      return curr_tok=NUMBER;
   default:
      if (isalpha(ch))
      {
         char* p = name_string;
         *p++ = ch;
         while (mycin.get(ch) && isalnum(ch))
            *p++ = ch;
         mycin.putback(ch);
         *p = 0;
         return curr_tok=NAME;
      }
      error("bad_token");
      return curr_tok=PRINT;
   }
}

double expr(istrstream& mycin)
{
   double left = term(mycin);

   for (;;)
   {
      switch (curr_tok)
      {
      case PLUS:
         get_token(mycin);
         left += term(mycin);
         break;
      case MINUS:
         get_token(mycin);
         left -= term(mycin);
         break;
      default:
         return left;
      }
   }
}

double term(istrstream& mycin)
{
   double left = power(mycin);

   for (;;)
   {
      switch (curr_tok)
      {
      case MUL:
         get_token(mycin);
         left *= prim(mycin);
         break;
      case DIV:
         {
            get_token(mycin);
            double d = prim(mycin);
            if (d ==0)
               return error("divide by 0");
            left /= d;
            break;
         }
      default:
         return left;
      }
   }
}

double power(istrstream& mycin)
{
   double left = prim(mycin);

   for (;;)
   {
      switch (curr_tok)
      {
      case POW:
         get_token(mycin);
         left = pow(left, prim(mycin));
         break;
      default:
         return left;
      }
   }
}

double prim(istrstream& mycin)
{
   switch (curr_tok)
   {
   case NUMBER:
      get_token(mycin);
      return number_value;
   case NAME:
      if (get_token(mycin) == ASSIGN)
      {
         name* n = insert(name_string);
         get_token(mycin);
         n->value = expr(mycin);
         return n->value;
      }
      return look(name_string)->value;
   case MINUS:
      get_token(mycin);
      return -prim(mycin);
   case LP:
      {
         get_token(mycin);
         double e = expr(mycin);
         if (curr_tok != RP)
            return error(") expected");
         get_token(mycin);
         return e;
      }
   case END:
      return 1;
   default:
      return error("primary expected");
   }
}

extern "C" __declspec(dllexport) double __stdcall calculateExpression(char* expression)
{
   istrstream mycin(expression);

   // insert pre-defined names:
   insert("pi")->value = 3.141592653589793;
   insert("e")->value = 2.718281828459045;

//   while (mycin)
//   {
      get_token(mycin);
//      if (curr_tok == END)
//         break;
//     if (curr_tok == PRINT)
//         continue;
      return expr(mycin);
//   }
}

int main()
{
   cout << calculateExpression("2*pi") << '\n';
   return no_of_errors;
}
