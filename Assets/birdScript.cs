using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class birdScript : MonoBehaviour
{

    private void Update()
    {
        if(transform.position.x < -10f)
        {
            transform.localRotation = Quaternion.Euler(0, 90, 0);
        }
        if(transform.position.x > 10f)
        {
            transform.localRotation = Quaternion.Euler(0, -90, 0);
        }

        if(transform.localRotation == Quaternion.Euler(0, -90, 0))
            transform.position = new Vector2(transform.position.x - Time.deltaTime, 0);
        else
            transform.position = new Vector2(transform.position.x + Time.deltaTime, 0);
    }
}
