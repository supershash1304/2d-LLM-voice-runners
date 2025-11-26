using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool gameOver;

    public void GameOver()
    {
        if (gameOver) return;
        gameOver = true;
        Invoke(nameof(Restart), 1.5f);
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

