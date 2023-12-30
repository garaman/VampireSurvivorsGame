using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectManager 
{
    public PlayerController Player {  get; private set; }
    public HashSet<MonsterController> Monsters { get; } = new HashSet<MonsterController>();
    public HashSet<ProjectileController> Projectiles { get; } = new HashSet<ProjectileController>();
    public HashSet<JamController> Jams { get; } = new HashSet<JamController>();
    public HashSet<GoldController> Golds { get; } = new HashSet<GoldController>();

    public T Spawn<T>(Vector3 position, int templateID = 0) where T : BaseController
    {
        System.Type type = typeof(T);

        if(type == typeof(PlayerController))
        {
            GameObject go = Managers.Resource.Instantiate("Slime_01.prefab", pooling: false);
            go.name = "Player";
            go.transform.position = position;

            PlayerController pc = go.GetOrAddComponent<PlayerController>();
            Player = pc;

            return pc as T;
        }
        else if (type == typeof(MonsterController))
        {
            string name = (templateID == 0 ? "Goblin_01" : "Snake_01");
            GameObject go = Managers.Resource.Instantiate(name + ".prefab" , pooling: true);
            go.transform.position = position;

            MonsterController mc = go.GetOrAddComponent<MonsterController>();
            Monsters.Add(mc);

            return mc as T;
        }
        else if (type == typeof(ProjectileController))
        {
            GameObject go = Managers.Resource.Instantiate("FireProjectile.prefab", pooling: true);
            go.transform.position = position;

            ProjectileController pc = go.GetOrAddComponent<ProjectileController>();
            Projectiles.Add(pc);
            pc.Init();

            return pc as T;
        }
        else if(type == typeof(JamController)) 
        {
            GameObject go = Managers.Resource.Instantiate(Define.EXP_JAM_PREFAB, pooling: true);
            go.transform.position= position;

            JamController jc = go.GetOrAddComponent<JamController>();
            Jams.Add(jc);
            jc.Init();

            string key = UnityEngine.Random.Range(0, 2) == 0 ? "EXPJam_01.sprite" : "EXPJam_02.sprite";
            Sprite sprite = Managers.Resource.Load<Sprite>(key);            
            go.GetComponent<SpriteRenderer>().sprite = sprite;

            GameObject.Find("@Grid").GetComponent<GridCell>().Add(go);

            return jc as T;
        }
        else if (type == typeof(GoldController))
        {
            GameObject go = Managers.Resource.Instantiate(Define.GOLD_PREFAB, pooling: true);
            go.transform.position = position;

            GoldController gc = go.GetOrAddComponent<GoldController>();
            Golds.Add(gc);
            gc.Init();

            GameObject.Find("@Grid").GetComponent<GridCell>().Add(go);

            return gc as T;
        }
        else if (typeof(T).IsSubclassOf(typeof(SkillController)))
        {
            if (Managers.DataXml.SkillDict.TryGetValue(templateID, out DataXml.SkillData skillData) == false)
            {
                Debug.LogError($"ObjectManager Spawn Skill Failed {templateID}");
                return null;
            }

            GameObject go = Managers.Resource.Instantiate(skillData.prefab, pooling: true);
            go.transform.position = position;

            T t = go.GetOrAddComponent<T>();
            t.Init();

            return t;
        }
        return null;
    }

    public void Despawn<T>(T obj) where T : BaseController
    {
        if(obj.IsVaild() == false) { return; }

        System.Type type = typeof(T);

        if (type == typeof(PlayerController))
        {
            //?
        }
        else if (type == typeof(MonsterController))
        {           
            Monsters.Remove(obj as MonsterController);
            Managers.Resource.Destroy(obj.gameObject);
        }
        else if (type == typeof(ProjectileController))
        {
            Projectiles.Remove(obj as ProjectileController);
            Managers.Resource.Destroy(obj.gameObject);
        }
        else if (type == typeof(JamController))
        {
            Jams.Remove(obj as JamController);
            Managers.Resource.Destroy(obj.gameObject);

            GameObject.Find("@Grid").GetComponent<GridCell>().Remove(obj.gameObject);
        }
        else if (type == typeof(GoldController))
        {
            Golds.Remove(obj as GoldController);
            Managers.Resource.Destroy(obj.gameObject);

            GameObject.Find("@Grid").GetComponent<GridCell>().Remove(obj.gameObject);
        }
    }
}
