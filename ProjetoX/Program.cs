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
            try { 
            Tabuleiro tab = new Tabuleiro(8, 8);
           
            tab.ColocarPeca(new Torre(tab, Cores.Preta) , new Posicao(0, 0));
            tab.ColocarPeca(new Torre(tab, Cores.Preta), new Posicao(1, 9));
            tab.ColocarPeca(new Rei(tab, Cores.Preta), new Posicao(0, 0));
            Tela.ImprimirTabuleiro(tab);

            Console.ReadLine();
            }
            catch (TabuleiroException ex) {
                Console.WriteLine(ex.Message);
            }
        }
    }
}