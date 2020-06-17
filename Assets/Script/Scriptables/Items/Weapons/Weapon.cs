using UnityEngine;

public enum AttackMode
{
    /// <summary>
    /// Attacks Continiously
    /// </summary>
    CONTINIOUS,
    /// <summary>
    /// Attacks Once
    /// </summary>
    SINGULAR
}

public abstract class Weapon : Item
{
    [Space]
    [Tooltip("Weapon Attack Mode")]
    [SerializeField] AttackMode attackMode;

    public AttackMode AttackMode
    {
        get
        {
            return attackMode;
        }

        private set
        {
            attackMode = value;
        }
    }

    public abstract void Attack(Character attacker);
}
      
