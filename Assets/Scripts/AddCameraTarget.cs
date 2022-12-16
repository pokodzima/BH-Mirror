using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Mirror;
using UnityEngine;

public class AddCameraTarget : NetworkBehaviour
{
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        var cinemachineFreeLook = FindObjectOfType<CinemachineFreeLook>();
        var thisTransform = transform;
        cinemachineFreeLook.Follow = thisTransform;
        cinemachineFreeLook.LookAt = thisTransform;
        GetComponent<PlayerMovement>().SetCameraTransform(Camera.main.transform);
    }
}
