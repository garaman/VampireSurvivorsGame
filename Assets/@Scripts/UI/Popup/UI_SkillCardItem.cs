using DataXml;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_SkillCardItem : UI_Base
{
    [SerializeField] TextMeshProUGUI _cardNameText;
    [SerializeField] Image _skillImage;
    [SerializeField] TextMeshProUGUI _skillDescriptionText;    
    [SerializeField] GameObject[] _starOn;

    int _templateID;
	DataXml.SkillData _skillData;	
    

    public override bool Init()
    {
        base.Init();

        foreach (var star in _starOn) 
        { 
            star.gameObject.SetActive(false); 
        }

        return true;
    }

    public void SetInfo(int templateID)
	{
		_templateID = templateID;

		Managers.DataXml.SkillDict.TryGetValue(_templateID, out _skillData);

        _cardNameText.text = _skillData.name;        
        _skillDescriptionText.text = _skillData.name;

        Sprite sprite = Managers.Resource.Load<Sprite>(_skillData.sprite);
        _skillImage.sprite = sprite;

        for (int i = 0; i < _skillData.level+1; i++)
        {
            _starOn[i].gameObject.SetActive(true);
        }
        /*
        switch (_templateID)
        {
            case (int)Define.FIRE_BALL_ID:
                _skillImage.color = Color.red;
                break;
            case (int)Define.EGO_SWORD_ID:
                _skillImage.color = Color.blue;
                break;
            case (int)Define.WIND_CUTTER_ID:
                _skillImage.color = Color.green;
                break;
        }
        */
    }


    public void OnClickItem()
	{
		// 스킬 레벨 업그레이드
        if(_skillData.level == 0)
        {
            switch (_templateID)
            {
                case (int)Define.FIRE_BALL_ID:
                    Managers.Game.Player.Skills.AddSkill<FireballSkill>(Managers.Game.Player.Indicator.position);
                    break;
                case (int)Define.EGO_SWORD_ID:
                    Managers.Game.Player.Skills.AddSkill<EgoSword>(Managers.Game.Player.FireSocket);
                    break;
                case (int)Define.WIND_CUTTER_ID:
                    Managers.Game.Player.Skills.AddSkill<WindCutter>(Managers.Game.Player.Indicator.position);
                    break;
            }
        }        
        Managers.DataXml.SkillDict[_templateID].level++;        

        Managers.UI.ClosePopup();
	}
}
