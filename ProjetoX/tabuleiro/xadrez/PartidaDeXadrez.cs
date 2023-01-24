using System;
using System.Runtime.ConstrainedExecution;
using tabuleiro;
using System.Collections.Generic;
using System.Runtime.InteropServices;

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
        public bool Xeque { get; private set; }
        public Peca VuneravelEnPassant { get; protected set; }
        public PartidaDeXadrez()
        {
            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cores.Branca;
            terminada = false;
            Xeque = false;
            VuneravelEnPassant = null;
            pecas = new HashSet<Peca>();
            capturadas = new HashSet<Peca>();
            colocarPecas();
        }
        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = tab.RetirarPeca(origem);
            p.ImplementarQteMovimentos();
            Peca pecaCapturada = tab.RetirarPeca(destino);
            tab.ColocarPeca(p, destino);
            if (pecaCapturada != null)
            {
                capturadas.Add(pecaCapturada);
            }
            //#Roque-Pequeno
            if (p is Rei && destino.coluna == origem.coluna + 2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna + 1);
                Peca T = tab.RetirarPeca(origemT);
                T.ImplementarQteMovimentos();
                tab.ColocarPeca(T, destinoT);
            }
            //#Roque-Grande
            if (p is Rei && destino.coluna == origem.coluna - 2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna - 4);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna - 1);
                Peca T = tab.RetirarPeca(origemT);
                T.ImplementarQteMovimentos();
                tab.ColocarPeca(T, destinoT);
            }
            //#Jogadaespecial En-passant
            if(p is Peao)
            {
                if(origem.coluna != destino.coluna && pecaCapturada == null)
                {
                    Posicao posP;
                    if (p.cor == Cores.Branca)
                    {
                        posP = new Posicao(destino.linha + 1, destino.coluna);
                    }
                    else
                    {
                        posP = new Posicao(destino.linha -1, destino.coluna);
                    }
                    pecaCapturada = tab.RetirarPeca(posP);
                    capturadas.Add(pecaCapturada);
                }
            }

            return pecaCapturada;
        }
        public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = tab.RetirarPeca(destino);
            p.RemoverQteMovimento();
            if (pecaCapturada != null)
            {
                tab.ColocarPeca(pecaCapturada, destino);
                capturadas.Remove(pecaCapturada);
            }
            tab.ColocarPeca(p, origem);

            //#jogadaespecial Roque-Pequeno
            if (p is Rei && destino.coluna == origem.coluna + 2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna + 3);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna + 1);
                Peca T = tab.RetirarPeca(destinoT);
                T.RemoverQteMovimento();
                tab.ColocarPeca(T, origemT);
            }
            //#jogadaespecial Roque-Grande
            if (p is Rei && destino.coluna == origem.coluna - 2)
            {
                Posicao origemT = new Posicao(origem.linha, origem.coluna - 4);
                Posicao destinoT = new Posicao(origem.linha, origem.coluna - 1);
                Peca T = tab.RetirarPeca(destinoT);
                T.RemoverQteMovimento();
                tab.ColocarPeca(T, origemT);
            }
            //#jogadaespecial En-passant

            if(p is Peao)
            {
                if (origem.coluna != destino.coluna && pecaCapturada == VuneravelEnPassant)
                {
                    Peca peao = tab.RetirarPeca(destino);
                    Posicao posP;
                    if(p.cor == Cores.Branca)
                    {
                        posP = new Posicao(3, destino.coluna);

                    }
                    else
                    {
                        posP = new Posicao(4, destino.coluna);
                    }
                    tab.ColocarPeca(peao, posP);
                }
            }
            
        }
        public void realizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutaMovimento(origem, destino);
            if (EstaEmXeque(jogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                {
                    throw new TabuleiroException("Você nao pode se colocar em xeque!");
                }

            }
            if (EstaEmXeque(Adversaria(jogadorAtual)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }
            if (TesteXequeMate(Adversaria(jogadorAtual)))
            {
                terminada = true;
            }
            else
            {
                turno++;
                mudaJogador();
            }
            Peca p = tab.peca(destino);
            //#Jogadaespecial En-Passant
            if (p is Peao && (destino.linha == origem.linha - 2 || destino.linha ==origem.linha + 2))
            {
                VuneravelEnPassant = p;
            }
            else
            {
                VuneravelEnPassant = null;
            }
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
            if (!tab.peca(origem).MovimentoPossivel(destino))
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
            foreach (Peca x in capturadas)
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
        private Cores Adversaria(Cores cor)
        {
            if (cor == Cores.Branca)
            {
                return Cores.Preta;
            }
            else
            {
                return Cores.Branca;
            }
        }
        private Peca rei(Cores cor)
        {
            foreach (Peca x in PecasEmJogo(cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }
            return null;
        }

        public bool EstaEmXeque(Cores cor)
        {
            Peca R = rei(cor);
            if (R == null)
            {
                throw new TabuleiroException("Não tem rei da cor " + cor + " no tabuleiro!");

            }
            foreach (Peca x in PecasEmJogo(Adversaria(cor)))
            {
                bool[,] mat = x.MovimentosPossiveis();
                if (mat[R.posicao.linha, R.posicao.coluna])
                {
                    return true;
                }
            }
            return false;
        }

        public bool TesteXequeMate(Cores cor)
        {
            if (!EstaEmXeque(cor))
            {
                return false;
            }
            foreach (Peca x in PecasEmJogo(cor))
            {
                bool[,] mat = x.MovimentosPossiveis();
                for (int i = 0; i < tab.linhas; i++)
                {
                    for (int j = 0; j < tab.colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = x.posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = ExecutaMovimento(origem, destino);
                            bool testeXeque = EstaEmXeque(cor);
                            DesfazMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;

        }

        public void colocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }
        private void colocarPecas()
        {



            colocarNovaPeca('a', 1, new Torre(tab, Cores.Branca));
            colocarNovaPeca('b', 1, new Cavalo(tab, Cores.Branca));
            colocarNovaPeca('c', 1, new Bispo(tab, Cores.Branca));
            colocarNovaPeca('d', 1, new Dama(tab, Cores.Branca));
            colocarNovaPeca('e', 1, new Rei(tab, Cores.Branca, this));
            colocarNovaPeca('f', 1, new Bispo(tab, Cores.Branca));
            colocarNovaPeca('g', 1, new Cavalo(tab, Cores.Branca));
            colocarNovaPeca('h', 1, new Torre(tab, Cores.Branca));
            colocarNovaPeca('a', 2, new Peao(tab, Cores.Branca, this));
            colocarNovaPeca('b', 2, new Peao(tab, Cores.Branca, this));
            colocarNovaPeca('c', 2, new Peao(tab, Cores.Branca, this));
            colocarNovaPeca('d', 2, new Peao(tab, Cores.Branca, this));
            colocarNovaPeca('e', 2, new Peao(tab, Cores.Branca, this));
            colocarNovaPeca('f', 2, new Peao(tab, Cores.Branca, this));
            colocarNovaPeca('g', 2, new Peao(tab, Cores.Branca, this));
            colocarNovaPeca('h', 2, new Peao(tab, Cores.Branca, this));

            colocarNovaPeca('a', 8, new Torre(tab, Cores.Preta));
            colocarNovaPeca('b', 8, new Cavalo(tab, Cores.Preta));
            colocarNovaPeca('c', 8, new Bispo(tab, Cores.Preta));
            colocarNovaPeca('d', 8, new Dama(tab, Cores.Preta));
            colocarNovaPeca('e', 8, new Rei(tab, Cores.Preta, this));
            colocarNovaPeca('f', 8, new Bispo(tab, Cores.Preta));
            colocarNovaPeca('g', 8, new Cavalo(tab, Cores.Preta));
            colocarNovaPeca('h', 8, new Torre(tab, Cores.Preta));
            colocarNovaPeca('a', 7, new Peao(tab, Cores.Preta, this));
            colocarNovaPeca('b', 7, new Peao(tab, Cores.Preta, this));
            colocarNovaPeca('c', 7, new Peao(tab, Cores.Preta, this));
            colocarNovaPeca('d', 7, new Peao(tab, Cores.Preta, this));
            colocarNovaPeca('e', 7, new Peao(tab, Cores.Preta, this));
            colocarNovaPeca('f', 7, new Peao(tab, Cores.Preta, this));
            colocarNovaPeca('g', 7, new Peao(tab, Cores.Preta, this));
            colocarNovaPeca('h', 7, new Peao(tab, Cores.Preta, this));
        }
    }
}