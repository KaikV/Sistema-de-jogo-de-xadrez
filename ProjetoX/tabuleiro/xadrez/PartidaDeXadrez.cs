using System;
using System.Runtime.ConstrainedExecution;
using tabuleiro;
using System.Collections.Generic;

namespace xadrez
{
    class PartidaDeXadrez
    {

        public Tabuleiro tab { get; private set; }
        public int turno { get; private set; }
        public Cores jogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;
        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cores.Branca;
            terminada = false;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
        }
        public void ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.RetirarPeca(origem);
            p.ImplementarQteMovimentos();
            Peca pecaCapturada = tab.RetirarPeca(destino);
            tab.ColocarPeca(p, destino);
            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }
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
            if (!tab.peca(origem).PodeMoverPara(destino))
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
        public HashSet<Peca> pecasCapturas(Cores cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach(Peca x in capturadas)
            {
                if (x.cor == cor)
                {
                    aux.Add(x); 
                }
            }
            return aux;
        }
        public HashSet<Peca> PecasEmJogo(Cores cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if (x.cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCapturas(cor));
            return aux;
        }
        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }
        private void colocarPecas()
        {
            colocarNovaPeca('c', 1, new Torre(tab, Cores.Branca));
            colocarNovaPeca('c', 2, new Torre(tab, Cores.Branca));
            colocarNovaPeca('d', 2, new Torre(tab, Cores.Branca));
            colocarNovaPeca('e', 2, new Torre(tab, Cores.Branca));
            colocarNovaPeca('e', 1, new Torre(tab, Cores.Branca));
            colocarNovaPeca('d', 1, new Rei(tab, Cores.Branca));

            colocarNovaPeca('c', 7, new Torre(tab, Cores.Preta));
            colocarNovaPeca('c', 8, new Rei(tab, Cores.Preta));
            colocarNovaPeca('d', 7, new Torre(tab, Cores.Preta));
            colocarNovaPeca('e', 7, new Torre(tab, Cores.Preta));
            colocarNovaPeca('e', 8, new Torre(tab, Cores.Preta));
            colocarNovaPeca('d', 8, new Torre(tab, Cores.Preta));

        }
    }
}