using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GamePanel : MonoBehaviour
{
    public static GamePanel instance;

    public GameObject SettingsButton;
    public GameObject Date, Coin;
    public Text week, day, month, date, coin;
    string[] daysInWeek = { "MONDAY", "TUESDAY", "WEDNESDAY", "THURDAY", "FRIDAY", "SATURDAY", "SUNDAY" };

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        updatePanel();
    }

    // Update is called once per frame
    void Update()
    {
        string scene = SceneManager.GetActiveScene().name;

        if (Player.instance.day > 0)
        {
            if (scene == "Dialogue" || scene == "Laptop" || scene == "Evaluation")
            {
                updatePanel();
                enablePanel(Coin);
                enablePanel(Date);
            }
        }
    }

    public void disablePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void enablePanel(GameObject panel)
    {
        panel.SetActive(true);
    }

    public void updatePanel()
    {
        int n = Player.instance.day - 1;
        if (n == -1)
        {
            n = 0;
        }
        string currDay = daysInWeek[n];


        string currWeek = Player.instance.week.ToString();
        string currMonth = Player.instance.month.ToString();
        string currDate = Player.instance.date.ToString();
        string currCoin = Player.instance.money.ToString();

        week.text = currWeek;
        day.text = currDay;
        month.text = currMonth;
        date.text = currDate;
        coin.text = currCoin;
    }
}
