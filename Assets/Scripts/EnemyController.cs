using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public enum EnemyState
{
    PATROL,
    CHASE,
    ATTACK
}
public class EnemyController : MonoBehaviour
{
    private EnemyAnimator enemyAnim;
    private NavMeshAgent navAgent;
    private EnemyState enemyState;
    public float _walkSpeed = 0.5f;
    public float _runSpeed = 4f;

    public float _chaseDistance = 7f;
    private float currentChaseDistance;
    public float _attackDistance = 2.2f;
    public float _chaseAfterAttack = 2f;

    public float _patrolRadiusMin = 20f, _patrolRadiusMax = 60f;
    public float _patrolForTime = 15f;
    private float patrolTimer;

    public float _waitBeforeAttack = 2f;
    private float attackTimer;

    private Transform target;
    public GameObject _attackPoint;
    private void Awake()
    {
        enemyAnim = GetComponent<EnemyAnimator>();
        navAgent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag(Tags.PLAYER_TAG).transform;
    }
    void Start()
    {
        enemyState = EnemyState.PATROL;

        patrolTimer = _patrolForTime;

        attackTimer = _waitBeforeAttack;

        currentChaseDistance = _chaseDistance;

    }

    // Update is called once per frame
    void Update()
    {
        if (enemyState == EnemyState.PATROL)
        {
            Patrol();
        }
        if (enemyState == EnemyState.CHASE)
        {
            Chase();
        }
        if (enemyState == EnemyState.ATTACK)
        {
            Attack();
        }
        
    }
    void Patrol()
    {
        navAgent.isStopped = false;
        navAgent.speed = _walkSpeed;
        patrolTimer += Time.deltaTime;
        if (patrolTimer > _patrolForTime)
        {
            SetNewRandomDestination();
            patrolTimer = 0f;
        }
        if (navAgent.velocity.sqrMagnitude > 0)
        {
            enemyAnim.Walk(true);
        }
        else
        {
            enemyAnim.Walk(false);
        }
        if (Vector3.Distance(transform.position, target.position) <= _chaseDistance)
        {
            enemyAnim.Walk(false);
            enemyState = EnemyState.CHASE;
           

        }





    }
    void Chase()
    {
        navAgent.isStopped = false;
        navAgent.speed = _runSpeed;

        navAgent.SetDestination(target.position);

        if (navAgent.velocity.sqrMagnitude > 0)
        {
            enemyAnim.Run(true);
        }
        else
        {
            enemyAnim.Run(false);
        }
        if (Vector3.Distance(transform.position, target.position) <= _attackDistance)
        {
            enemyAnim.Run(false);
            enemyAnim.Walk(false);
            enemyState = EnemyState.ATTACK;
            if (_chaseDistance != currentChaseDistance)
            {
                _chaseDistance = currentChaseDistance;
            }
        }else if(Vector3.Distance(transform.position, target.position) > _chaseDistance)
        {
            enemyAnim.Run(false);
            enemyState = EnemyState.PATROL;
            patrolTimer = _patrolForTime;
            if (_chaseDistance != currentChaseDistance)
                {
                    _chaseDistance = currentChaseDistance;
                }
        }
    }
    void Attack()
    {
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;

        attackTimer += Time.deltaTime;
        if (attackTimer > _waitBeforeAttack)
        {
            enemyAnim.Attack();
            attackTimer = 0f;


        }

        if (Vector3.Distance(transform.position, target.position) > _attackDistance + _chaseAfterAttack)
        {
            enemyState = EnemyState.CHASE;
        }
    }
    void SetNewRandomDestination()
    {
        float rand_radius = Random.Range(_patrolRadiusMin, _patrolRadiusMax);
        Vector3 randDir = Random.insideUnitSphere * rand_radius;
        randDir += transform.position;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDir, out navHit, rand_radius, -1);

        navAgent.SetDestination(navHit.position);
    }
    void Turn_On_AttackPoint()
    {
        _attackPoint.SetActive(true);
    }
    void Turn_Off_AttackPoint()
    {
        if (_attackPoint.activeInHierarchy)
        {
            _attackPoint.SetActive(false);
        }
    }
    public EnemyState enemy_State
    {
        get;
        set;
        
    }
}
