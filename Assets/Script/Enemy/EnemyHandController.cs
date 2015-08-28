using UnityEngine;
using System.Collections;

public class EnemyHandController : MonoBehaviour
{
    public Animator anim;

    public void RunHand()
    {
        anim.SetTrigger("Run");
    }

    public void DanoHand()
    {
        anim.SetTrigger("Dano");
    }

    public void DefesaHand()
    {
        anim.SetTrigger("Defesa");
    }

    public void PAttackHand()
    {
        anim.SetTrigger("Pattack");
    }

    public void SprawlHand()
    {
        anim.SetTrigger("Sprawl");
    }

    public void KillHand()
    {
        anim.SetTrigger("Kill");
    }

    public void AttackHand()
    {
        anim.SetTrigger("Attack");
    }

    public void IdleHand()
    {
        anim.SetTrigger("Idle");
    }
}