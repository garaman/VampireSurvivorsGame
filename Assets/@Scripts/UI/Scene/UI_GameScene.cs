using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class UI_GameScene : UI_Base
{
    [SerializeField] TextMeshProUGUI _killCountText;
    [SerializeField] TextMeshProUGUI _GoldCountText;
    [SerializeField] TextMeshProUGUI _LevelText;
    [SerializeField] Slider _gemSlider;

    bool _timeStop = false;

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

    public void SetLevel(int level)
    {
        _LevelText.text = $"{level}";
    }

    public void GameStop()
    {        
        if (_timeStop)
        {
            Time.timeScale = 1;
            _timeStop = false;
            return;
        }
        Time.timeScale = 0;
        _timeStop = true;
    }
}
