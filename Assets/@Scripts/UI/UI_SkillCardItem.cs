using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SkillCardItem : UI_Base
{
	// 어떤 스킬?
	// 몇 레벨?
	// 데이트시트?
	int _templateID;
	DataXml.SkillData _skillData;

	public void SetInfo(int templateID)
	{
		_templateID = templateID;

		Managers.DataXml.SkillDict.TryGetValue(templateID, out _skillData);
	}

	public void OnClickItem()
	{
		// 스킬 레벨 업그레이드
		Debug.Log("OnClickItem");
		Managers.UI.ClosePopup();
	}
}
