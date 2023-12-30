using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SkillCardItem : UI_Base
{
	// � ��ų?
	// �� ����?
	// ����Ʈ��Ʈ?
	int _templateID;
	DataXml.SkillData _skillData;

	public void SetInfo(int templateID)
	{
		_templateID = templateID;

		Managers.DataXml.SkillDict.TryGetValue(templateID, out _skillData);
	}

	public void OnClickItem()
	{
		// ��ų ���� ���׷��̵�
		Debug.Log("OnClickItem");
		Managers.UI.ClosePopup();
	}
}
