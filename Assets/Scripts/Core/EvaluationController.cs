using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EvaluationController : MonoBehaviour
{
    public STATUSBAR statusbar;
    public bool win;
    public GameObject WinningStatement, LosingStatement;
    void Start()
    {
        statusbar.SetStatus();
        evaluateStatus();
        if (win)
        {
            WinningStatement.SetActive(true);
        }
        else
        {
            LosingStatement.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void evaluateStatus()
    {
        int fitness = Player.instance.status.fitness;
        int beauty = Player.instance.status.beauty;
        int social = Player.instance.status.social;
        int confident = Player.instance.status.confident;
        int happiness = Player.instance.status.happiness;

        win = (fitness > 10 && beauty > 10 && social > 10 && confident > 10 && happiness > 10);
    }

    public void Next()
    {
        Player.instance.addDay();
        NovelController.instance.Next();
    }
    public void Quit()
    {
        Application.Quit();
    }

    public void Retry()
    {
        NovelController.instance.Command_Retry();
    }

    [System.Serializable]
    public class STATUSBAR
    {
      
        public Slider fitness, beauty, social, confident, happiness;
        int maxStatus = 120;
        public void SetStatus()
        {
            fitness.value = Player.instance.status.fitness;
            beauty.value = Player.instance.status.beauty;
            social.value = Player.instance.status.social;
            confident.value = Player.instance.status.confident;
            happiness.value = Player.instance.status.happiness;
        }

    }
}
