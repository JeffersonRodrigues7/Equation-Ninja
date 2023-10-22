using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class SetRecord : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recordText;

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
            // Lê todas as linhas do arquivo de leaderboard
            string[] rows = File.ReadAllLines(filePath);

            foreach (string row in rows)
            {
                // Divide cada linha em nome e pontuação
                string[] data = row.Split(';');
                if (data.Length == 2)
                {
                    string _name = data[0];
                    int _ponctuation = int.Parse(data[1]);

                    // Cria uma estrutura PlayerScore e a adiciona à lista
                    PlayerScore playerScore = new PlayerScore
                    {
                        name = _name,
                        ponctuation = _ponctuation
                    };

                    playerScores.Add(playerScore);
                }
            }
        }

        // Ordena a lista de jogadores pelo valor da pontuação (do maior para o menor).
        playerScores.Sort((a, b) => b.ponctuation.CompareTo(a.ponctuation));

        recordText.text = "Recorde: " + playerScores[0].ponctuation;
    }
}
