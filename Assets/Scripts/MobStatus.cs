using UnityEngine;

/// <summary>

/// </summary>
public abstract class MobStatus : MonoBehaviour
{
    /// <summary>

    /// </summary>
    protected enum StateEnum
    {
        Normal, 
        Attack, 
        Die 
    }

    /// <summary>

    /// </summary>
    public bool IsMovable => StateEnum.Normal == _state;

    /// <summary>

    /// </summary>
    public bool IsAttackable => StateEnum.Normal == _state;

    /// <summary>

    /// </summary>
    public float LifeMax => lifeMax;

    /// <summary>

    /// </summary>
    public float Life => _life;

    [SerializeField] private float lifeMax = 10; 
    protected Animator _animator;
    protected StateEnum _state = StateEnum.Normal; 
    private float _life; 

    protected virtual void Start()
    {
        _life = lifeMax; 
        _animator = GetComponentInChildren<Animator>();
    }

    /// <summary>

    /// </summary>
    protected virtual void OnDie()
    {
    }

    /// <summary>

    /// </summary>
    /// <param name="damage"></param>
    public void Damage(int damage)
    {
        if (_state == StateEnum.Die) return;

        _life -= damage;
        if (_life > 0) return;

        _state = StateEnum.Die;
        _animator.SetTrigger("Die");

        OnDie();
    }

    /// <summary>
 
    /// </summary>
    public void GoToAttackStateIfPossible()
    {
        if (!IsAttackable) return;

        _state = StateEnum.Attack;
        _animator.SetTrigger("Attack");
    }

    /// <summary>

    /// </summary>
    public void GoToNormalStateIfPossible()
    {
        if (_state == StateEnum.Die) return;

        _state = StateEnum.Normal;
    }
}