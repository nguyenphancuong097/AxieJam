
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class EnemyAttack : EnemyComponent
{
    float timeAttack;
    float coolDown;
    Player target;
    public override void OnInits(Character enemy)
    {
        base.OnInits(enemy);
        timeAttack = 0;
        coolDown = 1f / control.stat.attackSpeed;
    }


    public override void OnUpdate(float dt)
    {
        if (!control) return;
        base.OnUpdate(dt);

        if (target && Time.time - timeAttack >= coolDown)
        {
            OnAttack();
        }
    }
    public virtual void OnAttack()
    {
        timeAttack = Time.time;
        Attacktarget();
    }

    public virtual void Attacktarget()
    {
        if (!target) return;
    
        float dodge = target.stat.dodge;

        if (Random.value <= dodge)
        {
            SpawnText();
            return;

        }
        float damage = control.stat.damage;
        bool isCrit = Random.value <= control.stat.critRate;
        if (isCrit)
            damage += damage * control.stat.critDamage;
        target.GetPCom<PlayerHp>().TakeDamage(damage, isCrit);
    }
    public override void OnDead()
    {
        base.OnDead();
        target = null;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!target)
            target = collision.GetComponent<Player>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (target && target == collision.GetComponent<Player>())
        {
            target = null;
        }
    }



    private void SpawnText()
    {
        float height = GameManager.Instance.gameConfig.textHeight;
        var dd = PoolManager.Instance.SpawnObject(PoolType.TextDisplay).GetComponent<TextDisplay>();
        dd.ShowMiss();
        //  dd.transform.SetParent(GameManager.Instance.gameConfig.textParent);
        dd.transform.position = transform.position + height * Vector3.up;
        dd.transform.DOMoveY(dd.transform.position.y + height, 0.5f).OnComplete(() =>
        {
            PoolManager.Instance.DespawnObject(dd.transform);
        });
    }
}
