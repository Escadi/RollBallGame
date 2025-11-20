using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset = new Vector3(0, 8f, -10f);
    private bool followPlayer = true;

    void Awake()
    {
        if (Object.FindFirstObjectByType<CameraController>() != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        FindAndSetPlayer();
    }

    private void OnDestroy()
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
        {
            player = p;
        }
    }

    void LateUpdate()
    {
        if (followPlayer && player != null && player.activeSelf)
        {
            transform.position = player.transform.position + offset;
            transform.LookAt(player.transform);
        }
    }
}


