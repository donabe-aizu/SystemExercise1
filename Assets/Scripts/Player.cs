using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] 
    private UDPTest udp;
    
    [SerializeField]
    private float playerSpeed = 1;
    [SerializeField]
    private float moveSpeed = 1;

    private Rigidbody _rigidbody;
    private bool _isGameOrver;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _isGameOrver = false;
    }

    void FixedUpdate()
    {
        // _rigidbody.MovePosition(transform.position + new Vector3(Input.GetAxis("Horizontal")*moveSpeed, Input.GetAxis("Vertical")*moveSpeed, playerSpeed) * Time.deltaTime);
        _rigidbody.MovePosition(transform.position + new Vector3(udp.x*moveSpeed, udp.y*moveSpeed, playerSpeed) * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameOver();
    }

    private void GameOver()
    {
        _isGameOrver = true;
        udp.isHit = true;
    }

    private void GameClear()
    {
        if (!_isGameOrver)
        {
            udp.Send("Clear");
        }
    }
}
