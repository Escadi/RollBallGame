using UnityEngine;

public class OutAreaController : MonoBehaviour
{
    public GameObject losePanel;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) { 
            
            collision.gameObject.SetActive(false);

            if (losePanel != null) {

                losePanel.SetActive(true);

                
            }
            Time.timeScale = 0f;


        }
    }
}
