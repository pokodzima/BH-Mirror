using System.Collections;
using System.Collections.Generic;
using Interfaces;
using Mirror;
using UnityEngine;

public class DashAttacked : NetworkBehaviour, IDashSubject
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnDashed()
    {
        print($"Dashed {gameObject.name}");
    }
}