using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour
{
    public float JumpForce = 7f;
    public float jumpTime = 0.3f;
    [SerializeField] PotoController PotoPrefab;
    private List<PotoController> Potos = new List<PotoController>();
    float jumpTimeCounter;
    private Rigidbody _rb;
    private RaycastHit _hit;
    bool isJumping;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        AddPoto();
        AddPoto();
        AddPoto();
    }

    void JumpToPotos()
    {
        for(int i = 0; i<Potos.Count; i++)
        {
            Potos[i].Invoke("StartJump",((i+1)*1.5f)/PlateformeStart.Instance.GetActualSpeed());
        }
    }
    void StopJumpToPotos()
    {
        for(int i = 0; i<Potos.Count; i++)
        {
            Potos[i].Invoke("StopJump",((i+1)*1.5f)/PlateformeStart.Instance.GetActualSpeed());
        }
    }
    void Update()
    {
        if(Input.GetButtonDown("Fire1")
        && Physics.Raycast(transform.position,Vector3.down,out _hit,1f,LayerMask.GetMask("Ground")))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            _rb.velocity = Vector3.up * JumpForce;
            JumpToPotos();
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
        {
            isJumping = false;
            StopJumpToPotos();
        }

    }

    public void AddPoto() => Potos.Add(Instantiate(PotoPrefab.gameObject,new Vector3(transform.position.x-(1.5f*(Potos.Count+1)),transform.position.y,transform.position.z),Quaternion.identity).GetComponent<PotoController>());

}
