using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using Unity.VisualScripting;

public class UI_LobbyScene : UI_Base
{
    enum Buttons
    {
        ShopButton,
        StartButton,
    }

    public override bool Init()
    {
        if (base.Init() == false)
            return false;

        BindButton(typeof(Buttons));

        GetButton((int)Buttons.ShopButton).gameObject.BindEvent(OnClickShopButton);
        GetButton((int)Buttons.StartButton).gameObject.BindEvent(OnClickStartButton);

        return true;
    }

    void OnClickShopButton()
    {
        Debug.Log("Shop");
    }

    void OnClickStartButton()
    {
        Debug.Log("Start");
        //Destroy(gameObject);
        
    }
}
