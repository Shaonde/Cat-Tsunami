using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] float JumpForce = 5f;
    [SerializeField] float jumpTime = 5f;
    [SerializeField] PotoController PotoPrefab;
    private List<PotoController> Potos;
    float jumpTimeCounter;
    private Rigidbody _rb;
    private RaycastHit _hit;
    bool isJumping;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        AddPoto();
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1")
        && Physics.Raycast(transform.position,Vector3.down,out _hit,1f,LayerMask.GetMask("Ground")))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            _rb.velocity = Vector3.up * JumpForce;
        }

        if(Input.GetButton("Fire1") && isJumping)
        {
            if(jumpTimeCounter > 0)
            {
                _rb.velocity = Vector3.up * JumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if(Input.GetButtonUp("Fire1"))
            isJumping = false;
    }

    public void AddPoto() => Potos.Add(Instantiate(PotoPrefab.gameObject,new Vector3(transform.position.x-(1.5f*(Potos.Count+1)),transform.position.y,transform.position.z),Quaternion.identity).GetComponent<PotoController>());

}
