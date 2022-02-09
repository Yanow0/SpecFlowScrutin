
namespace SpecFlowScrutin
{
    public class Candidat
    {
        private string nom;
        private double votes;
        private int anneesExp;
        private double pourcentage;      
        

        public Candidat(string nom, int anneesExp)
        {
            this.nom = nom;
            this.anneesExp = anneesExp;
            this.votes = 0;
            this.pourcentage = 0;
        }

        public Candidat(string nom, double votes, double pourcentage)
        {
            this.nom = nom;
            this.votes = votes;
            this.pourcentage = pourcentage;
            this.anneesExp = 0;
        }

        public string Nom { get => nom; set => nom = value; }
        public double Votes { get => votes; set => votes = value; }
        public int AnneesExp { get => anneesExp; set => anneesExp = value; }
        public double Pourcentage { get => pourcentage; set => pourcentage = value; }

        public void reset()
        {
            this.votes = 0;
            this.pourcentage = 0;
        }
    }
}
