using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

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
        
        RaycastHit hit = new RaycastHit();

        if (Physics.Raycast(Camera.main.ViewportToWorldPoint(new Vector3 (0.5f, 0.5f, 0)),playerTransform.position - new Vector3(0,0,-5), out hit, 10))
        {
            hit.collider.gameObject.GetComponent<Renderer>().enabled = false;
        }
    }
}
