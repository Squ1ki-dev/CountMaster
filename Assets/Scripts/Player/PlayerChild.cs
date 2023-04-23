using UnityEngine;

public class PlayerChild : MonoBehaviour, IDamageable
{
    Animator anim;
    [HideInInspector] public ParticleSystem dieFx;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        dieFx = GetComponent<ParticleSystem>();
    }
    
    private void OnEnable()
    {
        EventManager.OnStartMovement += MovementChild;
        EventManager.OnStopMovement += StopMoveChild;
    }

    private void OnDisable()
    {
        EventManager.OnStartMovement -= MovementChild;
        EventManager.OnStopMovement -= StopMoveChild;
    }

    private void MovementChild() => anim.SetBool(AnimConst.isRun, true);
    public void StopMoveChild() => anim.SetBool(AnimConst.isRun, false);

    public void TakeDamage()
    {
        ParticleManager.Instance.GetSpawnParticle(Tags.Player, transform.position);
        ObjectPooling.Instance.BackToPool(this.gameObject, Tags.PlayerChild);
    }
}
