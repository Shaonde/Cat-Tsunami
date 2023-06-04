using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotoController : MonoBehaviour
{
    private CharacterController _player;
    float jumpTimeCounter;
    private Rigidbody _rb;
    private RaycastHit _hit;
    bool isJumping;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
    }

    void Update()
    {
        if(isJumping)
        {
            if(jumpTimeCounter > 0)
            {
                _rb.velocity = Vector3.up * _player.JumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        if(transform.position.y <= -5)
            _player.DeletePoto();
    }

    public void StartJump()
    {
        isJumping = true;
        jumpTimeCounter = _player.jumpTime;
        _rb.velocity = Vector3.up * _player.JumpForce;
    }

    public void StopJump()
    {
        isJumping = false;
    }

    public void DieAnim()
    {
        //TODO: Evrard, fait un truc
    }
}
