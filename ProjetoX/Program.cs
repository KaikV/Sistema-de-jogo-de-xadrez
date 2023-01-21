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
            try
            {
                Tabuleiro tab = new Tabuleiro(8, 8);

                tab.ColocarPeca(new Torre(tab, Cores.Preta), new Posicao(0, 0));
                tab.ColocarPeca(new Torre(tab, Cores.Preta), new Posicao(1, 4));
                tab.ColocarPeca(new Rei(tab, Cores.Preta), new Posicao(1, 5));


                tab.ColocarPeca(new Torre(tab, Cores.Azul), new Posicao(3, 5));
                Tela.ImprimirTabuleiro(tab);
                Console.ReadLine();
            }
            catch (TabuleiroException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}