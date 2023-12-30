using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class UI_GameScene : UI_Base
{
    [SerializeField] TextMeshProUGUI _killCountText;
    [SerializeField] TextMeshProUGUI _GoldCountText;
    [SerializeField] Slider _gemSlider;

    private void Awake()
    {
        _gemSlider.value = 0;
    }


    public void SetGemCountRatio(float ratio)
    {
        _gemSlider.value = ratio;
    }

    public void SetKillCount(int killCount)
    {
        _killCountText.text = $"{killCount}";
    }

    public void SetGoldCount(int GoldCountText)
    {
        _GoldCountText.text = $"{GoldCountText}";
    }
}
