using System.Collections;
using UnityEngine;

/// <summary>

/// </summary>
[RequireComponent(typeof(MobStatus))]
public class MobAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown = 0.5f; 
    [SerializeField] private Collider attackCollider;
    
    private MobStatus _status;

    private void Start()
    {
        _status = GetComponent<MobStatus>();
    }

    /// <summary>
    /// 攻撃可能な状態であれば攻撃を行います。
    /// </summary>
    public void AttackIfPossible()
    {
        if (!_status.IsAttackable) return; 

        _status.GoToAttackStateIfPossible();
    }

    /// <summary>
    /// </summary>
    /// <param name="collider"></param>
    public void OnAttackRangeEnter(Collider collider)
    {
        AttackIfPossible();
    }

    /// <summary>

    /// </summary>
    public void OnAttackStart()
    {
        attackCollider.enabled = true;
    }
    
    /// <summary>

    /// </summary>
    /// <param name="collider"></param>
    public void OnHitAttack(Collider collider)
    {
        var targetMob = collider.GetComponent<MobStatus>();
        if (null == targetMob) return;
        

        targetMob.Damage(1);
    }
    
    /// <summary>

    /// </summary>
    public void OnAttackFinished()
    {
        attackCollider.enabled = false;
        StartCoroutine(CooldownCoroutine());
    }

    private IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        _status.GoToNormalStateIfPossible();        
    }
}