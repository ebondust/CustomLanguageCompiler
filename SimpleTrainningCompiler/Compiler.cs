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
            public Token(int type, string lexeme)
            {
                this.type = type;
                this.lexeme = lexeme;
            }
            public int type;
            public string lexeme;
        }

        public List<string> Output = new List<string>();

        public List<string> OutputCode = new List<string>();

        private List<Token> tokens;

        private int tIndex = -1;

        private Token currentToken
        {
            get
            {
                return tokens[tIndex];
            }
        }

        private Token nextToken()
        {
            tIndex++;
            return tokens[tIndex];
        }

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
            Output.AddRange(tokens.Select(x => "Token: " + x.type + " Lexeme: " + x.lexeme));
        }

        public void Parse()
        {
            statements();
        }

        private void statements()
        {
            while(nextToken().type != EOF)
            {
                expression();
                if (currentToken.type != SEMICOLON)
                {
                    Output.Add("missing semicolon");
                    return;
                }
            }
        }

        private void expression()
        {

            Token a1 = term();
            while (nextToken().type == MINUS || currentToken.type == PLUS)
            {
                Token a2 = term();
                OutputCode.Add("ADD "+a1.lexeme+", "+a2.lexeme);
            }
        }

        private Token term()
        {
            var a1 = factor();
            while(nextToken().type == TIMES || currentToken.type == OVER)
            {
                Token a2 = factor();
                if (currentToken.type == OVER)
                {
                    OutputCode.Add();
                }
                else
                {
                    var a
                }
            }
        }

        private void assigment()
        {

        }

        private Token factor()
        {
            if (currentToken.type == NUMBER)
                return currentToken;
            else
            {
                Output.Add("Expected number, got "+currentToken.lexeme);
                return new Token(EOF,"EOf");
            }
        }


            bool isSpace(char a)
        {
            return (a == ' ' || a == '\n' || a == '\t') ? true : false;
        }

        bool isNumber(char a)
        {
            return Regex.IsMatch(a + "", "^[0-9]+$");
        }
        protected bool isVariable(char a)
        {
            return Regex.IsMatch(a + "", "^[A-Za-z]+$");
        }

        public string GetOutput()
        {
            return string.Join("\n", Output.ToArray());
        }
    }
}
