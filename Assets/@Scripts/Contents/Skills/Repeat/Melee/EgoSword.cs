using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EgoSword : RepeatSkill
{
    [SerializeField] ParticleSystem[] _swingParticles;

    protected enum SwingType
    {
        First,
        Second,
        Third,
        Fourth
    }

	public EgoSword()
	{
		
	}

	protected override IEnumerator CoStartSkill()
	{
		WaitForSeconds wait = new WaitForSeconds(CoolTime);

		while (true)
		{
			SetParticles(SwingType.First);
			_swingParticles[(int)SwingType.First].gameObject.SetActive(true);
			yield return new WaitForSeconds(_swingParticles[(int)SwingType.First].main.duration);
            
			if(SkillData.level > (int)SwingType.First)
			{
                SetParticles(SwingType.Second);
                _swingParticles[(int)SwingType.Second].gameObject.SetActive(true);
                yield return new WaitForSeconds(_swingParticles[(int)SwingType.Second].main.duration);
            }
			if (SkillData.level > (int)SwingType.Second)
			{
                SetParticles(SwingType.Third);
                _swingParticles[(int)SwingType.Third].gameObject.SetActive(true);
                yield return new WaitForSeconds(_swingParticles[(int)SwingType.Third].main.duration);
            }

			if (SkillData.level > (int)SwingType.Third)
			{
				SetParticles(SwingType.Fourth);
				_swingParticles[(int)SwingType.Fourth].gameObject.SetActive(true);
				yield return new WaitForSeconds(_swingParticles[(int)SwingType.Fourth].main.duration);
			}
            
            yield return wait;
		}
	}

	public override bool Init()
	{
		base.Init();
		SetInfo(Define.EGO_SWORD_ID);
		Damage = SkillData.damage;
		SkillLevel = SkillData.level;

        return true;
	}

    public void SetInfo(int templateID)
    {
        if (Managers.DataXml.SkillDict.TryGetValue(templateID, out DataXml.SkillData data) == false)
        {
            Debug.LogError("EgoSword SetInfo Failed");
            return;
        }              
        SkillData = data;
    }

    void SetParticles(SwingType swingType)
	{
		if (Managers.Game.Player == null)
			return;

		Vector3 tempAngle = Managers.Game.Player.Indicator.transform.eulerAngles;
		transform.localEulerAngles = tempAngle;
		transform.position = Managers.Game.Player.transform.position;

		float radian = Mathf.Deg2Rad * tempAngle.z * -1;

		var main = _swingParticles[(int)swingType].main;
		main.startRotation = radian;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {       
        MonsterController mc = collision.transform.GetComponent<MonsterController>();
        if (mc.IsValid() == false)
            return;
        
        mc.OnDamaged(Managers.Game.Player, Damage);       
    }



    protected override void DoSkillJob()
	{

	}
}
