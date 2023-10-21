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
}
