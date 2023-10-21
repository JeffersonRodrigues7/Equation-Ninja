using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CustomInputField : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;//Input onde o jogador irá digitar seu nome

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
            SceneManager.LoadScene("GameOver");
        }
        else
        {
            Debug.Log($"<color=red>Digite pelo menos 3 caracteres</color>");
        }
    }
}