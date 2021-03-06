using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour 
{
	public static DialogueSystem instance;

	public ELEMENTS elements;

	string currSpeaker;
	string lastSpeaker = "";
	GameObject child;
	public Player player;
	float typingSpeed = 0.01f;
	public GameObject charPanel;
	public Animator charAnimator;


	void Awake()
	{
		instance = this;
		DontDestroyOnLoad(this.gameObject);
		
	}

	// Use this for initialization
	void Start () 
	{
		
	}

	/// <summary>
	/// Say something and show it on the speech box.
	/// </summary>
	public void Say(string speech, string speaker = "", bool additive = false)
	{
		StopSpeaking();
		

		if (additive)
			speechText.text = targetSpeech;

		if (speaker == "Mila" || speaker == "Satya" || speaker == "Sisca" || speaker == "Rizki" || speaker == "Putra" || speaker == "Dewi")
		{
			/*lastSpeaker = speaker;
			charAnimator = charPanel.transform.Find(speaker + "(Clone)").GetComponent<Animator>();
			charAnimator.SetBool("Speaking", true);
			charAnimator.SetTrigger("StartSpeaking");*/
			
		}

		speaking = StartCoroutine(Speaking(speech, additive, speaker));
	}

	public void StopSpeaking()
	{
		if (isSpeaking)
		{
			StopCoroutine(speaking);
		}
		
		speaking = null;

	}
		
	public bool isSpeaking {get{return speaking != null;}}
	[HideInInspector] public bool isWaitingForUserInput = false;

	public string targetSpeech = "";
	Coroutine speaking = null;
	IEnumerator Speaking(string speech, bool additive, string speaker = "")
	{
		
		speechPanel.SetActive(true);
		targetSpeech = speech;

		if (!additive)
			speechText.text = "";
		else
			targetSpeech = speechText.text + targetSpeech;


		speakerNameText.text = DetermineSpeaker(speaker);//temporary

		isWaitingForUserInput = false;
		
		while (speechText.text != targetSpeech)
		{
			if (speaker == "Mila" || speaker == "Satya" || speaker == "Sisca" || speaker == "Rizki" || speaker == "Putra" || speaker == "Dewi")
			{
				
			}
			speechText.text += targetSpeech[speechText.text.Length];
			yield return new WaitForSeconds(typingSpeed);
		}
		
		//text finished //berenti animating
		/*isWaitingForUserInput = true;
        while (isWaitingForUserInput)
        {
			if (speaker == "Mila" || speaker == "Satya" || speaker == "Sisca" || speaker == "Rizki" || speaker == "Putra" || speaker == "Dewi")
			{
				charAnimator.SetBool("Speaking", false);
			}
			yield return new WaitForSeconds(typingSpeed);
		}*/
			
		
		StopSpeaking();
	}

	string DetermineSpeaker(string s)
	{
		string retVal = speakerNameText.text;//default return is the current name
		if (s != speakerNameText.text && s != "")
			retVal = (s.ToLower().Contains("Narrator")) ? "" : s;
		lastSpeaker = retVal;
		return retVal;
	}

	public void Close()
    {
		StopSpeaking();
		speechPanel.SetActive(false);
    }

	[System.Serializable]
	public class ELEMENTS
	{
		/// <summary>
		/// The main panel containing all dialogue related elements on the UI
		/// </summary>
		public GameObject speechPanel;
		public Text speakerNameText;
		public Text speechText;
		public Button nextButton;
		public GameObject speakerBox;
		
	}
	public GameObject speechPanel {get{return elements.speechPanel;}}
	public Text speakerNameText {get{return elements.speakerNameText;}}
	public Text speechText {get{return elements.speechText;}}
	
}