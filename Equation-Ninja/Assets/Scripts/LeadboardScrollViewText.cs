using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class LeadboardScrollViewText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI leadboardText;

    [Serializable]
    public class PlayerScore
    {
        public string name;
        public int ponctuation; 
    }

    private List<PlayerScore> playerScores = new List<PlayerScore>();

    private void Start()
    {
        // Define o caminho do arquivo de leaderboard
        string filePath = Path.Combine(Application.dataPath, "Data/leaderboard.txt");

        if (File.Exists(filePath))
        {
            // L� todas as linhas do arquivo de leaderboard
            string[] rows = File.ReadAllLines(filePath);

            foreach (string row in rows)
            {
                // Divide cada linha em nome e pontua��o
                string[] data = row.Split(';');
                if (data.Length == 2)
                {
                    string _name = data[0];
                    int _ponctuation = int.Parse(data[1]);

                    // Cria uma estrutura PlayerScore e a adiciona � lista
                    PlayerScore playerScore = new PlayerScore
                    {
                        name = _name,
                        ponctuation = _ponctuation
                    };

                    playerScores.Add(playerScore);
                }
            }
        }

        // Ordena a lista de jogadores pelo valor da pontua��o (do maior para o menor).
        playerScores.Sort((a, b) => b.ponctuation.CompareTo(a.ponctuation));

        // Exibe os dados na tela
        showLeaderboard();
    }

    // Exibe os dados na tela
    private void showLeaderboard()
    {
        for (int i = 0; i < playerScores.Count; i++)
        {
            // Cria uma entrada formatada com o �ndice, a pontua��o e o nome
            string input = $"{i + 1}) {playerScores[i].ponctuation} - {playerScores[i].name}\n";

            // Adiciona a entrada ao TextMeshProUGUI
            leadboardText.text += input;
        }
    }
}