using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;


public class DataManagerXml
{
    public Dictionary<int, DataXml.PlayerData> PlayerDict { get; private set; } = new Dictionary<int, DataXml.PlayerData>();
    public Dictionary<int, DataXml.SkillData> SkillDict { get; private set; } = new Dictionary<int, DataXml.SkillData>();
    public void Init()
    {
        PlayerDict = LoadXml<DataXml.PlayerDataLoader, int, DataXml.PlayerData>("PlayerData.xml").MakeDict();
        SkillDict = LoadXml<DataXml.SkillDataLoader, int, DataXml.SkillData>("SkillData.xml").MakeDict();        
    }

    Loader LoadXml<Loader, key, Value>(string name) where Loader : ILoader<key, Value> , new()
    {
        XmlSerializer xs = new XmlSerializer(typeof(Loader));
        TextAsset textAsset = Managers.Resource.Load<TextAsset>(name);
        using (MemoryStream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(textAsset.text)))
        {
            return (Loader)xs.Deserialize(stream);
        }        
    }

    

}


