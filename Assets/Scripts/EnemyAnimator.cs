using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private Animator anim;




    private void Awake()
    {
        anim = GetComponent<Animator>();
    }




   public void Walk(bool Walk)
    {
        anim.SetBool("Walk", Walk);

    }
    public void Run(bool Run)

    {

        anim.SetBool("Run", Run);
    }
    public void Attack()

    {

        anim.SetTrigger("Attack");
    }
    public void Dead()

    {

        anim.SetTrigger("Dead");
    }

    void Update()
    {
        
    }
}
