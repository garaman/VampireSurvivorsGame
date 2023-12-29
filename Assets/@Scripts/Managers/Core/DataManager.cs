using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface ILoader<key, Value>
{
    Dictionary<key, Value> MakeDict();
}

public class DataManager 
{
    public Dictionary<int,Data.PlayerData> PlayerDict { get; private set; } = new Dictionary<int, Data.PlayerData>();
    public void Init()
    {
        PlayerDict = LoadJson<Data.PlayerDataLoader, int, Data.PlayerData>("PlayerData.json").MakeDict();
    }

    Loader LoadJson<Loader, key, Value>(string path) where Loader : ILoader<key, Value>
    {
        TextAsset textAsset = Managers.Resource.Load<TextAsset>($"{path}");
        return JsonUtility.FromJson<Loader>(textAsset.text);
    }
    
}
