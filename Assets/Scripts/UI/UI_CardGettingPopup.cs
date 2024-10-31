using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Define;

public class UI_CardGettingPopup : UI_Popup
{
	enum Texts
	{
		TitleText
	}

	enum Buttons
	{
		// StartButton,
	}

	public override bool Init()
	{
		if (base.Init() == false)
			return false;

        SetPlayersMove(false);
        BindText(typeof(Texts));
		BindButton(typeof(Buttons)); 

		// 카드 3개 생성
		for (int i = 0; i < 3; i++)
		{
			UI_CardGettingCardProfile card = Managers.UI.MakeSubItem<UI_CardGettingCardProfile>(transform);
			int type = Random.Range(0, Managers.Data.Cards.Count); // 0 ~ 3
			
			// 카드의 이름을 다르게 설정
			card.gameObject.name = $"CardProfile_{type}";
			
			// Setting 메서드 호출
			card.Setting(this, type, (float)i * 0.1f);
			
			// 카드의 위치 조정
			card.transform.position += new Vector3((i - 1) * 800, 0, 0); 
		}

		// 타이틀 텍스트 설정
		GetText((int)Texts.TitleText).text = "카드를 선택하세요.";

		
		return true;
	}


	void OnClickStartButton()
	{
		Debug.Log("OnClickStartButton");
		Managers.Sound.Play(Sound.Effect, "Sound_FolderItemClick");

		// // 데이터 있는지 확인
		// if (Managers.Game.LoadGame())
		// {
		// 	Managers.UI.ShowPopupUI<UI_ConfirmPopup>().SetInfo(() =>
		// 	{
		// 		Managers.Game.Init();
		// 		Managers.Game.SaveGame();

		// 		Managers.UI.ClosePopupUI(this); // UI_TitlePopup
		// 		Managers.UI.ShowPopupUI<UI_NamePopup>();
		// 	}, Managers.GetText(Define.DataResetConfirm));
		// }
		// else
		// {
		// 	Managers.Game.Init();
		// 	Managers.Game.SaveGame();

		// 	Managers.UI.ClosePopupUI(this); // UI_TitlePopup
		// 	Managers.UI.ShowPopupUI<UI_NamePopup>();
		// }		
	}


	
	public void closePopupUI(){
		Managers.UI.ClosePopupUI(this);
	}

	void SetPlayersMove(bool moveable)
	{
        foreach (var p in GameObject.FindGameObjectsWithTag("Player"))
        {
            p.GetComponent<PlayerController>().Cam_setting(moveable);
        }
    }
}
