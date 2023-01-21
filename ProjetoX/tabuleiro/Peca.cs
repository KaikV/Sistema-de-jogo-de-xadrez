namespace tabuleiro
{
    class Peca
    {
        public Posicao posicao { get; set; }
        public Cores cor { get; protected set; }
        public int qteMovimentos { get; protected set; }
        public Tabuleiro tab { get; set; }

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

    }



}
