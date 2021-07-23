using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject soccerBall,golfBall;
    public Material m1;
    public Material m2;
    public Material m3;

    private float Height_1 = -10f;
    private float Height_2 = 100f;
    private float Height_3 = 300f;
    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<UI_ManagerScript>().CheckGolfBall() == true)
            GolfBallFunction();
        else
            SoccerBallFunction();
    }

    private void SoccerBallFunction()
    {
        transform.position = new Vector3(0, soccerBall.transform.position.y + 1, -10);
        if (soccerBall.transform.position.y > Height_1 && soccerBall.transform.position.y < Height_2)
        {
            RenderSettings.skybox = m1;
        }
        else if (soccerBall.transform.position.y > Height_2 && soccerBall.transform.position.y < Height_3)
        {
            RenderSettings.skybox = m2;
            Height_2 = Height_1;
        }
        else if (soccerBall.transform.position.y > Height_3)
        {
            RenderSettings.skybox = m3;
            Height_3 = Height_1;
        }
        DynamicGI.UpdateEnvironment();
    }

    private void GolfBallFunction()
    {
        transform.position = new Vector3(0, golfBall.transform.position.y + 1, -10);
        if (golfBall.transform.position.y > Height_1 && golfBall.transform.position.y < Height_2)
        {
            RenderSettings.skybox = m1;
        }
        else if (golfBall.transform.position.y > Height_2 && golfBall.transform.position.y < Height_3)
        {
            RenderSettings.skybox = m2;
            Height_2 = Height_1;
        }
        else if (golfBall.transform.position.y > Height_3)
        {
            RenderSettings.skybox = m3;
            Height_3 = Height_1;
        }
        DynamicGI.UpdateEnvironment();
    }
}
