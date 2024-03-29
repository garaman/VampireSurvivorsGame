using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance;
    static bool s_init = false;

    #region Contents    
    GameManager _game = new GameManager();
    ObjectManager _object = new ObjectManager();
    PoolManager _pool = new PoolManager();
    ShopManager _shop = new ShopManager();    
    public static GameManager Game { get { return Instance?._game; } }
    public static ObjectManager Object { get { return Instance?._object; } }
    public static PoolManager Pool { get { return Instance?._pool; } }
    public static ShopManager Shop { get { return Instance?._shop; } }
    #endregion

    #region Core
    DataManager _data = new DataManager();
    DataManagerXml _dataXml = new DataManagerXml();
    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();
    SoundManager _sound = new SoundManager();
    UIManager _ui = new UIManager();
    public static DataManager Data { get { return Instance?._data; } }
    public static DataManagerXml DataXml { get { return Instance?._dataXml; } }
    public static ResourceManager Resource { get { return Instance?._resource; } }
    public static SceneManagerEx Scene { get { return Instance?._scene; } }
    public static SoundManager Sound { get { return Instance?._sound; } }
    public static UIManager UI { get { return Instance?._ui; } }
    #endregion


    public static Managers Instance
    {
        get 
        { 
            if(s_init == false)
            {
                s_init = true;

                GameObject go = GameObject.Find("@Managers");
                if(go == null) 
                {
                    go = new GameObject { name = "@Managers" };
                    go.AddComponent<Managers>();
                }

                DontDestroyOnLoad(go);
                s_instance = go.GetComponent<Managers>();
            }
            return s_instance;
        }
    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
