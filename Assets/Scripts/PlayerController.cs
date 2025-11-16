
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float playerSpeed;
    private int weaponType;

    private float horizontalInput;

    private float verticalInput;

    private float horizontalScreenLimit = 11.4f;

    private float verticalScreenLimit = 3.8f;

    public GameObject bulletPrefab;

    public int lives;

    public GameObject explosionPrefab;
    public GameObject thrusterPrefab;
    public GameObject shieldPrefab;
    private GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        lives = 3;
        playerSpeed = 6f;
        weaponType = 1; //default weapon
        gameManager.ChangeLivesText(lives);
    }

    public void LoseALife()
    {
        //if you have a shield active - lose the shield first, no life decrease 

        lives--;

        gameManager.ChangeLivesText(lives);
        if (lives == 0)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);


            gameManager.GameOver();
        }
    }

    IEnumerator SpeedPowerDown()
    {
        yield return new WaitForSeconds(3f);
        playerSpeed = 6f;
        gameManager.ManagePowerupText(0);
        thrusterPrefab.SetActive(false);
        gameManager.PlaySound(2);
    }
    IEnumerator WeaponPowerDown()
    {
        yield return new WaitForSeconds(3f);
        weaponType = 1;
        gameManager.ManagePowerupText(0);
        gameManager.PlaySound(2);
    }
    private void OnTriggerEnter2D(Collider2D whatDidIHit)
    {
        if (whatDidIHit.tag == "Powerup")
        {
            Destroy(whatDidIHit.gameObject);
            int whichPowerup = Random.Range(1, 4);
            gameManager.PlaySound(1);
            switch (whichPowerup)
            {
                case 1:
                    //speed boost
                    playerSpeed = 10f;
                    thrusterPrefab.SetActive(true);
                    StartCoroutine(SpeedPowerDown());//make soon
                    gameManager.ManagePowerupText(1);
                    break;
                case 2:
                    //weapon upgrade
                    weaponType = 2;
                    StartCoroutine(WeaponPowerDown());//make soon 
                    gameManager.ManagePowerupText(2);
                    break;
                case 3:
                    weaponType = 3;
                    StartCoroutine(WeaponPowerDown());//make soon
                    gameManager.ManagePowerupText(3);
                    break;
                case 4:
                    //shield activate 
                    gameManager.ManagePowerupText(4);
                    break;
                default:
                    break;
            }
        }
        if (whatDidIHit.tag == "Health")
        {
            Destroy(whatDidIHit.gameObject);
            if (lives < 3)
            {
                lives++;
                gameManager.ChangeLivesText(lives);
            }
            else
            {
                gameManager.AddScore(1);
            }

        }
        if (whatDidIHit.tag == "Coin")
        {
            Destroy(whatDidIHit.gameObject);
            {
                gameManager.AddScore(1);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shooting();
    }

    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        //Move the player
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * playerSpeed);

        //Limit the player movement on screen horizontally
        if(transform.position.x > horizontalScreenLimit || transform.position.x < -horizontalScreenLimit)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }

        //Limit the player movement on screen vertically
        if (transform.position.y > 0.1f)
        {
            transform.position = new Vector3(transform.position.x, 0.1f, 0);
        }
        else if (transform.position.y < -verticalScreenLimit)
        {
            transform.position = new Vector3(transform.position.x, -verticalScreenLimit, 0);
        }
        

    }

    //Shooting
    void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Pew Pew" + horizontalInput);
            //spawn bullet
            Instantiate(bulletPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        }
    }


}
