using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class InputNameBox: MonoBehaviour
{
    public static InputNameBox instance;
    public string PlayerName;
    public Text InputName;
    //public Animator animator;
    public Player player;
    public GameObject InputBox;

    void Awake()
    {
        instance = this;
        closeInputBox();
    }

    public void SubmitName()
    {
        PlayerName = InputName.GetComponent<Text>().text;
        player.createPlayer(PlayerName);
        UnityEngine.Debug.Log(player.pName);
        closeInputBox();
    }

    public static void showInputBox()
    {
        instance.InputBox.SetActive(true);
    }

    public static void closeInputBox()
    {
        instance.InputBox.SetActive(false);
    }
}
