using System;
using tabuleiro;
using xadrex_C;
using xadrez;

namespace ProjetoX
{

    class Program
    {
        static void Main(string[] args)
        {
            Tabuleiro tab = new Tabuleiro(8, 8);
            
            tab.ColocarPeca(new Torre(tab, Cores.Preta) , new Posicao(0, 0));
            tab.ColocarPeca(new Torre(tab, Cores.Preta), new Posicao(1, 3));
            tab.ColocarPeca(new Rei(tab, Cores.Preta), new Posicao(2, 4));
            Tela.ImprimirTabuleiro(tab);

            Console.ReadLine();

        }
    }
}