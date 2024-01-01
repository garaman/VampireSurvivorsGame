using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_SkillSelectPopup : UI_Base
{
    [SerializeField] Transform _grid;
    [SerializeField] TextMeshProUGUI _beforeLevelValueText;
    [SerializeField] TextMeshProUGUI _afterLevelValueText;

    List<UI_SkillCardItem> _items = new List<UI_SkillCardItem>();

    public override bool Init()
    {
        base.Init();
        PopuplateGrid();

        SetLevel(1);

        return true;
	}

    void PopuplateGrid()
    {
        foreach (Transform t in _grid.transform)
            Managers.Resource.Destroy(t.gameObject);

        for (int i = 0; i < 3; i++)
        {
            var go = Managers.Resource.Instantiate("UI_SkillCardItem.prefab", pooling: false);
            UI_SkillCardItem item = go.GetOrAddComponent<UI_SkillCardItem>();

            item.transform.SetParent(_grid.transform);

            item.SetInfo(i+1);

            _items.Add(item);
        }
    }

    public void SetLevel(int Level)
    {
        _beforeLevelValueText.text = $"{Level}";
        _afterLevelValueText.text = $"{Level + 1}";
    }
}
