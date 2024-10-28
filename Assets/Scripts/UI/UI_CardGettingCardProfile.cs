using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening; // DoTween 네임스페이스 추가
using static Define;

public class UI_CardGettingCardProfile : UI_Base
{
    enum Texts
    {
        CardTitleText
    }

    enum Buttons
    {
        CardUseButton
    }

    [SerializeField]
    int _cardID;
    
    UI_CardGettingPopup _parent;
    
    public void Setting(UI_CardGettingPopup parent, int cardID, float delay)
    {
        Init();
        
        _parent = parent;
        _cardID = cardID;
        
        RefreshUI();

        // delay 시간만큼 대기한 후 애니메이션 실행
		Debug.Log("anim: " + delay);
        Invoke("Animate", delay);
    }

    public override bool Init()
    {
        Debug.Log("UI_CardOpenButton Init");
        if (base.Init() == false)
            return false;

        BindText(typeof(Texts));
        BindButton(typeof(Buttons)); 
        
        foreach (int key in Managers.Data.Cards.Keys)
        {
            Debug.Log("The key is " + key.ToString());
        }
        Debug.Log("The card ID is " + _cardID.ToString());
        GetText((int)Texts.CardTitleText).text = Managers.Data.Cards[(int)_cardID].name.ToString();

        GetButton((int)Buttons.CardUseButton).gameObject.BindEvent(OnClickCardButton);

        return true;
    }

    void RefreshUI()
    {
        GetText((int)Texts.CardTitleText).text = Managers.Data.Cards[(int)_cardID].name.ToString();
        GetButton((int)Buttons.CardUseButton).gameObject.BindEvent(OnClickCardButton);
    }

    void OnClickCardButton()
    {
        Debug.Log("OnClickCardButton");
        Debug.Log(Managers.Data.Cards[(int)_cardID].name.ToString());
        if (Managers.Data.Cards[(int)_cardID].type == 0)
        {
            GameObject.Find("@GameScene").GetComponent<GameScene>().SetTowerPrefab(Managers.Data.Cards[(int)_cardID].path);
        }
        // 부모에서 자신을 제거
        transform.SetParent(GameObject.Find("UI_GamePopup").transform);

        // 0.5초 뒤에 자신을 제거
        Destroy(gameObject, 0.5f);
		AnimateUI();

        _parent.closePopupUI();
    }

    // 애니메이션 실행
    void Animate()
    {
        // 버튼에 DoTween 애니메이션 적용 (크기를 1.2배로 확대했다가 원래 크기로 돌아오게 하는 예시)
        GetButton((int)Buttons.CardUseButton).GetComponent<DOTweenAnimation>().DOPlay();
    }

	public void AnimateUI()
    {
		// float scaleUpDuration = 0.1f; // 크기가 커지는 시간
		float scaleDownDuration = 0.5f; // 크기가 작아지는 시간
		// float fadeDuration = 0.3f; // 사라지는 시간
		
        // UI 요소를 원래 크기보다 커지게 함
        transform.DOScale(Vector3.zero, scaleDownDuration).SetEase(Ease.InQuad);
                    // .OnComplete(() =>
                    // {
                    //     // 크기가 작아진 후 투명하게 사라지게 함
                    //     GetComponent<Image>().DOFade(0, fadeDuration)
                    //         .OnComplete(() =>
                    //         {
                    //             // 애니메이션이 끝난 후 UI 요소를 비활성화할 수 있음
                    //             this.gameObject.SetActive(false);
                    //         });
                    // });
    }
}
