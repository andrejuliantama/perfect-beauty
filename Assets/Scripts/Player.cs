using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player instance;
    public STATUS status;
    public SAVEDDATA saveddata;
    public string pName;
    public int month = 6;
    public int date = 1;
    public int week, day;
    //string[] days = {"Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday"};
    public int weight, money; //berat badan
    
    void Awake()
    {
        day = 0;
        week = 0;
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        
    }
    public void createPlayer(string s)
    {
        pName = s;
        weight = 65;
        money = 10; //dari week 0, ke week 1 +5, jadi 15
        status.fitness = 30;
        status.social = 30;
        status.beauty = 30;
        status.confident = 30;
        status.happiness = 30;
        status.nBasketball = 0;
        status.nHangout = 0;
        status.nHomeChores = 0;
        status.nBeautySalon = 0;
    }

    //setter
    public void setDay(int n)
    {
        day = n;
    }

    public void setWeek(int n)
    {
        week = n;
    }

    public void setWeight(int n)
    {
        weight = n;
    }

    public void setMoney(int n)
    {
        money = n;
    }

    public void setFitness(int n)
    {
        status.fitness = n;
    }

    public void setSocial(int n)
    {
        status.social = n;
    }

    public void setBeauty(int n)
    {
        status.beauty = n;
    }

    public void setConfident(int n)
    {
        status.confident = n;
    }

    public void setHappiness(int n)
    {
        status.happiness = n;
    }

    //Player day, week, weight, money
    public void addDay()
    {
        addDate();
        if (day == 7)
        {
            setDay(1);
            addWeek();
        }
        else
        {
            setDay(day + 1);
        }
    }

    public void addWeek()
    {
        if (week % 4 == 0)
        {
            month++;
        }
        setWeek(week + 1);
        addMoney(5);
        setResetPoint();
    }

    public void addWeight(int n)
    {
        setWeight(weight + n);
    }

    public void reduceWeight(int n)
    {
        setWeight(weight - n);
    }

    public void addMoney(int n)
    {
        setMoney(money + n);
    }

    public void reduceMoney(int n)
    {
        setMoney(money - n);
    }

    //Player Activities
    public void doBasketball()
    {
        //only weekday
        setFitness(status.fitness + 10);
        status.nBasketball += 1;
    }

    public void doHangout()
    {
        //everyday
        setSocial(status.social + 10);
        setFitness(status.fitness - 5);
        if (status.fitness < 0)
        {
            status.fitness = 0;
        }
        reduceMoney(2);
        status.nHangout += 1;
    }

    public void doHomeChores()
    {
        //everyday
        setSocial(status.social - 5);
        if (status.social < 0)
        {
            status.social = 0;
        }
        addMoney(3);
        status.nHomeChores += 1;
    }

    public void doBeautySalon()
    {
        //weekdend
        setBeauty(status.beauty + 10);
        reduceMoney(5);
        status.nBeautySalon += 1;
    }

    public void doRest()
    {
        //do nothing WKWKKW
    }

    public void setDate(int n)
    {
        date = n;
    }

    public void addDate()
    {
        date++;
        if (date > 30)
        {
            if (month == 6 || month == 9 || month == 11)
            {
                setDate(1);
                month++;
            }
            
            if (date == 32)
            {
                setDate(1);
                month++;
            }
        }

    }

    public void setResetPoint()
    {
        //Profile
        saveddata.week = week;
        saveddata.day = 1;
        saveddata.money = money;
        saveddata.month = month;
        saveddata.date = date;

        //status
        saveddata.fitness = status.fitness;
        saveddata.social = status.social;
        saveddata.beauty = status.beauty;
        saveddata.confident = status.confident;
        saveddata.happiness = status.happiness;

        //Jumlah activity done
        saveddata.nBasketball = status.nBasketball;
        saveddata.nHangout = status.nHangout;
        saveddata.nBeautySalon = status.nBeautySalon;
        saveddata.nHomeChores = status.nHomeChores;
    }

    public void resetPlayer()
    {
        //Profile
        week = saveddata.week;
        day = saveddata.day;
        money = saveddata.money;
        month = saveddata.month;
        date = saveddata.date;

        //status
        status.fitness = saveddata.fitness;
        status.social = saveddata.social;
        status.beauty = saveddata.beauty;
        status.confident = saveddata.confident;
        status.happiness = saveddata.happiness;

        //Jumlah activity done
        status.nBasketball = saveddata.nBasketball;
        status.nHangout = saveddata.nHangout;
        status.nBeautySalon = saveddata.nBeautySalon;
        status.nHomeChores = saveddata.nHomeChores;
    }

    [System.Serializable]
    public class STATUS
    {
     
        public int fitness, social, beauty, confident, happiness;
        public int nBasketball, nHangout, nHomeChores, nBeautySalon;

        public int basketTrigger, hangoutTrigger, homeChoresTrigger, beautySalonTrigger;

    }

    [System.Serializable]
    public class SAVEDDATA
    {
        public int day;
        public int week;
        public int month, date;
        public int money, weight;
        public int fitness, social, beauty, confident, happiness;
        public int nBasketball, nHangout, nHomeChores, nBeautySalon;

    }
}
