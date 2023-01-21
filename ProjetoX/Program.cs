using System;
using tabuleiro;
using xadrex_C;

namespace ProjetoX
{

    class Program
    {
        static void Main(string[] args)
        {
            Tabuleiro tab = new Tabuleiro(8, 8);
            Tela.ImprimirTabuleiro(tab);

            Console.ReadLine();

        }
    }
}