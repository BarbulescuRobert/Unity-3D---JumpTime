using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public GameObject SoccerBall, GolfBall;
    public Text ScoreText;
    public int MaxScore = 0;
    public float MaxHeight = -10f;

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<UI_ManagerScript>().CheckGolfBall() == true)
            GolfScore();
        else
            SoccerScore();

        //Colors for text 
        if (MaxScore > 1 && MaxScore < 10)
            ScoreText.color = Color.green;
        else if(MaxScore > 10 && MaxScore < 20)
            ScoreText.color = Color.yellow;
        else if (MaxScore > 20 && MaxScore < 30)
            ScoreText.color = Color.red;
        else if (MaxScore > 30 && MaxScore < 40)
            ScoreText.color = Color.white;
        else if (MaxScore > 40 && MaxScore < 50)
            ScoreText.color = Color.cyan;
    }

    private void SoccerScore()
    {
        if (SoccerBall.transform.position.y / 2 > 1)
        {
            if (MaxScore < SoccerBall.transform.position.y / 2)
                MaxScore = (int)(SoccerBall.transform.position.y / 2);
            ScoreText.text = (MaxScore).ToString();
        }
        if (MaxHeight < SoccerBall.transform.position.y)
            MaxHeight = SoccerBall.transform.position.y;
    }
    private void GolfScore()
    {
        if (GolfBall.transform.position.y / 2 > 1)
        {
            if (MaxScore < GolfBall.transform.position.y / 2)
                MaxScore = (int)(GolfBall.transform.position.y / 2);
            ScoreText.text = (MaxScore).ToString();
        }
        if (MaxHeight < GolfBall.transform.position.y)
            MaxHeight = GolfBall.transform.position.y;
    }

    public void NewGame()
    {
        MaxHeight = -10f;
        MaxScore = 0;
        ScoreText.color = Color.white;
    }
}
