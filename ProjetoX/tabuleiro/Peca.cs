namespace tabuleiro
{
   abstract class Peca
    {
        public Posicao posicao { get; set; }
        public Cores cor { get; protected set; }
        public int qteMovimentos { get; protected set; }
        public Tabuleiro tab { get; protected set; }

        public Peca(Tabuleiro tab, Cores cor)
        {
            this.posicao = null;
            this.tab = tab;
            this.cor = cor;
            this.qteMovimentos = 0;

        }
        public void ImplementarQteMovimentos ()
        {
            qteMovimentos++;
        }
        public void RemoverQteMovimento ()
        {
            qteMovimentos--;
        }
        public bool ExisteMovimentosPossiveis()
        {
            bool[,] mat = MovimentosPossiveis();
            for (int i=0; i<tab.linhas; i++)
            {
                for (int j=0; j<tab.linhas; j++)
                {
                    if (mat[i,j])
                    {
                        return true;
                    }

                }
            }
            return false;
        }

        public bool PodeMoverPara(Posicao pos)
        {
            return MovimentosPossiveis()[pos.linha, pos.coluna];
        }

        public abstract bool[,] MovimentosPossiveis();

    }



}
