
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{

    public GameObject playerPrefab;

    public GameObject enemyOnePrefab;
    public GameObject enemyTwoPrefab;
    public GameObject cloudPrefab;
    public GameObject healthPrefab;
    public GameObject gameOverText;
    public GameObject restartText;
    public GameObject powerupPrefab;
    public GameObject audioPlayer;
    public GameObject CoinPrefab;


    public AudioClip powerUpSound;
    public AudioClip powerDownSound;


    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI powerUpText;

    public int score;
    public float horizontalScreenSize;
    public float verticalScreenSize;

    public int cloudMove;
    private bool gameOver;
    // Start is called before the first frame update
    void Start()
    {
        horizontalScreenSize = 10f;
        verticalScreenSize = 7f;
        score = 0;
        cloudMove = 1;
        gameOver = false;
        AddScore(0);
        Instantiate(playerPrefab, transform.position, Quaternion.identity);

        CreateSky();

        StartCoroutine(SpawnPowerup());
        StartCoroutine(SpawnHealth());
        StartCoroutine(SpawnCoin());
        InvokeRepeating("CreateEnemyOne", 1, 2); //repetition right at the start and very exact 
        InvokeRepeating("CreateEnemyTwo", 3.5f, 3.7f);
        //??? for loops are set amounts 
        //coroutines are for random repeition or timed events - think of it like a count down 
        powerUpText.text = "No Powers yet!";



    }

    IEnumerator SpawnPowerup()
    {
        float spawnTime = Random.Range(3, 5);
        yield return new WaitForSeconds(spawnTime);
        CreatePowerup();
        StartCoroutine(SpawnPowerup());
    }

    IEnumerator SpawnHealth()
    {
        float spawnTime = Random.Range(3, 7);
        yield return new WaitForSeconds(spawnTime);
        CreateHealth();
        StartCoroutine(SpawnHealth());
    }
     IEnumerator SpawnCoin()
    {
        float spawnTime = Random.Range(3, 3);
        yield return new WaitForSeconds(spawnTime);
        CreateCoin();
        StartCoroutine(SpawnCoin());
    }

    void CreatePowerup()
    {
        Instantiate(powerupPrefab, new Vector3(Random.Range(-horizontalScreenSize * 0.8f, horizontalScreenSize * 0.8f), Random.Range(-3.6f, 0.3f), 0), Quaternion.identity);
    }
    void CreateHealth()
    {
        Instantiate(healthPrefab, new Vector3(Random.Range(-horizontalScreenSize * 0.8f, horizontalScreenSize * 0.8f), Random.Range(-3.6f, 0.3f), 0), Quaternion.identity);
    }
    void CreateCoin()
    {
        Instantiate(CoinPrefab, new Vector3(Random.Range(-horizontalScreenSize * 0.8f, horizontalScreenSize * 0.8f), Random.Range(-3.6f, 0.3f), 0), Quaternion.identity);
    }
    public void ManagePowerupText(int powerupType)
    {
        switch (powerupType)
        {
            case 1:
                powerUpText.text = "Speed Boost!";
                break;
            case 2:
                powerUpText.text = "Double Shot!";
                break;
            case 3:
                powerUpText.text = "Triple Shot!";
                break;
            case 4:
                powerUpText.text = "Shield Activated!";
                break;
            default:
                powerUpText.text = "No Powerups yet!";
                break;
        }
    }
    public void PlaySound(int soundType)
    {

        switch (soundType)
        {
            
            case 1:
                audioPlayer.GetComponent<AudioSource>().PlayOneShot(powerUpSound);
                break;
            case 2:
                audioPlayer.GetComponent<AudioSource>().PlayOneShot(powerDownSound);
                break;

        }
    }

    void CreateSky()
    {
        for (int i = 0; i < 30; i++)
        {
            Instantiate(cloudPrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize), Random.Range(-verticalScreenSize, verticalScreenSize), 0), Quaternion.identity);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (gameOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    void CreateEnemyOne()
    {
        Instantiate(enemyOnePrefab, new Vector3(Random.Range(-9f, 9f), 6.5f, 0), Quaternion.identity);
    }
    void CreateEnemyTwo()
    {
        Instantiate(enemyTwoPrefab, new Vector3(-11.5f, Random.Range(-3.6f, 0.3f), 0), Quaternion.identity);
        Debug.Log("Enemy Two Created at " + enemyTwoPrefab.transform.position.y);
    }
    public void AddScore(int earnedScore)
    {
        score += earnedScore;
        scoreText.text = "Score: " + score;
    }
    public void ChangeLivesText(int currentLives)
    {
        livesText.text = "Lives: " + currentLives;
    }
    public void GameOver()
    {
        gameOverText.SetActive(true);
        restartText.SetActive(true);
        gameOver = true;
        CancelInvoke();
        cloudMove = 0;
    }
}