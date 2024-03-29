using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Unsafe") || collision.transform.CompareTag("Enemy"))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
        GetComponent<Jumping>().CheckGround();
    }
}
