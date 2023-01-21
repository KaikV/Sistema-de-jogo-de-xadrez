using tabuleiro;

namespace xadrez
{
    internal class Torre : Peca
    {

        public Torre(Tabuleiro tab, Cores cor) : base(tab, cor) { }

        public override string ToString()
        {
            return "T";
        }

    }


}
