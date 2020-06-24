using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    public static PanelController instance;
    public Text week, day, month, date, coin;
    string[] daysInWeek = { "MONDAY", "TUESDAY", "WEDNESDAY", "THURDAY", "FRIDAY", "SATURDAY", "SUNDAY" };
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        updatePanel();
    }

    public void updatePanel()
    {
        string currWeek = Player.instance.week.ToString();
        string currDay = daysInWeek[Player.instance.day + 1];
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
