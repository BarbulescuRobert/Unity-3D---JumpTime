using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    public GameObject SoccerBall, GolfBall;
   
    void Update()
    {
        if(FindObjectOfType<UI_ManagerScript>().CheckGolfBall() == true)
            transform.position = new Vector3(GolfBall.transform.position.x, GolfBall.transform.position.y + 1, -1.1f);
        else
            transform.position = new Vector3(SoccerBall.transform.position.x, SoccerBall.transform.position.y + 1, -1.1f);
    }
}
