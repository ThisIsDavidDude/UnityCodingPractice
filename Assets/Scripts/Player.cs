using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //movement
    //shooting
    //scope access modifier private or public

    [SerializeField] private float playerSpeed;

    [HideInInspector]  private float horizontalInput;

    private float verticalInput;


    private float horizontalScreenLimit = 9.5f;

    private float verticalScreenLimit = 3;

    public GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        playerSpeed = 6f;

    }

    // Update is called once per frame
    void Update()
    {
        //Movement
        //Shooting
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
