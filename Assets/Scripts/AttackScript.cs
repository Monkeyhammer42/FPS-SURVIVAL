using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{

    public float _damage = 2f;
    public float _radius = 1f;
    public LayerMask _layerMask;






    void Update()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _radius, _layerMask);
        if (hits.Length > 0)
        {
            hits[0].gameObject.GetComponent<HealthScript>().ApplyDamage(_damage);
            gameObject.SetActive(false);

        }
    }



}
