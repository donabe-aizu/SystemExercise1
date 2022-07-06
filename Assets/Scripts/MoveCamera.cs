using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] 
    private Transform playerTransform;

    [SerializeField] 
    private Vector3 addPosition = new Vector3(0,0,-10);

    void Start()
    {
        
    }
    
    void Update()
    {
        this.transform.position = new Vector3(0,0,playerTransform.transform.position.z) + addPosition;
    }
}
