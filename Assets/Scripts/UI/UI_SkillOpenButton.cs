using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class UI_SkillOpenButton : UI_Base
{
	enum Texts
	{
		SkillNameText
	}

	enum Buttons
	{
		SkillOpenButton
	}

    CardID _cardID;

	public override bool Init()
	{
        Debug.Log("UI_SkillOpenButton Init");
		if (base.Init() == false)
			return false;

		BindText(typeof(Texts));
		BindButton(typeof(Buttons)); 

        GetText((int)Texts.SkillNameText).text = "skill text";

		GetButton((int)Buttons.SkillOpenButton).gameObject.BindEvent(OnClickSkillButton);
        

		// Managers.Sound.Clear();
		// Managers.Sound.Play(Sound.Effect, "Sound_MainTitle");
		return true;
	}

	void OnClickSkillButton()
	{
		Debug.Log("OnClickSkillButton");
        foreach (CardData CardData in Managers.Data.Cards.Values)
		{
            Debug.Log(CardData.ID+", "+CardData.name.ToString());
		}
	}

}
