using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBci : MonoBehaviour
{
    [SerializeField] private PlayerMovement pm;
    private Transform playerTransform;

    void Start()
    {
        playerTransform = pm.transform;
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, -10f);
    }
}
