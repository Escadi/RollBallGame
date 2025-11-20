using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float movementX;
    private float movementY;
    public float speed = 10;
    public float jumpForce = 7;
    public bool isGrounded;
    private int point;
    public TextMeshProUGUI pointText;
    public GameObject winText;
    private AudioSource audioSource;
    public AudioClip pickSound;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        point = 0;
        SetCountText();
        winText.SetActive(false);
        audioSource = GetComponent<AudioSource>();

    }
    

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }
     

    void OnJump(InputValue value)
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void FixedUpdate()
    {
    
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            point++;
            
            if (audioSource != null && pickSound != null){

                audioSource.PlayOneShot(pickSound);
            }
            SetCountText();
        }
    }

    void SetCountText()
    {
        pointText.text = "Puntos: " + point.ToString();
        if (point >= 10)
        {
            winText.SetActive(true);
            StartCoroutine(LoadNextSceneAfterDelay(3));
            Time.timeScale = 0f;
        }
    }


    private IEnumerator LoadNextSceneAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
  
    public void ResetPoints()
    {
        point = 0;
        SetCountText();
        winText.SetActive(false);

    }
}
