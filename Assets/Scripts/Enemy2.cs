using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime * 6f);
        if (transform.position.x > 7.5f)
        {
            Destroy(this.gameObject);
        }
    }
}
