using System;
using System.Linq.Expressions;
using tabuleiro;
using xadrex_C;
using xadrez;

namespace ProjetoX
{

    class Program
    {
        static void Main(string[] args)
        {

            PosicaoXadrez pos = new PosicaoXadrez('c', 7);

            Console.WriteLine(pos.toPosicao());

            Console.ReadLine();
        }
    }
}