using UnityEngine;

public class GameManager : MonoBehaviour
{

    public bool gameOver;
    public bool playerWon;

    public GameObject gameOverText;
    public GameObject playerWonText;

    public GameObject enemy;

    bool broadcastedGameOver;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameOver = false;
        broadcastedGameOver = false;
        gameOverText.SetActive(false);
        playerWonText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!broadcastedGameOver)
        {
            if (gameOver)
            {
                if (playerWon)
                {
                    playerWonText.SetActive(true);
                }
                else
                {
                    gameOverText.SetActive(true);
                }
                EventBus.Publish<GameOverEvent>(new GameOverEvent(playerWon, enemy));
            }
        }
    }

    public class GameOverEvent
    {
        public bool playerWon;
        public GameObject enemy = null;

        public GameOverEvent(bool _playerWon, GameObject _enemy) { playerWon = _playerWon; enemy = _enemy; }

    }
}
