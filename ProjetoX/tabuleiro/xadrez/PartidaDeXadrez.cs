using System;
using System.Runtime.ConstrainedExecution;
using tabuleiro;


namespace xadrez
{
    class PartidaDeXadrez
    {

        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }
        public Cores jogadorAtual { get; private set; }
        public bool terminada { get; private set; }

        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cores.Branca;
            terminada = false;
            colocarPecas();
        }
        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.RetirarPeca(origem);
            p.ImplementarQteMovimentos();
            Peca pecaCapturada = tab.RetirarPeca(destino);
            tab.ColocarPeca(p, destino);
            
        }

        
        public void realizaJogada(Posicao origem, Posicao destino)
        {
            ExecutaMovimento(origem, destino);
            turno++;
            mudaJogador();
        }

        public void validarPosicaoDeOrigem(Posicao pos)
        {
            if (tab.peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }
            if (jogadorAtual != tab.peca(pos).cor)
            {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }
            if (!tab.peca(pos).ExisteMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possíveis para a peça de origem escolhida!");
            }
        }

        public void validarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if(!tab.peca(origem).PodeMoverPara(destino))
            {
                throw new TabuleiroException("Posição de destino inválida!");
            }
        }
       

        private void mudaJogador()
        {
            if (jogadorAtual == Cores.Branca)
            {
                jogadorAtual = Cores.Preta;
            }
            else
            {
                jogadorAtual = Cores.Branca;
            }
        }

        private void colocarPecas()
        {
            tab.ColocarPeca(new Torre(tab, Cores.Branca), new PosicaoXadrez('c', 1).toPosicao());
            tab.ColocarPeca(new Torre(tab, Cores.Branca), new PosicaoXadrez('c', 2).toPosicao());
            tab.ColocarPeca(new Torre(tab, Cores.Branca), new PosicaoXadrez('d', 2).toPosicao());
            tab.ColocarPeca(new Torre(tab, Cores.Branca), new PosicaoXadrez('e', 2).toPosicao());
            tab.ColocarPeca(new Torre(tab, Cores.Branca), new PosicaoXadrez('e', 1).toPosicao());
            tab.ColocarPeca(new Rei(tab, Cores.Branca), new PosicaoXadrez('d', 1).toPosicao());
            tab.ColocarPeca(new Torre(tab, Cores.Preta), new PosicaoXadrez('c', 7).toPosicao());
            tab.ColocarPeca(new Torre(tab, Cores.Preta), new PosicaoXadrez('c', 8).toPosicao());
            tab.ColocarPeca(new Torre(tab, Cores.Preta), new PosicaoXadrez('d', 7).toPosicao());
            tab.ColocarPeca(new Torre(tab, Cores.Preta), new PosicaoXadrez('e', 7).toPosicao());
            tab.ColocarPeca(new Torre(tab, Cores.Preta), new PosicaoXadrez('e', 8).toPosicao());
            tab.ColocarPeca(new Rei(tab, Cores.Preta), new PosicaoXadrez('d', 8).toPosicao());
        }
    }
}