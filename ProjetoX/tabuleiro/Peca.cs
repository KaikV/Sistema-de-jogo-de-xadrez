namespace tabuleiro
{
     class Peca 
    {
        public Posicao posicao { get; set; }
        public Cores cor { get; protected set; }
        public int qteMovimentos { get; protected set; }
        public Tabuleiro tab { get; set; }

        public Peca (Posicao posicao,Tabuleiro tab, Cores cor  )
        {
            this.posicao = posicao;
            this.tab = tab;
            this.cor = cor;
            this.qteMovimentos = 0;

        }


    }



}
