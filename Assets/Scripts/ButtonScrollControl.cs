using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScrollControl : MonoBehaviour
{
    public GameObject ButtonPrefab;

    private double JumpStartPrice = 300.25;

    private double SpeedStartPrice = 200.25;

    private double SuperJumpStartPrice = 100.25;

    private double SpringSpeedPrice = 500.27;

    private double AirJumpsPrice = 300.95;


    private void Start()
    {
        JumpSpeedUpgradeButton();
        SpeedUpgradeButton();
        SuperJumpUpgradeButton();
        if(FindObjectOfType<UI_ManagerScript>().CheckSpringBought() == true)
            SpringSpeedUpgradeButton();
        AirJumpsUpgradeButton();
    }

    private void JumpSpeedUpgradeButton()
    {
        GameObject Button = Instantiate(ButtonPrefab) as GameObject;
        Button.SetActive(true);
        Button.GetComponent<ButtonListButton>().setJumpButton("JumpButton", JumpStartPrice);
        Button.transform.SetParent(ButtonPrefab.transform.parent, false);
    }

    private void SpeedUpgradeButton()
    {
        GameObject Button = Instantiate(ButtonPrefab) as GameObject;
        Button.SetActive(true);
        Button.GetComponent<ButtonListButton>().setSpeedButton("SpeedButton", SpeedStartPrice);
        Button.transform.SetParent(ButtonPrefab.transform.parent, false);
    }

    private void SuperJumpUpgradeButton()
    {
        GameObject Button = Instantiate(ButtonPrefab) as GameObject;
        Button.SetActive(true);
        Button.GetComponent<ButtonListButton>().setSuperJumpButton("SuperJumpButton", SuperJumpStartPrice);
        Button.transform.SetParent(ButtonPrefab.transform.parent, false);
    }

    public void SpringSpeedUpgradeButton()
    {
        GameObject Button = Instantiate(ButtonPrefab) as GameObject;
        Button.SetActive(true);
        Button.GetComponent<ButtonListButton>().setSpringSpeedButton("SpringSpeedButton", SpringSpeedPrice);
        Button.transform.SetParent(ButtonPrefab.transform.parent, false);
    }
    private void AirJumpsUpgradeButton()
    {
        GameObject Button = Instantiate(ButtonPrefab) as GameObject;
        Button.SetActive(true);
        Button.GetComponent<ButtonListButton>().setAirJumpsButton("AirJumpsButton", AirJumpsPrice);
        Button.transform.SetParent(ButtonPrefab.transform.parent, false);
    }
}
