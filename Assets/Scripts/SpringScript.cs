using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringScript : MonoBehaviour
{
    private int nr;
    private int Timedelay = 50;

    // Start is called before the first frame update
    void Start()
    {
        nr = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(nr == 1)
        {
            if (gameObject.transform.localScale.y <= 0.33f)
            {
                if (Timedelay > 0)
                        Timedelay--;
                else
                {
                    FindObjectOfType<player>().BoostSpring();
                    nr = 2;
                }
            }
            else
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x, gameObject.transform.localScale.y - Time.deltaTime);
                gameObject.GetComponent<BoxCollider2D>().size = new Vector2(gameObject.GetComponent<BoxCollider2D>().size.x, gameObject.GetComponent<BoxCollider2D>().size.y - Time.deltaTime/10000);
            }
        } 
        if(nr == 2)
        {
            if (gameObject.transform.localScale.y >= 0.9f)
            {
                nr = 0;
                Timedelay = 50;
            }
            else
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x, gameObject.transform.localScale.y + Time.deltaTime + 0.1f);
                gameObject.GetComponent<BoxCollider2D>().size = new Vector2(gameObject.GetComponent<BoxCollider2D>().size.x, gameObject.GetComponent<BoxCollider2D>().size.y + Time.deltaTime + 0.1f);
            }
        }
        
    }

    public void StartSpringNow()
    {
        nr = 1;
    }
}
