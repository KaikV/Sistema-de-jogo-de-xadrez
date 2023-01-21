using tabuleiro;
namespace xadrez
{
    internal class PartidaDeXadrez
    {
        public Tabuleiro tab { get; private set; }
        private int Turno;
        private Cores JogadorAtual; 
        public bool Terminada { get; private set; }


        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cores.Branca;
            ColocarPecas();
        }
        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.RetirarPeca(origem);
            p.ImplementarQteMovimentos();
            Peca pecaCaputrada = tab.RetirarPeca(destino);
            tab.ColocarPeca(p, destino);
        }

        private void ColocarPecas()
        {

            tab.ColocarPeca(new Torre(tab, Cores.Branca), new PosicaoXadrez('c', 1).toPosicao());  
            tab.ColocarPeca(new Torre(tab, Cores.Branca), new PosicaoXadrez('c', 2).toPosicao());  
            tab.ColocarPeca(new Torre(tab, Cores.Branca), new PosicaoXadrez('d', 2).toPosicao());  
            tab.ColocarPeca(new Torre(tab, Cores.Branca), new PosicaoXadrez('e', 2).toPosicao());  
            tab.ColocarPeca(new Torre(tab, Cores.Branca), new PosicaoXadrez('e', 1).toPosicao());  
            tab.ColocarPeca(new Rei(tab, Cores.Branca), new PosicaoXadrez('d', 1).toPosicao());

            tab.ColocarPeca(new Torre(tab, Cores.Preta), new PosicaoXadrez('c', 8).toPosicao());  
            tab.ColocarPeca(new Torre(tab, Cores.Preta), new PosicaoXadrez('c', 7).toPosicao());  
            tab.ColocarPeca(new Torre(tab, Cores.Preta), new PosicaoXadrez('d', 7).toPosicao());  
            tab.ColocarPeca(new Torre(tab, Cores.Preta), new PosicaoXadrez('e', 7).toPosicao());  
            tab.ColocarPeca(new Torre(tab, Cores.Preta), new PosicaoXadrez('e', 8).toPosicao());  
            tab.ColocarPeca(new Rei(tab, Cores.Preta), new PosicaoXadrez('d', 8).toPosicao());  
        }



    }
}
