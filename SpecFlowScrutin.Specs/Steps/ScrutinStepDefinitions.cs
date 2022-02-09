using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Xunit;

namespace SpecFlowScrutin.Specs.Steps
{
    [Binding]
    public sealed class ScrutinStepDefinitions
    {

        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext _scenarioContext;
        private readonly Scrutin _scrutin = new Scrutin();

        public ScrutinStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"les candidats au premier tour sont")]
        public void GivenLesCandidatsAuPremierTourSont(Table candidats)
        {
            _scrutin.setCandidats(candidats);
            _scrutin.ouvrir();
        }

        [Given(@"un électeur vote (.*)")]
        public void GivenUnElecteurVotePour(string nomCandidat)
        {
            _scrutin.ajouterVote(nomCandidat);
        }

        [When(@"le scrutin est clôturé")]
        public void WhenLeScrutinEstCloture()
        {
            _scrutin.cloturer();
            _scrutin.calculResultats();
        }

        [Then(@"le vainqueur du scrutin est (.*)")]
        public void ThenLeVainqueurDuScrutinEst(string nomCandidat)
        {
            _scrutin.getVainqueur().Should().Be(nomCandidat);
        }

        [Then(@"les résultats sont")]
        public void ThenLesResultatsSont(Table resultats)
        {

            List<Candidat> expectedlistResulats = new List<Candidat>();
            List<Candidat> listResultats = _scrutin.getResultats();

            for (int i = 0; i < resultats.Rows.Count; i++)
            {
                Candidat candidat = new Candidat(resultats.Rows[i]["Nom"], Convert.ToDouble(resultats.Rows[i]["Votes"]), Convert.ToDouble(resultats.Rows[i]["%"]));
                expectedlistResulats.Add(candidat);
            }

            for (int i = 0; i < expectedlistResulats.Count; i++)
            {
                Assert.Equal(expectedlistResulats.ElementAt(i).Nom, listResultats.ElementAt(i).Nom);
                Assert.Equal(expectedlistResulats.ElementAt(i).Votes, listResultats.ElementAt(i).Votes);
                Assert.Equal(expectedlistResulats.ElementAt(i).Pourcentage, listResultats.ElementAt(i).Pourcentage);
            }

            /*_scrutin.getResultats().Should().Be(resultats);*/

        }

        [Then(@"les candidats au deuxième tour sont")]
        public void ThenLesCandidatsAuDeuxiemeTourSont(Table candidatsTour2)
        {
            List<Candidat> expectedlistCandidatsTour2 = new List<Candidat>();
            List<Candidat> listCandidatsTour2 = _scrutin.getCandidats();

            for (int i = 0; i < candidatsTour2.Rows.Count; i++)
            {
                Candidat candidat = new Candidat(candidatsTour2.Rows[i]["Nom"], 0);
                expectedlistCandidatsTour2.Add(candidat);
            }

            for (int i = 0; i < expectedlistCandidatsTour2.Count; i++)
            {
                Assert.Equal(expectedlistCandidatsTour2.ElementAt(i).Nom, listCandidatsTour2.ElementAt(i).Nom);
            }

            /*_scrutin.getCandidats().Should().Be(candidatsTour2);*/

        }

    }
}
