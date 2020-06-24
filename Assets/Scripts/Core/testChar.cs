using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testChar : MonoBehaviour
{
    public Character Mila;

    public int Expression = 0;

    // Start is called before the first frame update
    void Start()
    {
        Mila = CharacterManager.instance.getCharacter("Mila", enableCreatedCharacterOnStart: false);
        Mila.GetSprite();
    }

    public string[] speech;
    int i = 0;
    
    // Update is called once per frame
    public void Next()
    {
        if (i < speech.Length)
            Mila.Say(speech[i]);
        else
            DialogueSystem.instance.Close();
        i++;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Mila.SetExpression(Expression);
        }
    }
}
