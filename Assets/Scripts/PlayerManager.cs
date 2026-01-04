using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    bool gameOver;
    bool hasPresent;

    public GameObject present;

    public GameObject goal1;
    public GameObject goal2;
    public GameObject goal3;

    public GameManager gameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hasPresent = false;
        gameOver = false;
        present.SetActive(false);

        goal1.SetActive(false);
        goal2.SetActive(false);
        goal3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Present"))
        {
            Debug.Log("Got Present");
            hasPresent = true;
            present.SetActive(true);
            collision.gameObject.SetActive(false);

            goal1.SetActive(true);
            goal2.SetActive(true);
            goal3.SetActive(true);
        }


        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Game Over, player lost");
            gameOver = true;
            gameManager.gameOver = true;
            gameManager.playerWon = false;
        }

        if (collision.gameObject.CompareTag("Goal") && hasPresent)
        {
            Debug.Log("Game Over, player won");
            gameOver = true;
            gameManager.gameOver = true;
            gameManager.playerWon = true;
        }
    }

}
