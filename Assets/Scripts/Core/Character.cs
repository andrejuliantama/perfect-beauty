using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Character
{
    public string charName;
    [HideInInspector] public RectTransform root;

    public bool enabled { get { return root.gameObject.activeInHierarchy; } set { root.gameObject.SetActive(value); } }
    
    DialogueSystem dialogue;

    public void Say(string speech, bool add = false)
    {
        if (!enabled)
            enabled = true;
        
        dialogue.Say(speech, charName, add);
		
    }

	//Begin Transitioning Images\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
	public Sprite GetSprite(int index = 0)
	{
		Sprite[] sprites = new Sprite[3];
		int expression = 1;
		for (int i = 0; i < 3; i++)
        {
			sprites[i] = Resources.Load<Sprite>("images/character sprites/" + charName + "/DEFAULT/DEFAULT KEDIP " + expression);
			expression++;
		}
		UnityEngine.Debug.Log(sprites.Length);
		return sprites[index];
	}

	public void SetExpression(int index)
	{
		renderers.expressionRenderer.sprite = GetSprite(index);
	}
	public void SetExpression(Sprite sprite)
	{
		renderers.expressionRenderer.sprite = sprite;
	}

	//Transition Expression
	bool isTransitioningExpression { get { return transitioningExpression != null; } }
	Coroutine transitioningExpression = null;

	public void TransitionExpression(Sprite sprite, float speed, bool smooth)
	{
		if (renderers.expressionRenderer.sprite == sprite)
			return;

		StopTransitioningExpression();
		transitioningExpression = CharacterManager.instance.StartCoroutine(TransitioningExpression(sprite, speed, smooth));
	}

	void StopTransitioningExpression()
	{
		if (isTransitioningExpression)
			CharacterManager.instance.StopCoroutine(transitioningExpression);
		transitioningExpression = null;
	}

	public IEnumerator TransitioningExpression(Sprite sprite, float speed, bool smooth)
	{
		for (int i = 0; i < renderers.allExpressionRenderers.Count; i++)
		{
			Image image = renderers.allExpressionRenderers[i];
			if (image.sprite == sprite)
			{
				renderers.expressionRenderer = image;
				break;
			}
		}

		if (renderers.expressionRenderer.sprite != sprite)
		{
			Image image = GameObject.Instantiate(renderers.expressionRenderer.gameObject, renderers.expressionRenderer.transform.parent).GetComponent<Image>();
			renderers.allExpressionRenderers.Add(image);
			renderers.expressionRenderer = image;
			image.color = GlobalF.SetAlpha(image.color, 0f);
			image.sprite = sprite;
		}

		while (GlobalF.TransitionImages(ref renderers.expressionRenderer, ref renderers.allExpressionRenderers, speed, smooth))
			yield return new WaitForEndOfFrame();

		Debug.Log("done");
		StopTransitioningExpression();
	}


	//End Transition Images\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\


	public Character (string _name, bool enableOnStart = true)
    {
        CharacterManager cm = CharacterManager.instance;
        GameObject prefab = Resources.Load("characters/" + _name + "") as GameObject;
        GameObject ob = GameObject.Instantiate(prefab, cm.characterPanel);
		

		root = ob.GetComponent<RectTransform>();
        charName = _name;

        renderers.expressionRenderer = ob.transform.Find("Default/Kedip/1").GetComponent<Image>();
		renderers.allExpressionRenderers.Add(renderers.expressionRenderer);

		dialogue = DialogueSystem.instance;

        enabled = enableOnStart;
    }

    [System.Serializable]
    public class Renderers
    {
        public Image expressionRenderer;
		public List<Image> allExpressionRenderers = new List<Image>();
	}

    public Renderers renderers = new Renderers();
}
