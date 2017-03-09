using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SimpleTrainingCompiler
{
    public class Compiler
    {
        //Tokens
        private const int EOF = 0;
        private const int SEMICOLON = 1;
        private const int PLUS = 2;
        private const int MINUS = 3;
        private const int TIMES = 4;
        private const int OVER = 5;
        private const int LEFT_PAR = 6;
        private const int RIGHT_PAR = 7;
        private const int EQUAL = 8;
        private const int NUMBER = 9;
        private const int VARIABLE = 10;

        struct Token
        {
            public Token(int value, string lexeme)
            {
                this.value = value;
                this.lexeme = lexeme;
            }
            public int value;
            public string lexeme;
        }

        public List<string> Output = new List<string>();

        private List<Token> tokens;

        public void Compile(string code)
        {
            tokenize(code);
        }

        private void tokenize(string code)
        {
            tokens = new List<Token>();
            int current;

            for (current = 0; current < code.Length; current++)
            {
                while (code.Length > current && isSpace(code[current]))
                    current++;
                switch (code[current])
                {
                    case ';': tokens.Add(new Token(SEMICOLON, ";")); break;
                    case '+': tokens.Add(new Token(PLUS, "+")); break;
                    case '-': tokens.Add(new Token(SEMICOLON, "-")); break;
                    case '*': tokens.Add(new Token(TIMES, "*")); break;
                    case '/': tokens.Add(new Token(OVER, "/")); break;
                    case '(': tokens.Add(new Token(LEFT_PAR, "(")); break;
                    case ')': tokens.Add(new Token(RIGHT_PAR, ")")); break;
                    case '=': tokens.Add(new Token(EQUAL, "=")); break;
                    default:
                        string value = "";

                        if (!isNumber(code[current]))
                        {
                            if (isVariable(code[current]))
                            {

                                do
                                {
                                    value += "" + code[current];
                                    current++;
                                } while (current < code.Length && isVariable(code[current]));
                                if (current < code.Length)
                                    current--;

                                tokens.Add(new Token(VARIABLE, value));
                            }
                            else
                            {
                                Output.Add("Invalid character :" + code[current]);
                                tokens.Add(new Token(EOF, ""));
                                return;
                            }
                        }
                        else
                        {
                            do
                            {
                                value += "" + code[current];
                                current++;
                            } while (current < code.Length && isNumber(code[current]));

                            if (current < code.Length)
                                current--;

                            tokens.Add(new Token(NUMBER, value));
                        }

                        break;
                }
            }
            tokens.Add(new Token(EOF, ""));
            Output.Add("Tokenization Completed");
            Output.AddRange(tokens.Select(x => "Token: " + x.value + " Lexeme: " + x.lexeme));
        }

        bool isSpace(char a)
        {
            return (a == ' ' || a == '\n' || a == '\t') ? true : false;
        }

        bool isNumber(char a)
        {
            return (Regex.IsMatch(a + "", "^[0-9]+$")) ? true : false;
        }
        protected bool isVariable(char a)
        {
            return (Regex.IsMatch(a + "", "^[A-Za-z]+$")) ? true : false;
        }

        public string GetOutput()
        {
            return string.Join("\n", Output.ToArray());
        }
    }
}
