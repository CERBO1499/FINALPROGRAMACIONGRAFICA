using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class MoveBalaPlaceHolder : MonoBehaviour
{
    private Rigidbody insBalaRB;
    [SerializeField] private float bulletSpeed;
    private bool Moviendo=false;
    [SerializeField] private float tiempo = 5f;
    [SerializeField] private Transform initialPlace;
    
    

    private void Awake()
    {
        insBalaRB = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Movement();
        
    }


    IEnumerator ReturnEfectMove()
    {
        yield return new WaitForSeconds(tiempo);
        gameObject.transform.position = initialPlace.position;
        gameObject.transform.rotation = initialPlace.rotation;
        insBalaRB.velocity=Vector3.zero;
        insBalaRB.angularVelocity=Vector3.zero;
      // gameObject.SetActive(false);
        Moviendo = false;

    }

    void Movement()
    {
        if (Input.GetKeyDown(KeyCode.A)&& Moviendo==false)
        {
            gameObject.SetActive(true);
            //insBalaRB.AddForce(Vector3.left*bulletSpeed);
            Moviendo = true;
        }

        if (Moviendo)
        {
            StartCoroutine(ReturnEfectMove());

        }
    }
}
