using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    private Transform targetTransform;
    private bool zombie_Alive;
    private bool canAttack;
    private float timerAttack;
    private Animator zombie_Animation;
    // Start is called before the first frame update
    void Start()
    {
        zombie_Alive = true;
        zombie_Animation = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (zombie_Alive)
        {
            ZombieMove();
        }
       
        targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void ZombieMove()
    {
        if (targetTransform)
        {
            if (Vector3.Distance(targetTransform.position, transform.position) > 1.5f)
            {
                Move(targetTransform);
                FaceTarget(targetTransform);
            }
            else if (Vector3.Distance(targetTransform.position, transform.position) < 1.5f)
            {
                if (canAttack)
                {
                   
                    timerAttack += Time.deltaTime;
                    if (timerAttack > 0.45f)
                    {
                        timerAttack = 0f;
                        //AudioManager.instance.ZombieAttackSound();
                    }
                }

            }
        }
    }
    
    private float move_Speed = 1f;



    public void Move(Transform targetTransform)
    {
        
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetTransform.position.x, 0, targetTransform.position.z), move_Speed * Time.deltaTime);
    }
    public void FaceTarget(Transform targetTransform)
    {
        transform.LookAt(targetTransform);
    }
}
