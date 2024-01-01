using System.Collections.Generic;
using UnityEngine;

public class SkillBook : MonoBehaviour
{
	// 일종의 스킬 매니저
	public List<SkillBase> Skills { get; } = new List<SkillBase>();
	public List<SkillBase> RepeatedSkills { get; } = new List<SkillBase>();
	// 프리팹 만들까?
	public List<SequenceSkill> SequenceSkills { get; } = new List<SequenceSkill>();
    
    public T AddSkill<T>(Vector3 position, Transform parent = null) where T : SkillBase
    {
        System.Type type = typeof(T);

		if (type == typeof(EgoSword))
        {
			var egoSword = Managers.Object.Spawn<EgoSword>(position, Define.EGO_SWORD_ID);
			egoSword.transform.SetParent(parent);
			egoSword.ActivateSkill();

			Skills.Add(egoSword);
			RepeatedSkills.Add(egoSword);
			Debug.Log("Add EgoSword");
			return egoSword as T;
		}
        else if (type == typeof(FireballSkill))
        {
			var fireball = Managers.Object.Spawn<FireballSkill>(position, Define.FIRE_BALL_ID);
            fireball.transform.SetParent(parent);
			fireball.ActivateSkill();
            fireball.transform.localScale = Vector3.zero;

            Skills.Add(fireball);
			RepeatedSkills.Add(fireball);
            Debug.Log("Add FireballSkill");
            return fireball as T;
		}
        else if (type == typeof(WindCutter))
        {
            var windcutter = Managers.Object.Spawn<WindCutter>(position, Define.WIND_CUTTER_ID);
            windcutter.transform.SetParent(parent);
            windcutter.ActivateSkill();
			windcutter.transform.localScale = Vector3.zero;
            
            Skills.Add(windcutter);
            RepeatedSkills.Add(windcutter);
            Debug.Log("Add WindCutter");
            return windcutter as T;
        }
        else if (type.IsSubclassOf(typeof(SequenceSkill)))
		{
			var skill = gameObject.GetOrAddComponent<T>();
			Skills.Add(skill); 
			SequenceSkills.Add(skill as SequenceSkill);

			return null;
		}

		return null;
    }

	int _sequenceIndex = 0;

	public void StartNextSequenceSkill()
	{
		if (_stopped)
			return;
		if (SequenceSkills.Count == 0)
			return;
		
		SequenceSkills[_sequenceIndex].DoSkill(OnFinishedSequenceSkill);
	}

	void OnFinishedSequenceSkill()
	{
		_sequenceIndex = (_sequenceIndex + 1) % SequenceSkills.Count;
		StartNextSequenceSkill();
	}

	bool _stopped = false;

	public void StopSkills()
	{
		_stopped = true;

		foreach (var skill in RepeatedSkills)
		{
			skill.StopAllCoroutines();
		}
	}
}
