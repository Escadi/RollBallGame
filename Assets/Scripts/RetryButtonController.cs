using UnityEngine;
using UnityEngine.SceneManagement;

public class RetryButton : MonoBehaviour
{
    // Este método se llama al pulsar el botón
    public void Retry()
    {
        // Reactiva el tiempo por si el juego estaba congelado
        Time.timeScale = 1f;

        // Reinicia la escena actual
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void nextLevel() {

        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }

    public void sameLevel() {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    public void tryAgainGame() { 
    
        SceneManager.LoadScene(0);
    }

    public void exitGame() {

        Application.Quit();
    }

    // (Opcional) Si quieres resetear los puntos del jugador
    private void ResetPlayerPoints()
    {
        var player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            var controller = player.GetComponent<PlayerController>();
            if (controller != null)
            {
                controller.ResetPoints();
            }
        }
    }
}
