using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IAttackable
{
    EnemyArea enemyScript;

    Collider enemyColl;

    SkinnedMeshRenderer mesh;

    private void Awake()
    {
        enemyScript = GetComponentInParent<EnemyArea>();
        enemyColl = GetComponent<CapsuleCollider>();
        mesh = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
        {
            GetComponent<IAttackable>().Attack(damageable);

            GameManager.Instance.DecreasePlayerAmount();

            enemyScript.AllEnemiesDied();
            
            enemyColl.enabled = false;
            mesh.enabled = false;

        }
    }

    public void Attack(IDamageable damageable)
    {
        damageable.TakeDamage();

        ParticleManager.Instance.GetSpawnParticle(Tags.Enemy, transform.position);

        Destroy(this);
    }

    public void StartMove()
    {
        GetComponent<NavMeshAgent>().speed = 15;
        GetComponent<Animator>().SetBool(AnimConst.enemyRun, true);
    }
    
    IEnumerator DeathPlayers(GameObject go)
    {
        yield return new WaitForSecondsRealtime(0.5f);
        ObjectPooling.Instance.BackToPool(go, Tags.PlayerChild);
        gameObject.SetActive(false);
    }
}
