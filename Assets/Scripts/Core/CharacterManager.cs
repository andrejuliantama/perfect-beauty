using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.UI;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance;

    public RectTransform characterPanel;

    public List<Character> characters = new List<Character>();

    public Dictionary<string, int> characterDictionary = new Dictionary<string, int>();


    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public Character getCharacter(string charName, bool createCharacterIfDoesNotExist = true, bool enableCreatedCharacterOnStart = true)
    {
        
        int index = -1;
        if (characterDictionary.TryGetValue(charName, out index))
        {
            return characters[index];
        }
        else if (createCharacterIfDoesNotExist)
        {
            return createCharacter(charName, enableCreatedCharacterOnStart);
        }

        return null;
      
    }
    
    public Character createCharacter(string charName, bool enableOnStart = true )
    {
        Character newCharacter = new Character(charName, enableOnStart);
        characterDictionary.Add(charName, characters.Count);
        characters.Add(newCharacter);
        return newCharacter;
    }
    
}
