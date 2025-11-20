using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    public GameObject losePanel;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        FindAndSetPlayer();
        losePanel.SetActive(false);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(FindPlayerAfterDelay());
    }

    private IEnumerator FindPlayerAfterDelay()
    {
        yield return new WaitForSeconds(0.1f);
        FindAndSetPlayer();
    }

    private void FindAndSetPlayer()
    {
        GameObject p = GameObject.FindWithTag("Player");
        if (p != null)
            player = p.transform;
    }


    void Update()
    {
        if (player != null)
            agent.SetDestination(player.position);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.SetActive(false);

            if (losePanel != null)
            {
                losePanel.SetActive(true);
            }
                
            Time.timeScale = 0f;
        }
    }
}



