using SimpleTrainingCompiler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CustomLanguageCompiler
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Type in a mathematical expression: ");
            string code = Console.ReadLine();
            Compiler compiler = new Compiler();
            compiler.Compile(code);
            
            Console.WriteLine(compiler.GetOutput());
            Console.WriteLine(compiler.GetOutputCode());
            Console.ReadLine();
        }
    }
}
