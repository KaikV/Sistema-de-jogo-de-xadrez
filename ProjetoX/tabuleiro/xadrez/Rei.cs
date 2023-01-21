using tabuleiro;

namespace xadrez
{
    internal class Rei : Peca
    {

        public Rei(Tabuleiro tab, Cores cor ) : base( tab, cor ) { }

        public override string ToString()
        {
            return "R";
        }

    }
    
    
}
