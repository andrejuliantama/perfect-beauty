using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LaptopController : MonoBehaviour
{
    public static LaptopController instance;
    public STATUSBAR statusbar;
    public Button basket, hangout, beautySalon, homeChores, rest;
    public Button confirm;
    public string selectedActivity;
    public GameObject statusScreen, selectActivity;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        statusScreen.SetActive(true);
        statusbar.SetStatus();
        selectActivity.SetActive(false);
        determineButtonState();
        //confirm.onClick.AddListener(delegate { NovelController.instance.Next(); });
        
        selectedActivity = "";
        
    }

    // Update is called once per frame
    void Update()
    {
              
    }

    public void doActivity()
    {
        NovelController.instance.Next();
        //SceneManager.LoadScene("Activity");
        //Player.instance.addDay();
    }
    public void makePlan()
    {
        statusScreen.SetActive(false);
        selectActivity.SetActive(true);
    }

    public void setActivity(string activity)
    {
        selectedActivity = activity;
    }

    public void determineButtonState()
    {
        bool isWeekday = (Player.instance.day < 6);
        bool shouldRest = (Player.instance.week == 3 && Player.instance.day < 4);
        bool isUnlocked = (Player.instance.week > 1);
        int money = Player.instance.money;

        //basket
        if (isWeekday && !shouldRest)
        {
            basket.interactable = true;
        }
        else
        {
            basket.interactable = false;
        }
        
        //hangout
        if (money > 1 && !shouldRest)
        {
            hangout.interactable = true;
        }
        else
        {
            hangout.interactable = false;
        }
        
        //beauty salon
        if (isUnlocked && money > 4 && !isWeekday &&  !shouldRest)
        {
            beautySalon.interactable = true;
        }
        else
        {
            beautySalon.interactable = false;
        }
        
        //homechores
        if (isUnlocked && !shouldRest)
        {
            homeChores.interactable = true;
        }
        else
        {
            homeChores.interactable = false;
        }
        
        //rest
        if (shouldRest)
        {
            rest.interactable = true;
        }
        else
        {
            rest.interactable = false;
        }

    }

    [System.Serializable]
    public class STATUSBAR
    {
        public Text weight;
        public Slider fitness, beauty, social, confident, happiness;
        int maxStatus = 120;
        public void SetStatus()
        {
            fitness.value = Player.instance.status.fitness;
            beauty.value = Player.instance.status.beauty;
            social.value = Player.instance.status.social;
            confident.value = Player.instance.status.confident;
            happiness.value = Player.instance.status.happiness;
            weight.text = Player.instance.weight.ToString();
        }

    }
}
