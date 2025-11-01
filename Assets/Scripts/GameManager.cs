
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject enemyOnePrefab;
    public GameObject enemyTwoPrefab;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CreateEnemyOne", 2f, 3f);
        InvokeRepeating("CreateEnemyTwo", 3.5f, 3.7f);

    }

    // Update is called once per frame
    void CreateEnemyOne()
    {
        Instantiate(enemyOnePrefab, new Vector3(Random.Range(-8f, 8f), 6.5f, 0), Quaternion.identity);
        Debug.Log("Enemy One Created");
    }
    void CreateEnemyTwo()
    {
        Instantiate(enemyTwoPrefab, new Vector3(-6.5f, Random.Range(-3.6f, 4.75f), 0), Quaternion.identity);
        Debug.Log("Enemy Two Created at "+ enemyTwoPrefab.transform.position.y);
    }

}
