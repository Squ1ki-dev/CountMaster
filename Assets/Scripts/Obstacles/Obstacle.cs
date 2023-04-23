using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour,IAttackable
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamageable damageable))
            Attack(damageable);
    }

    public void Attack(IDamageable damageable)
    {
        GameManager.Instance.DecreasePlayerAmount();
        damageable.TakeDamage();
    }
}
