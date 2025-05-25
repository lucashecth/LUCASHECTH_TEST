using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Load the game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("Island");
    }
}
