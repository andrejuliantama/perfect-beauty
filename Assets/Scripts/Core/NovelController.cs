using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using UnityEngine.UI;
using System.Data.Common;

public class NovelController : MonoBehaviour
{
    public static NovelController instance;
    public EVENTBRANCH eventbranch;
    List<string> data = new List<string>();
    int progress = 0;
    public Player player;
    public TextAsset chapterFile;
    public string fileName;
    public Animator charAnimator;

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    void Start()
    {
        fileName = "w" + player.week + "d" + player.day;
        eventbranch.ChapterFile = fileName;
        LoadChapterFile(fileName);
    }

    void Update()
    {
        
    }

    public void Next()
    {
        if (progress < data.Count)
        {
            if (data[progress].Length == 1)
            {
                progress++;
            }
            HandleLine(data[progress]);
            progress++;
        }
        else
        {
            //Application.Quit();
        }

    }

    void LoadChapterFile(string fileName, int startProgress = 0)
    {
        chapterFile = (TextAsset)Resources.Load("story/" + fileName, typeof(TextAsset));
        List<string> lines = new List<string>(chapterFile.text.Split('\n'));
        data = lines;  
        progress = startProgress;
        cachedLastSpeaker = "";
        Next();
    }

    void HandleLine(string line)
    {
        string[] dialogueAndActions = line.Split('"');

        //ada dialog
        if (dialogueAndActions.Length == 3)
        {
            HandleDialogue(dialogueAndActions[0], dialogueAndActions[1]);
            HandleEventsFromLine(dialogueAndActions[2]);
        }
        else
        {
            HandleEventsFromLine(dialogueAndActions[0]);
        }
    }

    string cachedLastSpeaker = "";
    

    void HandleDialogue(string dialogueDetails, string dialogue)
    {
        string speaker = cachedLastSpeaker;
        bool additive = dialogueDetails.Contains("+");
        //speakerBox.SetActive(true);
        DialogueSystem.instance.elements.speakerBox.SetActive(true);
        if (additive)
        {
            dialogueDetails = dialogueDetails.Remove(dialogueDetails.Length - 1);
        }

        if (dialogueDetails.Length > 0)
        {
            if (dialogueDetails[dialogueDetails.Length - 1] == ' ')
                dialogueDetails = dialogueDetails.Remove(dialogueDetails.Length - 1);

            speaker = dialogueDetails;
            
            cachedLastSpeaker = speaker;
        }

        if (speaker != "Narrator" && !speaker.Contains("Student") && speaker != "Player" && !speaker.Contains("Male") && !speaker.Contains("Female") && speaker != "Teacher")
        {

            Character character = CharacterManager.instance.getCharacter(speaker);
            string newDialogue = dialogue;
            if (dialogue.Contains("Player"))
            {
                newDialogue = dialogue.Replace("Player", player.pName);
            }
            character.Say(newDialogue, additive);

        }
        else
        {
            if (speaker == "Narrator")
            {
                //speakerBox.SetActive(false);
                DialogueSystem.instance.elements.speakerBox.SetActive(false);
            }
            if (speaker == "Player")
            {
                speaker = player.pName;
            }
            string newDialogue = dialogue;
            if (dialogue.Contains("Player"))
            {
                newDialogue = dialogue.Replace("Player", player.pName);
            }
            DialogueSystem.instance.Say(newDialogue, speaker, additive);
        }
    }

    void HandleEventsFromLine(string events)
    {
        string[] actions = events.Split(' ');
        foreach (string action in actions)
        {
            HandleAction(action);
        }
    }

    void HandleAction(string action)
    {
        print("Handle action [" + action + "]");
        string[] data = action.Split('(', ')');

        if (data[0] == "setBackground")
        {
            Command_SetLayerImage(data[1], BCFC.instance.background);
            return;
        }

        if (data[0] == "setBG")
        {
            Command_SetBG(data[1]);
            return;
        }

        if (data[0] == "setForeground")
        {
            BCFC.instance.foreground.root.SetActive(true);
            Command_SetLayerImage(data[1], BCFC.instance.foreground);
            return;
        }

        
        if (data[0] == "disableForeground")
        {
            Command_DisableImage(BCFC.instance.foreground);
            return;
        }

        if (data[0] == "setExpression")
        {
            Command_SetExpression(data[1]);
            return;
        }

        if (data[0] == "disableChar")
        {
            Character character = CharacterManager.instance.getCharacter(data[1]);
            character.enabled = false;
            return;
        }
        if (data[0] == "playSound")
        {
            Command_PlaySound(data[1]);
            return;
        }
			
        if (data[0] == "playMusic")
        {
            Command_PlayMusic(data[1]);
            return;
        }
		
        if (data[0] == "makeChoice")
        {
            UnityEngine.Debug.Log(data[1]);
            Command_MakeChoice(data[1]);
            return;
        }

        if (data[0] == "loadChapter")
        {
            LoadChapterFile(data[1]);
            return;
        }

        if (data[0] == "loadScene")
        {
            Command_LoadScene(data[1]);
            return;
        }

        if (data[0] == "nextDay")
        {
            Command_NextDay();
            return;
        }

        if (data[0] == "insertPlayerName")
        {
            Command_SetPlayerName();
            return;
        }

        if (data[0] == "initialChoice")
        {
            Command_SetInitialChoice();
            return;
        }

        if (data[0] == "closeDialogue")
        {
            DialogueSystem.instance.Close();
            return;
        }

        if (data[0] == "setStatus")
        {
            Command_SetStatus(data[1]);
            return;
        }

        if (data[0] == "reloadLastChapterProgress")
        {
            Command_ReloadLastChapterProgress();
            return;
        }

        if (data[0] == "checkEvent")
        {
            eventbranch.lastProgress = progress + 2;
            Command_CheckSpecialEvent();
            return;
        }

        if (data[0] == "reduceWeight")
        {
            Command_ReduceWeight(data[0]);
            return;
        }

        if (data[0] == "retry")
        {
            Command_Retry();
            return;
        }

        if (data[0] == "quit")
        {
            Application.Quit();
            return;
        }
    }

    void Command_SetExpression(string data)
    {
        charAnimator = CharacterManager.instance.characterPanel.gameObject.transform.Find(cachedLastSpeaker + "(Clone)").GetComponent<Animator>();
        charAnimator.SetTrigger(data);
    }
    void Command_SetLayerImage(string data, BCFC.LAYER layer)
    {
        string texName = data.Contains(",") ? data.Split(',')[0] : data;
        print("Current Background" + texName);
        Texture2D tex = Resources.Load("images/UI/BG/" + texName) as Texture2D;
        layer.SetTexture(tex);
    }

    void Command_ReloadLastChapterProgress()
    {
        string lastChapter = eventbranch.ChapterFile;
        int lastProgress = eventbranch.lastProgress;
        LoadChapterFile(lastChapter, lastProgress);
    }

    public void Command_Retry()
    {
        Player.instance.resetPlayer();
        int week = Player.instance.week;
        int day = Player.instance.day;
        string chapterFile = "w" + week + "d" + day;
        LoadChapterFile(chapterFile);
    }
    
    void Command_SetBG(string data)
    {
        string texName = data.Contains(",") ? data.Split(',')[0] : data;
        print("Current BG" + texName);
        //Texture2D tex = Resources.Load("images/UI/BG/" + texName) as Texture2D;
        DialogueScene.instance.changeBG(texName);
    }

    void Command_DisableImage(BCFC.LAYER layer)
    {
        layer.root.SetActive(false);
    }

    void Command_PlaySound(string data)
    {
        AudioClip clip = Resources.Load("audio/SFX/" + data) as AudioClip;

        if (clip != null)
            AudioManager.instance.PlaySFX(clip);
        else
            Debug.LogError("Clip does not exist - " + data);
    }

    void Command_PlayMusic(string data)
    {
        AudioClip clip = Resources.Load("audio/BGM/" + data) as AudioClip;

        AudioManager.instance.PlaySong(clip);
    }

    void Command_SetPlayerName()
    {
        InputNameBox.showInputBox();
        DialogueSystem.instance.elements.nextButton.gameObject.SetActive(false);
        //nextButton.SetActive(false);
    }
    //Initial choice//
    void Command_SetInitialChoice()
    {
        //nextButton.SetActive(false);
        DialogueSystem.instance.elements.nextButton.gameObject.SetActive(false);
        StartCoroutine(InitialChoice());
    }
    public void enableButton()
    {
        //nextButton.SetActive(true);
        DialogueSystem.instance.elements.nextButton.gameObject.SetActive(true);
    }

    IEnumerator InitialChoice()
    {
        string[] choices = { "I'm fat", "I'm lazy and love doing nothing", "I'm ugly", "I'm an emotional person" };
        ChoiceScreen.Show(choices);
        while (ChoiceScreen.isWaitingForChoiceToBeMade)
            yield return new WaitForEndOfFrame();

        switch (ChoiceScreen.lastChoiceMade.index)
        {
            case 0:
                player.addWeight(5);
                break;
            case 1:
                player.setFitness(player.status.fitness - 5);
                break;
            case 2:
                player.setBeauty(player.status.beauty - 5);
                break;
            case 3:
                player.setHappiness(player.status.happiness - 5);
                break;
        }
        yield return new WaitForEndOfFrame();
    }
    //////////////////////////////

    //Choice
    void Command_MakeChoice(string data)
    {
        string newData = data.Replace("=", " ");
        string[] choices = newData.Split(':');
        //nextButton.SetActive(false);
        StartCoroutine(makeChoice(choices));

    }
    IEnumerator makeChoice(string[] choices)
    {
        int week = player.week;
        int day = player.day;
        string chapterName = fileName;
        if (chapterName == "w1d1BeforeActivity")
        {
            chapterName = "w1d1";
        }
        print("PILIHAN LAMAA" + ChoiceScreen.lastChoiceMade.index);
        ChoiceScreen.Show(choices);
        bool wait = true;
        while (wait)
        {
            yield return new WaitForEndOfFrame();
            print("PILIHAN BARUU" + ChoiceScreen.lastChoiceMade.index);
            
            switch (ChoiceScreen.lastChoiceMade.index)
            {
                case 0:
                    fileName = chapterName + "c1";
                    LoadChapterFile(fileName);
                    wait = false;
                    break;
                case 1:
                    fileName = chapterName + "c2";
                    LoadChapterFile(fileName);
                    wait = false;
                    break;
            }
            
        }
        print("KELUAR DARI WHILE" + ChoiceScreen.lastChoiceMade.index);

        //BENERIN COY/////////////////////////////////////////////////////////////////////////////////////////////////////



    }

    void Command_LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
        //DontDestroyOnLoad(player);

    }

    void Command_NextDay()
    {
        if (player.week == 0)
        {
            player.addWeek();
        }
        player.addDay();
        fileName = "w" + player.week + "d" + player.day;
    }

    void Command_SetStatus(string data)
    {
        string[] s = data.Split('[', ']');
        string status = s[0];
        int n = Int32.Parse(s[1]);

        switch (status)
        {
            case "happiness":
                int happiness = player.status.happiness;
                Player.instance.setHappiness(happiness + n);
                break;
            case "confident":
                int confident = player.status.confident;
                Player.instance.setConfident(confident + n);
                break;
        }
    }

    void Command_ReduceWeight(string data)
    {
        int n = Int32.Parse(data);
        Player.instance.weight -= n;
    }

    void Command_CheckSpecialEvent()
    {
        eventbranch.ChapterFile = fileName;
        int basket = player.status.nBasketball;
        int hangout = player.status.nHangout;
        int beautysalon = player.status.nBeautySalon;
        int homechores = player.status.nHomeChores;

        if (basket == 1 && eventbranch.basket1 == false)
        {
            fileName = "basket1";
            eventbranch.basket1 = true;
            LoadChapterFile(fileName);
            return;
        }

        if (basket == 3 && eventbranch.basket2 == false)
        {
            fileName = "basket2";
            eventbranch.basket2 = true;
            LoadChapterFile(fileName);
            return;
        }

        if (basket == 5 && eventbranch.basket3 == false)
        {
            fileName = "basket3";
            eventbranch.basket3 = true;
            LoadChapterFile(fileName);
            return;
        }

        if (hangout == 1 && eventbranch.hangout1 == false)
        {
            fileName = "hangout1";
            eventbranch.hangout1 = true;
            LoadChapterFile(fileName);
            return;
        }

        if (hangout == 3 && eventbranch.hangout2 == false)
        {
            fileName = "hangout2";
            eventbranch.hangout2 = true;
            LoadChapterFile(fileName);
            return;
        }

        if (hangout == 5 && eventbranch.hangout3 == false)
        {
            fileName = "hangout3";
            eventbranch.hangout3 = true;
            LoadChapterFile(fileName);
            return;
        }
        progress += 2;
        HandleLine(data[progress]);
    }

    [System.Serializable]
    public class EVENTBRANCH
    {
        public string ChapterFile;
        public int lastProgress;
        public bool basket1 = false, basket2 = false, basket3 = false, basket4 = false, basket5 = false;
        public bool hangout1 = false, hangout2 = false, hangout3 = false, hangout4 = false, hangout5 = false;
        public bool beautysalon1 = false, beautysalon2 = false, beautysalon3 = false, beautysalon4 = false, beautysalon5 = false;
        public bool homechores1 = false, homechores2 = false, homechores3 = false, homechores4 = false, homechores5 = false;
    }
}
