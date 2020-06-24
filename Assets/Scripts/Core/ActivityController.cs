using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;

public class ActivityController : MonoBehaviour
{
    public string activityName;
    public ACTIVITY activity;
    public ACTIVITYHEADER activityheader;
    public ACTIVITYBUTTON activitybutton;
    public STATUSBAR statusbar;
    public Button nextButton;
    
    int buttonClicked;
    void Awake()
    {
        activityName = LaptopController.instance.selectedActivity;
        
        showSelectedActivity(activityName);
        showActivityIcons(activityName);
    }
    void Start()
    {
        buttonClicked = 0;
        statusbar.SetStatus();
    }

    // Update is called once per frame
    void Update()
    {
   
    }
        public void buttonClick()
    {
        buttonClicked += 1;
        print(buttonClicked);
        if (buttonClicked == 4)
        {
            activity.act2.gameObject.SetActive(false);
            activitybutton.buttonArea.SetActive(false);
            nextButton.gameObject.SetActive(true);
            switch (activityName)
            {
                case "Basket":
                    statusbar.basket.gameObject.SetActive(true);
                    Player.instance.doBasketball();
                    break;
                case "Hangout":
                    statusbar.hangout.SetActive(true);
                    Player.instance.doHangout();

                    break;
                case "HomeChores":
                    statusbar.homechores.gameObject.SetActive(true);
                    Player.instance.doBeautySalon();
                    break;
                case "BeautySalon":
                    statusbar.beautysalon.gameObject.SetActive(true);
                    Player.instance.doHomeChores();
                    break;
                case "Rest":
                    Player.instance.doRest();
                    break;

            }
            buttonClicked = 0;
        }
        if (buttonClicked == 2)
        {
            activity.act1.gameObject.SetActive(false);
        }
    }

    void showSelectedActivity(string selectedActivity)
    {
        
        Texture2D tex1 = Resources.Load("images/UI/Layar Kegiatan/image kegiatan/" + selectedActivity + "1") as Texture2D;
        Texture2D tex2 = Resources.Load("images/UI/Layar Kegiatan/image kegiatan/" + selectedActivity + "2") as Texture2D;
        Texture2D tex3 = Resources.Load("images/UI/Layar Kegiatan/image kegiatan/" + selectedActivity + "3") as Texture2D;
        activity.act1.texture = tex1;
        activity.act2.texture = tex2;
        activity.act3.texture = tex3;

        /*SpriteState spriteState = new SpriteState();
        spriteState = button.spriteState;

        Sprite buttonTex1 = Resources.Load("images/UI/Layar Kegiatan/ICON Layar Kegiatan/" + selectedActivity + "1") as Sprite;
        Sprite buttonTex2 = Resources.Load("images/UI/Layar Kegiatan/ICON Layar Kegiatan/" + selectedActivity + "2") as Sprite;
        button.GetComponent<Image>().sprite = buttonTex1;
        spriteState.pressedSprite = buttonTex2;*/

    }

    void showActivityIcons(string selectedActivity)
    {
        switch (selectedActivity)
        {
            case "Basket":
                activitybutton.basket.gameObject.SetActive(true);
                activityheader.basket.gameObject.SetActive(true);
                break;
            case "Hangout":
                activitybutton.hangout.gameObject.SetActive(true);
                activityheader.hangout.gameObject.SetActive(true);
                break;
            case "HomeChores":
                activitybutton.homechores.gameObject.SetActive(true);
                activityheader.homechores.gameObject.SetActive(true);
                break;
            case "BeautySalon":
                activitybutton.beautysalon.gameObject.SetActive(true);
                activityheader.beautysalon.gameObject.SetActive(true);
                break;
            case "Rest":
                activitybutton.rest.gameObject.SetActive(true);
                activityheader.rest.gameObject.SetActive(true);
                break;
        
        }

    }

    public void Next()
    {
        NovelController.instance.Next();
    }
    [System.Serializable]
    public class ACTIVITY
    {
        public RawImage act1, act2, act3;
    }

    [System.Serializable]
    public class ACTIVITYHEADER
    {
        public GameObject basket, hangout, homechores, beautysalon, rest;
    }

    [System.Serializable]
    public class ACTIVITYBUTTON
    {
        public GameObject buttonArea;
        public Button basket, hangout, homechores, beautysalon, rest;
    }

    [System.Serializable]
    public class STATUSBAR
    {
        public GameObject statusBarArea;
        public GameObject hangout;
        public Slider basket, hangoutSocial, hangoutFitness, beautysalon, homechores;

        public void SetStatus()
        {
            basket.value = Player.instance.status.fitness;
            beautysalon.value = Player.instance.status.beauty;
            hangoutSocial.value = Player.instance.status.social;
            hangoutFitness.value = Player.instance.status.fitness;
            homechores.value = Player.instance.status.social;
        }
    }


}
