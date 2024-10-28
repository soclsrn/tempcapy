using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class UI_CardProfile : UI_Base
{
	enum Texts
	{
		CardNameText
	}

	enum Buttons
	{
		CardUseButton
	}

	[SerializeField]
    int _cardID;

	public override bool Init()
	{
        Debug.Log("UI_CardOpenButton Init");
		if (base.Init() == false)
			return false;

		BindText(typeof(Texts));
		BindButton(typeof(Buttons)); 
		
		foreach (int key in Managers.Data.Cards.Keys){
			Debug.Log("The key is "+key.ToString());
		}
		Debug.Log("The card ID is "+_cardID.ToString());
        GetText((int)Texts.CardNameText).text = Managers.Data.Cards[(int)_cardID].name.ToString();

		GetButton((int)Buttons.CardUseButton).gameObject.BindEvent(OnClickCardButton);
        

		// Managers.Sound.Clear();
		// Managers.Sound.Play(Sound.Effect, "Sound_MainTitle");
		return true;
	}

	void OnClickCardButton()
	{
		Debug.Log("OnClickCardButton");
        Debug.Log(Managers.Data.Cards[(int)_cardID].name.ToString());
		if(Managers.Data.Cards[(int)_cardID].type == 0){
			GameObject.Find("@GameScene").GetComponent<GameScene>().SetTowerPrefab(Managers.Data.Cards[(int)_cardID].path);
		}
	}

}
