using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class HealthScript : MonoBehaviour
{
    private EnemyAnimator enemyAnim;
    private NavMeshAgent navAgent;
    private EnemyController enemyController;

    public float _health = 100f;
    public bool _isPlayer, _isZombie;
    private bool isDead;



    void Awake()
    {
        if (_isZombie)
        {
            enemyAnim = GetComponent<EnemyAnimator>();
            enemyController = GetComponent<EnemyController>();
            navAgent = GetComponent<NavMeshAgent>();
        }
        if (_isPlayer)
        {

        }
    }




   public void ApplyDamage(float damage)
    {
        if (isDead)
            return;
        _health -= damage;

        if (_isPlayer)
        {

        }
        if (_isZombie)
        {
            if (enemyController.enemy_State == EnemyState.PATROL)
            {
                enemyController._chaseDistance = 50f;
            }
        }
        if (_health <= 0f)
        {
            PlayerDied();
            isDead = true;
        }
    }
    void PlayerDied()
    {
        if (_isZombie)
        {
            GetComponent<Animator>().enabled = false;
            GetComponent<BoxCollider>().isTrigger = false;
            GetComponent<Rigidbody>().AddTorque(-transform.forward * 20f);

            enemyController.enabled = false;
            navAgent.enabled = false;
            enemyAnim.enabled = false;

        }
        if (_isPlayer)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(Tags.ENEMY_TAG);

            for (int i = 0; i < enemies.Length; i++)
            {
                enemies[i].GetComponent<EnemyController>().enabled = false;
            }
            GetComponent<PlayerMovement>().enabled = false;
            GetComponent<PlayerAttack>().enabled = false;
            GetComponent<WeaponManager>().GetCurrentSelectedWeapon().gameObject.SetActive(false);
        }
        if (tag == Tags.PLAYER_TAG)
        {
            Invoke("RestartGame",3f);


        }
        else
        {
            Invoke("TurnOffGameObject",3f);
        }

    }
    void RestartGame()
    {
        //  UnityEngine.SceneManagement.SceneManager.LoadScene("");
        Debug.Log("help");
    }

    void TurnOffGameObject()
    {
        gameObject.SetActive(false);
    }


}
