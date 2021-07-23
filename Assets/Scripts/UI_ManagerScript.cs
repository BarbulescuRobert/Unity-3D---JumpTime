using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ManagerScript : MonoBehaviour
{
    public GameObject GameOverPanel, StartGamePanel, UpgradesPanel, LevelsPanel, LevelWonPanel, InventoryPanel;
    public Button StartGameButton, StartGame_UpgradesButton, GameOver_UpgradesButton, ContinueButton, BackButton, NewGameButton, NextLevelButton, LevelWon_UpgradesButton, MenuButton, LevelsButton, BackLevelsButton, InventoryButton, InventoryBackButton;
    public GameObject SoccerBall, GolfBall, Spring;
    public Button GolfBallButton, SpringButton, AmmoBoxButton;
    public Text GolfBallText, SpringText, ScoreText, UpgradesCoins, GameOverCoins, AmmoBoxText, LevelWonCoinsText, StartPanel_NewGameText;
    public Button[] Levels = new Button[25];
    public Button S1,S2,S3, S1_1, S1_2, S2_1, S2_2, S3_1;
    public GameObject S1Panel, S2Panel, S3Panel;

    private bool SpringBought = false, GolfBought = false, AmmoBoxBought = false;
    private int nr = 0;
    public int RoundCoins;
    public double AllCoins = 0;
    private float Jump = 300f, Speed = 0.8f, SuperJump = 1000f, SpringSpeed = 3000f;
    private int AirJumps = 1;
    private int CurrentLevel, MaxLevel;
    private float[] FinishLineHeight = { 30f, 45f, 70f, 105f, 130f, 185f, 250f, 355f, 450f, 550f , 700f, 800f, 850f, 900f, 950f, 1000f, 1100f, 1200f, 1300f, 1400f , 1800f, 2500f, 3000f, 3500f, 5000f};
    private string NewGameText = "New Game";
    private int SeasonNr = 1;

    private double SpringPrice = 1025.00, GolfBallPrice = 500.50, AmmoBoxPrice = 785.50;

    void Start()
    {
        SoccerBall.SetActive(false);
        GolfBall.SetActive(false);
        Spring.SetActive(false);
        ScoreText.gameObject.SetActive(false);
        ScoreText.text = "";
        StartGamePanel.SetActive(true);
        GameOverPanel.SetActive(false);
        UpgradesPanel.SetActive(false);
        LevelsPanel.SetActive(false);
        LevelWonPanel.SetActive(false);
        InventoryPanel.SetActive(false);
        FindObjectOfType<CameraScript>().enabled = false;
        FindObjectOfType<WorldGenerator>().enabled = false;
        SeasonClicked();
        S1_2.interactable = false;
        S2_2.interactable = false;
        S3_1.interactable = false;
        GolfBallText.text = GolfBallPrice + " coins";
        SpringText.text = SpringPrice + " coins";
        AmmoBoxText.text = AmmoBoxPrice + " coins";

        StartGameButton.onClick.AddListener(StartGameButtonClicked);
        StartGame_UpgradesButton.onClick.AddListener(StartGame_UpgradesButtonClicked);
        GameOver_UpgradesButton.onClick.AddListener(GameOver_UpgradesButtonClicked);
        ContinueButton.onClick.AddListener(ContinueButtonClicked);
        BackButton.onClick.AddListener(BackButtonClicked);
        NewGameButton.onClick.AddListener(NewGameButtonClicked);
        NextLevelButton.onClick.AddListener(NextLevelButtonClicked);
        LevelWon_UpgradesButton.onClick.AddListener(LevelWon_UpgradesButtonClicked);
        MenuButton.onClick.AddListener(MenuButtonClicked);
        LevelsButton.onClick.AddListener(LevelsButtonClicked);
        BackLevelsButton.onClick.AddListener(BackLevelsButtonClicked);
        InventoryButton.onClick.AddListener(InventoryButtonClicked);
        InventoryBackButton.onClick.AddListener(InventoryBackButtonClicked);

        GolfBallButton.onClick.AddListener(GolfBallButtonClicked);
        SpringButton.onClick.AddListener(SpringButtonClicked);
        AmmoBoxButton.onClick.AddListener(AmmoBoxClicked);

        Levels[0].onClick.AddListener(Level1ButtonClicked);
        Levels[1].onClick.AddListener(Level2ButtonClicked);
        Levels[2].onClick.AddListener(Level3ButtonClicked);
        Levels[3].onClick.AddListener(Level4ButtonClicked);
        Levels[4].onClick.AddListener(Level5ButtonClicked);
        Levels[5].onClick.AddListener(Level6ButtonClicked);
        Levels[6].onClick.AddListener(Level7ButtonClicked);
        Levels[7].onClick.AddListener(Level8ButtonClicked);
        Levels[8].onClick.AddListener(Level9ButtonClicked);
        Levels[9].onClick.AddListener(Level10ButtonClicked);
        Levels[10].onClick.AddListener(Level11ButtonClicked);
        Levels[11].onClick.AddListener(Level12ButtonClicked);
        Levels[12].onClick.AddListener(Level13ButtonClicked);
        Levels[13].onClick.AddListener(Level14ButtonClicked);
        Levels[14].onClick.AddListener(Level15ButtonClicked);
        Levels[15].onClick.AddListener(Level16ButtonClicked);
        Levels[16].onClick.AddListener(Level17ButtonClicked);
        Levels[17].onClick.AddListener(Level18ButtonClicked);
        Levels[18].onClick.AddListener(Level19ButtonClicked);
        Levels[19].onClick.AddListener(Level20ButtonClicked);
        Levels[20].onClick.AddListener(Level21ButtonClicked);
        Levels[21].onClick.AddListener(Level22ButtonClicked);
        Levels[22].onClick.AddListener(Level23ButtonClicked);
        Levels[23].onClick.AddListener(Level24ButtonClicked);
        Levels[24].onClick.AddListener(Level25ButtonClicked);

        for (int i = 1; i < Levels.Length; i++)
            Levels[i].interactable = false;

        S1.onClick.AddListener(S1ButtonClicked);
        S2.onClick.AddListener(S2ButtonClicked);
        S3.onClick.AddListener(S3ButtonClicked);

        S1_1.onClick.AddListener(S1_1ButtonClicked);
        S1_2.onClick.AddListener(S1_2ButtonClicked);
        S2_1.onClick.AddListener(S2_1ButtonClicked);
        S2_2.onClick.AddListener(S2_2ButtonClicked);
        S3_1.onClick.AddListener(S3_1ButtonClicked);
    }

    private void Update()
    {
        if(UpgradesPanel.activeSelf)
            UpgradesCoins.text = "Coins : " + AllCoins;
    }
    private void StartGameButtonClicked()
    {
        if(NewGameText == "New Game")
        {
            StartGamePanel.SetActive(false);
            LevelsPanel.SetActive(true);
        }
        else
        {
            FindObjectOfType<WorldGenerator>().ChangeFinishLine(FinishLineHeight[MaxLevel]);
            FindObjectOfType<WorldGenerator>().enabled = true;
            FindObjectOfType<CameraScript>().enabled = true;
            StartGamePanel.SetActive(false);
            ActivatePlayer();
            CheckSpring();
            ScoreText.gameObject.SetActive(true);
            ScoreText.text = "Height";
            FindObjectOfType<ScoreScript>().NewGame();
            FindObjectOfType<WorldGenerator>().StartNewGame();
        }
    }
    private void ContinueButtonClicked()
    {
        FindObjectOfType<WorldGenerator>().enabled = true;
        FindObjectOfType<CameraScript>().enabled = true;
        GameOverPanel.SetActive(false);
        AllCoins -= RoundCoins;
        ActivatePlayer();
        ScoreText.gameObject.SetActive(true);
        FindObjectOfType<WorldGenerator>().ContinueGame();
    }

    private void StartGame_UpgradesButtonClicked()
    {
        ContinueButton.interactable = true;
        nr = 1;
        StartGamePanel.SetActive(false);
        UpgradesPanel.SetActive(true);
        UpgradesCoins.text = "Coins : " + AllCoins;
    }
    private void GameOver_UpgradesButtonClicked()
    {
        nr = 2;
        GameOverPanel.SetActive(false);
        UpgradesPanel.SetActive(true);
        ContinueButton.interactable = false;
        UpgradesCoins.text = "Coins : " + AllCoins;
    }

    private void BackButtonClicked()
    {
        if (nr == 1)
            StartGamePanel.SetActive(true);
        else if (nr == 2)
            GameOverPanel.SetActive(true);
        else if (nr == 3)
            LevelWonPanel.SetActive(true);
        UpgradesPanel.SetActive(false);
    }

    private void BackLevelsButtonClicked()
    {
        LevelsPanel.SetActive(false);
        StartGamePanel.SetActive(true);
    }
    private void NewGameButtonClicked()
    {
        FindObjectOfType<WorldGenerator>().enabled = true;
        FindObjectOfType<CameraScript>().enabled = true;
        GameOverPanel.SetActive(false);
        ActivatePlayer();
        CheckSpring();
        ScoreText.gameObject.SetActive(true);
        ScoreText.text = "Height";
        FindObjectOfType<ScoreScript>().NewGame();
        ContinueButton.interactable = true;
        FindObjectOfType<WorldGenerator>().StartNewGame();
    }

    private void InventoryButtonClicked()
    {
        StartGamePanel.SetActive(false);
        InventoryPanel.SetActive(true);
        CheckUpgrades();
    }

    private void InventoryBackButtonClicked()
    {
        StartGamePanel.SetActive(true);
        InventoryPanel.SetActive(false);
    }

    private void CheckUpgrades()
    {
        if (SpringBought == true)
            S3_1.interactable = true;
        if (GolfBought == true)
            S1_2.interactable = true;
        if (AmmoBoxBought == true)
            S2_2.interactable = true;

    }
    private void NextLevelButtonClicked()
    {
        LevelWonPanel.SetActive(false);
        FindObjectOfType<WorldGenerator>().ChangeFinishLine(FinishLineHeight[CurrentLevel]);
        FindObjectOfType<WorldGenerator>().enabled = true;
        FindObjectOfType<CameraScript>().enabled = true;
        ActivatePlayer();
        CheckSpring();
        ScoreText.gameObject.SetActive(true);
        ScoreText.text = "Height";
        FindObjectOfType<ScoreScript>().NewGame();
        FindObjectOfType<WorldGenerator>().StartNewGame();
    }

    private void LevelWon_UpgradesButtonClicked()
    {
        nr = 3;
        LevelWonPanel.SetActive(false);
        UpgradesPanel.SetActive(true);
        UpgradesCoins.text = "Coins : " + AllCoins;
    }

    private void MenuButtonClicked()
    {
        LevelWonPanel.SetActive(false);
        StartGamePanel.SetActive(true);
    }

    private void LevelsButtonClicked()
    {
        LevelsPanel.SetActive(true);
        StartGamePanel.SetActive(false);
    }
    private void GolfBallButtonClicked()
    {
        if (AllCoins >= GolfBallPrice)
        {
            FindObjectOfType<WorldGenerator>().ChangePlayer(1);
            GolfBought = true;
            GolfBallText.text = "BOUGHT";
            GolfBallText.color = Color.red;
            GolfBallButton.interactable = false;
            AllCoins -= GolfBallPrice;
        }
    }
    private void SpringButtonClicked()
    {
        if (AllCoins >= SpringPrice)
        {
            FindObjectOfType<ButtonScrollControl>().SpringSpeedUpgradeButton();
            Spring.SetActive(true);
            SpringBought = true;
            SpringText.text = "BOUGHT";
            SpringText.color = Color.red;
            SpringButton.interactable = false;
            AllCoins -= SpringPrice;
        }
    }

    private void AmmoBoxClicked()
    {
        if (AllCoins >= AmmoBoxPrice)
        {
            FindObjectOfType<WorldGenerator>().ChangeModel3d(1);
            AmmoBoxText.text = "BOUGHT";
            AmmoBoxText.color = Color.red;
            AmmoBoxButton.interactable = false;
            AmmoBoxBought = true;
            AllCoins -= AmmoBoxPrice;
        }
    }

    public void LevelWon()
    {
        LevelWonPanel.SetActive(true);
        FindObjectOfType<WorldGenerator>().enabled = false;
        RoundCoins = FindObjectOfType<ScoreScript>().MaxScore / 3 * 10;
        ScoreText.gameObject.SetActive(false);
        ScoreText.text = "";
        LevelWonCoinsText.text = "+" + RoundCoins + " Coins";
        AllCoins += RoundCoins;
        CurrentLevel = CurrentLevel + 1;
        if (CurrentLevel <= 7)
        {
            Levels[CurrentLevel].interactable = true;
            MaxLevelFunction(CurrentLevel);
        }
        else
            NextLevelButton.interactable = false;
    }
    public void PlayerDied()
    {
        GameOverPanel.SetActive(true);
        FindObjectOfType<WorldGenerator>().enabled = false;
        RoundCoins = FindObjectOfType<ScoreScript>().MaxScore / 10;
        ScoreText.gameObject.SetActive(false);
        ScoreText.text = "";
        GameOverCoins.text = "+" + RoundCoins + " Coins";
        AllCoins += RoundCoins;
    }

    private void ActivatePlayer()
    {
        if (GolfBought == true)
            GolfBall.SetActive(true);
        else
            SoccerBall.SetActive(true);
        FindObjectOfType<player>().ModifyJump(Jump);
        FindObjectOfType<player>().ModifySpeed(Speed);
        FindObjectOfType<player>().ModifySuperJump(SuperJump);
        FindObjectOfType<player>().ModifySpringSpeed(SpringSpeed);
        FindObjectOfType<player>().ModifyAirJumps(AirJumps);
    }
    private void CheckSpring()
    {
        if (SpringBought == true)
            FindObjectOfType<player>().SpringBought();
        else
            FindObjectOfType<player>().SpringNotBought();
    }

    public bool CheckGolfBall()
    {
        return GolfBought;
    }

    public void ModifySpeed(double val)
    {
        Speed = (float)val;
    }
    public void ModifyJump(double val)
    {
        Jump = (float)val;
    }
    public void ModifySuperJump(double val)
    {
        SuperJump = (float)val;
    }
    public void ModifySpringSpeed(double val)
    {
        SpringSpeed = (float)val;
    }
    public void ModifyAirJumps(int val)
    {
        AirJumps = val;
    }

    public bool CheckSpringBought()
    {
        return SpringBought;
    }

    private void Level1ButtonClicked()
    {
        LevelsPanel.SetActive(false);
        CurrentLevel = 0;
        FindObjectOfType<WorldGenerator>().ChangeFinishLine(FinishLineHeight[CurrentLevel]);
        NewGameText = "Continue";
        StartPanel_NewGameText.text = "Continue";
        MaxLevelFunction(CurrentLevel);
        FindObjectOfType<WorldGenerator>().enabled = true;
        FindObjectOfType<CameraScript>().enabled = true;
        StartGamePanel.SetActive(false);
        ActivatePlayer();
        CheckSpring();
        ScoreText.gameObject.SetActive(true);
        ScoreText.text = "Height";
        FindObjectOfType<ScoreScript>().NewGame();
        FindObjectOfType<WorldGenerator>().StartNewGame();
    }
    private void Level2ButtonClicked()
    {
        LevelsPanel.SetActive(false);
        CurrentLevel = 1;
        FindObjectOfType<WorldGenerator>().ChangeFinishLine(FinishLineHeight[CurrentLevel]);
        MaxLevelFunction(CurrentLevel);
        StartNewGame_NextGame();
    }
    private void Level3ButtonClicked()
    {
        LevelsPanel.SetActive(false);
        CurrentLevel = 2;
        FindObjectOfType<WorldGenerator>().ChangeFinishLine(FinishLineHeight[CurrentLevel]);
        MaxLevelFunction(CurrentLevel);
        StartNewGame_NextGame();
    }
    private void Level4ButtonClicked()
    {
        LevelsPanel.SetActive(false);
        CurrentLevel = 3;
        FindObjectOfType<WorldGenerator>().ChangeFinishLine(FinishLineHeight[CurrentLevel]);
        MaxLevelFunction(CurrentLevel);
        StartNewGame_NextGame();
    }
    private void Level5ButtonClicked()
    {
        LevelsPanel.SetActive(false);
        CurrentLevel = 4;
        FindObjectOfType<WorldGenerator>().ChangeFinishLine(FinishLineHeight[CurrentLevel]);
        MaxLevelFunction(CurrentLevel);
        StartNewGame_NextGame();
    }
    private void Level6ButtonClicked()
    {
        LevelsPanel.SetActive(false);
        CurrentLevel = 5;
        FindObjectOfType<WorldGenerator>().ChangeFinishLine(FinishLineHeight[CurrentLevel]);
        MaxLevelFunction(CurrentLevel);
        StartNewGame_NextGame();
    }
    private void Level7ButtonClicked()
    {
        LevelsPanel.SetActive(false);
        CurrentLevel = 6;
        FindObjectOfType<WorldGenerator>().ChangeFinishLine(FinishLineHeight[CurrentLevel]);
        MaxLevelFunction(CurrentLevel);
        StartNewGame_NextGame();
    }
    private void Level8ButtonClicked()
    {
        LevelsPanel.SetActive(false);
        CurrentLevel = 7;
        FindObjectOfType<WorldGenerator>().ChangeFinishLine(FinishLineHeight[CurrentLevel]);
        MaxLevelFunction(CurrentLevel);
        StartNewGame_NextGame();
    }

    private void Level9ButtonClicked()
    {
        LevelsPanel.SetActive(false);
        CurrentLevel = 8;
        FindObjectOfType<WorldGenerator>().ChangeFinishLine(FinishLineHeight[CurrentLevel]);
        MaxLevelFunction(CurrentLevel);
        StartNewGame_NextGame();
    }

    private void Level10ButtonClicked()
    {
        LevelsPanel.SetActive(false);
        CurrentLevel = 9;
        FindObjectOfType<WorldGenerator>().ChangeFinishLine(FinishLineHeight[CurrentLevel]);
        MaxLevelFunction(CurrentLevel);
        StartNewGame_NextGame();
    }

    private void Level11ButtonClicked()
    {
        LevelsPanel.SetActive(false);
        CurrentLevel = 10;
        FindObjectOfType<WorldGenerator>().ChangeFinishLine(FinishLineHeight[CurrentLevel]);
        MaxLevelFunction(CurrentLevel);
        StartNewGame_NextGame();
    }

    private void Level12ButtonClicked()
    {
        LevelsPanel.SetActive(false);
        CurrentLevel = 11;
        FindObjectOfType<WorldGenerator>().ChangeFinishLine(FinishLineHeight[CurrentLevel]);
        MaxLevelFunction(CurrentLevel);
        StartNewGame_NextGame();
    }

    private void Level13ButtonClicked()
    {
        LevelsPanel.SetActive(false);
        CurrentLevel = 12;
        FindObjectOfType<WorldGenerator>().ChangeFinishLine(FinishLineHeight[CurrentLevel]);
        MaxLevelFunction(CurrentLevel);
        StartNewGame_NextGame();
    }

    private void Level14ButtonClicked()
    {
        LevelsPanel.SetActive(false);
        CurrentLevel = 13;
        FindObjectOfType<WorldGenerator>().ChangeFinishLine(FinishLineHeight[CurrentLevel]);
        MaxLevelFunction(CurrentLevel);
        StartNewGame_NextGame();
    }

    private void Level15ButtonClicked()
    {
        LevelsPanel.SetActive(false);
        CurrentLevel = 14;
        FindObjectOfType<WorldGenerator>().ChangeFinishLine(FinishLineHeight[CurrentLevel]);
        MaxLevelFunction(CurrentLevel);
        StartNewGame_NextGame();
    }

    private void Level16ButtonClicked()
    {
        LevelsPanel.SetActive(false);
        CurrentLevel = 15;
        FindObjectOfType<WorldGenerator>().ChangeFinishLine(FinishLineHeight[CurrentLevel]);
        MaxLevelFunction(CurrentLevel);
        StartNewGame_NextGame();
    }

    private void Level17ButtonClicked()
    {
        LevelsPanel.SetActive(false);
        CurrentLevel = 16;
        FindObjectOfType<WorldGenerator>().ChangeFinishLine(FinishLineHeight[CurrentLevel]);
        MaxLevelFunction(CurrentLevel);
        StartNewGame_NextGame();
    }

    private void Level18ButtonClicked()
    {
        LevelsPanel.SetActive(false);
        CurrentLevel = 17;
        FindObjectOfType<WorldGenerator>().ChangeFinishLine(FinishLineHeight[CurrentLevel]);
        MaxLevelFunction(CurrentLevel);
        StartNewGame_NextGame();
    }

    private void Level19ButtonClicked()
    {
        LevelsPanel.SetActive(false);
        CurrentLevel = 18;
        FindObjectOfType<WorldGenerator>().ChangeFinishLine(FinishLineHeight[CurrentLevel]);
        MaxLevelFunction(CurrentLevel);
        StartNewGame_NextGame();
    }

    private void Level20ButtonClicked()
    {
        LevelsPanel.SetActive(false);
        CurrentLevel = 19;
        FindObjectOfType<WorldGenerator>().ChangeFinishLine(FinishLineHeight[CurrentLevel]);
        MaxLevelFunction(CurrentLevel);
        StartNewGame_NextGame();
    }

    private void Level21ButtonClicked()
    {
        LevelsPanel.SetActive(false);
        CurrentLevel = 20;
        FindObjectOfType<WorldGenerator>().ChangeFinishLine(FinishLineHeight[CurrentLevel]);
        MaxLevelFunction(CurrentLevel);
        StartNewGame_NextGame();
    }

    private void Level22ButtonClicked()
    {
        LevelsPanel.SetActive(false);
        CurrentLevel = 21;
        FindObjectOfType<WorldGenerator>().ChangeFinishLine(FinishLineHeight[CurrentLevel]);
        MaxLevelFunction(CurrentLevel);
        StartNewGame_NextGame();
    }

    private void Level23ButtonClicked()
    {
        LevelsPanel.SetActive(false);
        CurrentLevel = 22;
        FindObjectOfType<WorldGenerator>().ChangeFinishLine(FinishLineHeight[CurrentLevel]);
        MaxLevelFunction(CurrentLevel);
        StartNewGame_NextGame();
    }

    private void Level24ButtonClicked()
    {
        LevelsPanel.SetActive(false);
        CurrentLevel = 23;
        FindObjectOfType<WorldGenerator>().ChangeFinishLine(FinishLineHeight[CurrentLevel]);
        MaxLevelFunction(CurrentLevel);
        StartNewGame_NextGame();
    }

    private void Level25ButtonClicked()
    {
        LevelsPanel.SetActive(false);
        CurrentLevel = 24;
        FindObjectOfType<WorldGenerator>().ChangeFinishLine(FinishLineHeight[CurrentLevel]);
        MaxLevelFunction(CurrentLevel);
        StartNewGame_NextGame();
    }



    private void MaxLevelFunction(int val)
    {
        if (MaxLevel < val)
            MaxLevel = val;
    }
    private void StartNewGame_NextGame()
    {
        MaxLevelFunction(CurrentLevel);
        FindObjectOfType<WorldGenerator>().enabled = true;
        FindObjectOfType<CameraScript>().enabled = true;
        StartGamePanel.SetActive(false);
        ActivatePlayer();
        CheckSpring();
        ScoreText.gameObject.SetActive(true);
        ScoreText.text = "Height";
        FindObjectOfType<ScoreScript>().NewGame();
        FindObjectOfType<WorldGenerator>().StartNewGame();
    }

    private void S1ButtonClicked()
    {
        SeasonNr = 1;
        SeasonClicked();
    }
    private void S2ButtonClicked()
    {
        SeasonNr = 2;
        SeasonClicked();
    }
    private void S3ButtonClicked()
    {
        SeasonNr = 3;
        SeasonClicked();
    }

    private void SeasonClicked()
    {
        switch(SeasonNr)
        {
            case 1:
                S1.GetComponent<Image>().color = Color.blue;
                S2.GetComponent<Image>().color = Color.black;
                S3.GetComponent<Image>().color = Color.black;
                break;
            case 2:
                S1.GetComponent<Image>().color = Color.black;
                S2.GetComponent<Image>().color = Color.blue;
                S3.GetComponent<Image>().color = Color.black;
                break;
            case 3:
                S1.GetComponent<Image>().color = Color.black;
                S2.GetComponent<Image>().color = Color.black;
                S3.GetComponent<Image>().color = Color.blue;
                break;
        }
        SeasonPanel();
    }

    private void SeasonPanel()
    {
        switch (SeasonNr)
        {
            case 1:
                S1Panel.SetActive(true);
                S2Panel.SetActive(false);
                S3Panel.SetActive(false);
                break;
            case 2:
                S1Panel.SetActive(false);
                S2Panel.SetActive(true);
                S3Panel.SetActive(false);
                break;
            case 3:
                S1Panel.SetActive(false);
                S2Panel.SetActive(false);
                S3Panel.SetActive(true);
                break;
        }
    }

    private void S1_1ButtonClicked()
    {
        FindObjectOfType<WorldGenerator>().ChangePlayer(0);
        GolfBought = false;
    }

    private void S1_2ButtonClicked()
    {
        FindObjectOfType<WorldGenerator>().ChangePlayer(1);
        GolfBought = true;
    }

    private void S2_1ButtonClicked()
    {
        FindObjectOfType<WorldGenerator>().ChangeModel3d(0);
        AmmoBoxBought = false;
    }

    private void S2_2ButtonClicked()
    {
        FindObjectOfType<WorldGenerator>().ChangeModel3d(1);
        AmmoBoxBought = true;
    }

    private void S3_1ButtonClicked()
    {
        FindObjectOfType<ButtonScrollControl>().SpringSpeedUpgradeButton();
        SpringBought = true;
    }

    public void updateCoins(double val)
    {
        AllCoins -= val;
    }
}
