using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChoiceButton : MonoBehaviour 
{
	public Text choiceText;
	public string text {get{return choiceText.text;} set{choiceText.text = value;}}

	[HideInInspector]
	public int choiceIndex = -1;
}
