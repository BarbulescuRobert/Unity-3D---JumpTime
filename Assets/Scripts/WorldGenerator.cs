using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    private float FinishLineHeight;

    private float[] inaltimePlatforma = { 0.1f, 0.6f };

    public GameObject[] Baza = new GameObject[1];  
    public GameObject[] Model3d = new GameObject[2];  
    public GameObject[] ColliderModel = new GameObject[2];  
    public GameObject[] Player = new GameObject[2];
    public GameObject bird;
    public GameObject FinishLine;


    public GameObject lightPrefab;
    public GameObject[] a;
    public GameObject[] b;
    public GameObject[] c;
   // public GameObject[] d;
    public int numaraPlatforme = 0;
    public int UltimaPlatforma = -5;
    private int BazaNr = 0;
    private int Model3dNr = 0;
    private int ColliderModelNr = 0;
    private int PlayerNr = 0;
    public int numaraBird = 0;

    void Start()
    {
        a = new GameObject[5];
        b = new GameObject[5];
        c = new GameObject[5];
        SpawnPlatform();
       // d = new GameObject[5];
        //SpawnBird();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player[PlayerNr].transform.position.y > FinishLineHeight)
        {
            FindObjectOfType<UI_ManagerScript>().LevelWon();
            Player[PlayerNr].gameObject.SetActive(false);
            GetComponent<WorldGenerator>().enabled = false;
            FinishLine.SetActive(false);
        }
        else if (Player[PlayerNr].transform.position.y > b[numaraPlatforme - 1].transform.position.y && FinishLineHeight - 4f > Player[PlayerNr].transform.position.y)
        { SpawnPlatform(); //SpawnBird();
                           }

        if (UltimaPlatforma >= 0)
        {
            if (Player[PlayerNr].transform.position.y < (b[UltimaPlatforma].transform.position.y - 2f))
            {
                FindObjectOfType<UI_ManagerScript>().PlayerDied();
                Player[PlayerNr].gameObject.SetActive(false);
                GetComponent<WorldGenerator>().enabled = false;
                FinishLine.SetActive(false);
            }
        }
        else if (Player[PlayerNr].transform.position.y < -10)
        {
            FindObjectOfType<UI_ManagerScript>().PlayerDied();
            Player[PlayerNr].gameObject.SetActive(false);
            GetComponent<WorldGenerator>().enabled = false;
            FinishLine.SetActive(false);
        }
    }

  
    private void SpawnPlatform()
    {
        if (numaraPlatforme == 5)
        {
            if (numaraPlatforme + UltimaPlatforma == 5)
                Baza[BazaNr].SetActive(false);
            numaraPlatforme = 0;
        }
        Destroy(a[numaraPlatforme]);
        Destroy(b[numaraPlatforme]);
        Destroy(c[numaraPlatforme]);
        a[numaraPlatforme] = Instantiate(Model3d[Model3dNr]) as GameObject;
        b[numaraPlatforme] = Instantiate(ColliderModel[ColliderModelNr]) as GameObject;
        c[numaraPlatforme] = Instantiate(lightPrefab) as GameObject;
        a[numaraPlatforme].transform.position = new Vector2(Random.Range(-6.5f, 6.5f), Random.Range(Player[PlayerNr].transform.position.y + 2.0f, Player[PlayerNr].transform.position.y + 4.0f));
        b[numaraPlatforme].transform.position = new Vector2(a[numaraPlatforme].transform.position.x, a[numaraPlatforme].transform.position.y + inaltimePlatforma[Model3dNr]);
        c[numaraPlatforme].transform.position = new Vector3(a[numaraPlatforme].transform.position.x, a[numaraPlatforme].transform.position.y + 1.8f, -0.5f);
        c[numaraPlatforme].GetComponent<Light>().color = Color.red;
        if (Random.Range(0.0f, 1.0f) > 0.7f)
        {
            b[numaraPlatforme].tag = "Boost";
            c[numaraPlatforme].GetComponent<Light>().color = Color.green;
        }
        numaraPlatforme++;
        UltimaPlatforma++;
        if (UltimaPlatforma == 5)
            UltimaPlatforma = 0;
    }

    /*private void SpawnBird()
    {
        if (numaraBird == 5)
            numaraBird = 0;
        Destroy(d[numaraBird]);
        d[numaraBird] = Instantiate(bird) as GameObject;
        d[numaraBird].transform.position = new Vector2(Random.Range(-6.5f, 6.5f), Random.Range(Player[PlayerNr].transform.position.y + 2.0f, Player[PlayerNr].transform.position.y + 4.0f));
        numaraBird++;
    }
  */
    public void ContinueGame()
    {
        float Height = FindObjectOfType<ScoreScript>().MaxHeight;
        if (Height < 0 || (Height >= 0 && Height <= 5))
            Player[PlayerNr].transform.position = new Vector2(Baza[BazaNr].transform.position.x, Baza[BazaNr].transform.position.y + 1);
        else
        {
            bool gasit = false;
            int i = 0;
            while (i <= 3)
            {
                if (Height >= a[i].transform.position.y && Height <= a[i + 1].transform.position.y)
                {
                    gasit = true;
                    break;
                }
                else
                    i++;
            }
            if (gasit == true)
                Player[PlayerNr].transform.position = new Vector2(a[i].transform.position.x, a[i].transform.position.y + 1);
            else
                Player[PlayerNr].transform.position = new Vector2(a[4].transform.position.x, a[4].transform.position.y + 1);
        }
    }

    public void StartNewGame()
    {
        FinishLine.SetActive(true);
        SpawnFinishLine();
        //SpawnBird();
        for (int i = 0; i < 5; i++)
        {
            Destroy(a[i]);
            Destroy(b[i]);
            Destroy(c[i]);
        }
        numaraPlatforme = 0;
        UltimaPlatforma = -5;
        Baza[BazaNr].SetActive(true);
        SpawnPlatform();
    }

    private void SpawnFinishLine()
    {
        FinishLine.transform.position = new Vector2(0.2f, FinishLineHeight);
    }

    public void ChangePlayer(int val)
    {
        PlayerNr = val;
    }

    public void ChangeModel3d(int val)
    {
        Model3dNr = val;
        ColliderModelNr = val;
    }
    public void ChangeBaza(int val)
    {
        BazaNr = val;
    }

    public void ChangeFinishLine(float val)
    {
        FinishLineHeight = val;
    }
}
