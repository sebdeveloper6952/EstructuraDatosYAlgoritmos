using System;

namespace ExcerciseStack
{
    class Program
    {
        static void Main(string[] args)
        {
            Stack<int> stack = new Stack<int>();
            int n;
            Console.Write("Ingrese la cantidad de numeros: ");
            n = int.Parse(Console.ReadLine());
            for(int i = 0; i < n; i++)
            {
                Console.Write("Numero " + i + ": ");
                stack.push(int.Parse(Console.ReadLine()));
            }
            Console.WriteLine("Imprimir stack...");
            string s = "[ ";
            for(int i = 0; i < n; i++)
            {
                s += stack.pop() + " ";
            }
            s += "]";
            Console.WriteLine(s);
            Console.ReadKey();
        }
    }
}
