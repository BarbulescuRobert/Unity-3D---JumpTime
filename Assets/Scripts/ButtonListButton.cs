using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonListButton : MonoBehaviour
{
    public Button button;
    public Text TextButton;
    double Price;
    string ButtonType;

    double JumpSpeed = 300;
    double Speed = 0.8;
    double SuperJumpSpeed = 1000;
    double SpringSpeed = 3000;
    int AirJumpsNr = 0;

    public Sprite JumpImage;
    public Sprite SpeedImage;
    public Sprite SuperJumpImage;
    public Sprite SpringSpeedImage;
    public Sprite[] AirJumpsImage = new Sprite[3];
    public void setJumpButton(string buttonType, double price)
    {
        Price = System.Math.Round(price, 2);
        TextButton.text = Price + " coins";
        ButtonType = buttonType;
    }

    public void setSpeedButton(string buttonType, double price)
    {
        Price = System.Math.Round(price, 2);
        TextButton.text = Price + " coins";
        ButtonType = buttonType;
    }

    public void setSuperJumpButton(string buttonType, double price)
    {
        Price = System.Math.Round(price, 2);
        TextButton.text = Price + " coins";
        ButtonType = buttonType;
    }

    public void setSpringSpeedButton(string buttonType, double price)
    {
        Price = System.Math.Round(price, 2);
        TextButton.text = Price + " coins";
        ButtonType = buttonType;
    }
    public void setAirJumpsButton(string buttonType, double price)
    {
        Price = System.Math.Round(price, 2);
        TextButton.text = Price + " coins";
        ButtonType = buttonType;
    }

    private void Start()
    {
        button = gameObject.GetComponent<Button>();

        button.onClick.AddListener(ButtonClicked);

        switch (ButtonType)
        {
            case "JumpButton": button.GetComponent<Image>().sprite = JumpImage; break;
            case "SpeedButton": button.GetComponent<Image>().sprite = SpeedImage; break;
            case "SuperJumpButton": button.GetComponent<Image>().sprite = SuperJumpImage; break;
            case "SpringSpeedButton": button.GetComponent<Image>().sprite = SpringSpeedImage; break;
            case "AirJumpsButton": button.GetComponent<Image>().sprite = AirJumpsImage[AirJumpsNr]; break;
        }
        
    }


    private void ButtonClicked()
    {
        double price = FindObjectOfType<UI_ManagerScript>().AllCoins;
        if (ButtonType == "JumpButton")
        {  
            if (price >= Price)
            {
                FindObjectOfType<UI_ManagerScript>().updateCoins(Price);
                JumpSpeed = JumpSpeed + JumpSpeed / 10;
                FindObjectOfType<UI_ManagerScript>().ModifyJump(JumpSpeed);
                setJumpButton(ButtonType, Price + Price / 10);
            }
        }
        else if (ButtonType == "SpeedButton")
        {
            if (price >= Price)
            {
                FindObjectOfType<UI_ManagerScript>().updateCoins(Price);
                Speed = Speed + Speed / 10;
                FindObjectOfType<UI_ManagerScript>().ModifySpeed(Speed);
                setSpeedButton(ButtonType, Price + Price / 10);
            }
        }
        else if (ButtonType == "SuperJumpButton")
        {
            if (price >= Price)
            {
                FindObjectOfType<UI_ManagerScript>().updateCoins(Price);
                SuperJumpSpeed = SuperJumpSpeed + SuperJumpSpeed / 10;
                FindObjectOfType<UI_ManagerScript>().ModifySuperJump(SuperJumpSpeed);
                setSuperJumpButton(ButtonType, Price + Price / 10);
            }
        }
        else if (ButtonType == "SpringSpeedButton")
        {
            if (price >= Price)
            {
                FindObjectOfType<UI_ManagerScript>().updateCoins(Price);
                SpringSpeed = SpringSpeed + SpringSpeed / 10;
                FindObjectOfType<UI_ManagerScript>().ModifySpringSpeed(SpringSpeed);
                setSpringSpeedButton(ButtonType, Price + Price / 10);
            }
        }
        else if (ButtonType == "AirJumpsButton")
        {
            if (price >= Price)
            {
                AirJumpsNr = AirJumpsNr + 1;
                if (AirJumpsNr >= 6)
                {
                    button.GetComponent<Image>().sprite = AirJumpsImage[AirJumpsNr - 1];
                    button.interactable = false;
                    FindObjectOfType<UI_ManagerScript>().ModifyAirJumps(AirJumpsNr + 1);
                    TextButton.text = "MAX";
                    TextButton.color = Color.red;
                }
                else
                {
                    FindObjectOfType<UI_ManagerScript>().updateCoins(Price);
                    button.GetComponent<Image>().sprite = AirJumpsImage[AirJumpsNr];
                    FindObjectOfType<UI_ManagerScript>().ModifyAirJumps(AirJumpsNr + 1);
                    setAirJumpsButton(ButtonType, Price + Price / 10);
                }
            }
        }

    }

}
