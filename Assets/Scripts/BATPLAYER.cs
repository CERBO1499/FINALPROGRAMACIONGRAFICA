using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BATPLAYER : MonoBehaviour
{
    [SerializeField] private Transform bat, bat_ueq;

    public Transform Target;

    public bool efectoReproduciendo;
    // Update is called once per frame
    void Update()
    {
        if (!efectoReproduciendo)
        {
            bat.position = bat_ueq.position;
            bat.rotation = bat_ueq.rotation;
        }
        
    }
}
