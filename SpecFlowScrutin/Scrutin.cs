using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TechTalk.SpecFlow;

namespace SpecFlowScrutin
{
    public class Scrutin
    {
        private bool statut = false;
        private List<Candidat> listCandidats = new List<Candidat>();
        private double nbVotesExprimes = 0;
        private double nbVotesTotal = 0;
        private Candidat blanc;
        private Candidat vainqueur;

        public void setCandidats(Table candidats)
        {

            for (int i = 0; i < candidats.Rows.Count; i++)
            {
                Candidat candidat = new Candidat(candidats.Rows[i]["Nom"], Convert.ToInt32(candidats.Rows[i]["Exp"]));
                listCandidats.Add(candidat);
            }

            this.blanc = new Candidat("Blanc", 0);

        }

        public void ouvrir()
        {
            this.statut = true;
        }

        public void ajouterVote(string nomCandidat)
        {
            Candidat candidat = this.listCandidats.Find(candidat => candidat.Nom == nomCandidat);
            if (nomCandidat == "Blanc")
            {
                this.blanc.Votes++;
            }
            else if (candidat != null)
            {
                candidat.Votes++;
                this.nbVotesExprimes++;
            }
            this.nbVotesTotal++;

        }

        public void cloturer()
        {
            this.statut = false;
        }

        public void calculResultats()
        {
            this.listCandidats.ForEach(candidat =>
            {
                candidat.Pourcentage = (candidat.Votes / nbVotesExprimes) * 100;
            });

            this.blanc.Pourcentage = (this.blanc.Votes / nbVotesTotal) * 100;

            this.vainqueur = this.listCandidats.Find(candidat => candidat.Pourcentage > 50);


            if (vainqueur == null && this.listCandidats.Count > 2)
            {
                this.listCandidats.OrderByDescending(x => x.Pourcentage);
                double pctSecond = this.listCandidats.ElementAt(1).Pourcentage;
                double pctTroisieme = this.listCandidats.ElementAt(2).Pourcentage;
                if (pctSecond != pctTroisieme)
                {
                    this.listCandidats.RemoveRange(2, this.listCandidats.Count - 2);
                } else
                {
                    if (this.listCandidats.ElementAt(1).AnneesExp > this.listCandidats.ElementAt(2).AnneesExp)
                    {
                        this.listCandidats.RemoveAt(2);
                    } else
                    {
                        this.listCandidats.RemoveAt(1);
                    }
                }

                this.listCandidats.ForEach(candidat =>
                {
                    candidat.reset();
                });
                this.blanc.reset();

                this.nbVotesExprimes = 0;
                this.nbVotesTotal = 0;
    }

        }

        public string getVainqueur()
        {
            if (this.vainqueur != null)
            {
                return this.vainqueur.Nom;
            }
            else
            {
                return "indéterminé";
            }

        }

        public List<Candidat> getResultats()
        {
            /*Table resultats = new Table("Nom", "Votes", "%");
            this.listCandidats.ForEach(candidat =>
            {
                resultats.AddRow(candidat.Nom, candidat.Votes.ToString(), candidat.Pourcentage.ToString());
            });

            resultats.AddRow(this.blanc.Nom, this.blanc.Votes.ToString(), this.blanc.Pourcentage.ToString());*/

            List<Candidat> resultats = listCandidats;
            resultats.Add(this.blanc);
            return resultats;
        }

        public List<Candidat> getCandidats()
        {
            /*Table resultats = new Table("Nom");
          
            this.listCandidats.ForEach(candidat =>
            {
                resultats.AddRow(candidat.Nom);
            });*/
            return this.listCandidats;
        }

    }
}
