using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueScene : MonoBehaviour
{
    public static DialogueScene instance;
    public GameObject black, classroom, sportsfield, beautysalon, room, roomnight;

    void Awake()
    {
        instance = this;
    }

    public void changeBG(string name)
    {
        switch (name)
        {
            case "Black":
                black.SetActive(true);
                break;
            case "Classroom":
                classroom.SetActive(true);
                break;
            case "SportsField":
                sportsfield.SetActive(true);
                break;
            case "BeautySalon":
                beautysalon.SetActive(true);
                break;
            case "Room":
                room.SetActive(true);
                break;
            case "RoomNight":
                roomnight.SetActive(true);
                break;
        }
    }
}
