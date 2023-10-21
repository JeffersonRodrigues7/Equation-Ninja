using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneButton : MonoBehaviour
{
    // Este método é chamado quando o botão "Game" é clicado
    public void LoadGameScene()
    {
        SceneManager.LoadScene("Game");
    }

    // Este método é chamado quando o botão "Credits" é clicado
    public void LoadCreditsScene()
    {
        SceneManager.LoadScene("Credits");
    }

    // Este método é chamado para voltar ao menu inicial
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    // Este método é chamado para chamar a tela de gameOver
    public void LoadGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
