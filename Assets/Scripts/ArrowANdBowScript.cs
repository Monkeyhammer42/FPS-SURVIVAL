using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowANdBowScript : MonoBehaviour
{
    private Rigidbody myBody;
    public float _speed = 30f;
    public float _deactivateTimer = 3f;
    public float _damage = 15f;



    private void Awake()
    {
        myBody = GetComponent<Rigidbody>();
    }
    void Start()
    {
        Invoke("DeactivateGameObject", _deactivateTimer);
    }

    public void Launch(Camera mainCamera)
    {
        myBody.velocity = mainCamera.transform.forward * _speed;
        transform.LookAt(transform.position + myBody.velocity);
    }
    void DeactivateGameObject()
    {
        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider target)
    {
        
    }
}




