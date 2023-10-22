using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

public class CustomInputField : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;//Input onde o jogador irá digitar seu name
    [SerializeField] private TextMeshProUGUI finalPonctuation;//Input onde o jogador irá digitar seu name

    private string filePath = "leaderboard.txt";

    private void Start()
    {
        if (inputField != null)
        {
            // Adicione um ouvinte ao evento "OnEndEdit" do InputField.
            inputField.onEndEdit.AddListener(OnInputFieldEndEdit);
        }
    }

    private void OnInputFieldEndEdit(string text)
    {
        // Verifica se o texto tem pelo menos 3 caracteres e se a tecla Enter foi pressionada.
        if (text.Length >= 3 && Input.GetKey(KeyCode.Return))
        {
            filePath = Path.Combine(Application.dataPath, "Data/leaderboard.txt");

            // Abrindo o arquivo em modo de adição e adicionadno novo jogador com pontuação
            using (StreamWriter writer = File.AppendText(filePath))
            {
                writer.WriteLine(inputField.text + ";" + finalPonctuation.text);
            }

            SceneManager.LoadScene("GameOver");
        }
        else
        {
            Debug.Log($"<color=red>Digite pelo menos 3 caracteres</color>");
        }
    }
}