Feature: Scrutin

Scenario: Scrutin, un candidat obtient la majorité absolue des votes
	Given les candidats au premier tour sont
		| Nom | Exp |
		| A   | 8   |
		| B   | 3   |
		| C   | 5   |
	And un électeur vote A
	And un électeur vote A
	And un électeur vote A
	And un électeur vote B
	When le scrutin est clôturé
	Then le vainqueur du scrutin est A
	And les résultats sont
		| Nom | Votes | %  |
		| A   | 3     | 75 |
		| B   | 1     | 25 |
		| C   | 0     | 0  |

Scenario: Aucun candidat n'obtient la majorité au premier scrutin
	Given les candidats au premier tour sont
		| Nom | Exp |
		| A   | 8   |
		| B   | 3   |
		| C   | 5   |
	And un électeur vote A
	And un électeur vote A
	And un électeur vote B
	And un électeur vote B
	And un électeur vote C
	When le scrutin est clôturé
	Then les candidats au deuxième tour sont
		| Nom |
		| A   |
		| B   |
	Given un électeur vote A
	And un électeur vote A
	And un électeur vote A
	And un électeur vote B
	And un électeur vote B
	When le scrutin est clôturé
	Then le vainqueur du scrutin est A
	And les résultats sont
		| Nom | Votes | %  |
		| A   | 3     | 60 |
		| B   | 2     | 40 |

Scenario: Les deux candidats sont a égalité au deuxième tour
	Given les candidats au premier tour sont
		| Nom | Exp |
		| A   | 8   |
		| B   | 3   |
		| C   | 5   |
	And un électeur vote A
	And un électeur vote A
	And un électeur vote B
	And un électeur vote B
	And un électeur vote C
	When le scrutin est clôturé
	Then les candidats au deuxième tour sont
		| Nom |
		| A   |
		| B   |
	Given un électeur vote A
	And un électeur vote A
	And un électeur vote B
	And un électeur vote B
	When le scrutin est clôturé
	Then le vainqueur du scrutin est indéterminé
	And les résultats sont
		| Nom | Votes | %  |
		| A   | 2     | 50 |
		| B   | 2     | 50 |

Scenario: Les candidats qui finissent 2ème et 3ème à l'issue du premier tour sont à égalité
	Given les candidats au premier tour sont
		| Nom | Exp |
		| A   | 8   |
		| B   | 3   |
		| C   | 5   |
	And un électeur vote A
	And un électeur vote A
	And un électeur vote A
	And un électeur vote B
	And un électeur vote B
	And un électeur vote C
	And un électeur vote C
	When le scrutin est clôturé
	Then les candidats au deuxième tour sont
		| Nom |
		| A   |
		| C   |
	Given un électeur vote A
	And un électeur vote A
	And un électeur vote A
	And un électeur vote C
	When le scrutin est clôturé
	Then le vainqueur du scrutin est A
	And les résultats sont
		| Nom | Votes | %  |
		| A   | 3     | 75 |
		| C   | 1     | 25 |

Scenario: Un électeur vote blanc au premier tour
	Given les candidats au premier tour sont
		| Nom | Exp |
		| A   | 8   |
		| B   | 3   |
		| C   | 5   |
	And un électeur vote A
	And un électeur vote A
	And un électeur vote A
	And un électeur vote Blanc
	And un électeur vote B
	When le scrutin est clôturé
	Then le vainqueur du scrutin est A
	And les résultats sont
		| Nom   | Votes | %  |
		| A     | 3     | 75 |
		| B     | 1     | 25 |
		| C     | 0     | 0  |
		| Blanc | 1     | 20 |