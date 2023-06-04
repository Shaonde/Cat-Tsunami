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
    private Animator animator;
    
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        _rb = GetComponent<Rigidbody>();
        AddPoto();
        AddPoto();
        AddPoto();
    }

    void Death()
    {
        PlayerPrefs.SetInt("score",PlateformeStart.Instance.Score);
        SceneManager.LoadScene(2);
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
        animator.SetBool("isJumping" ,isJumping);
        if(Input.GetButtonDown("Fire1")
        && Physics.Raycast(transform.position,Vector3.down,out _hit,1.5f,LayerMask.GetMask("Ground")))
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

        if(transform.position.y <= -5)
            Death();

    }

    public void AddPoto()
    {
        if (Potos.Count < 5)
        {
            animator.SetTrigger("Paint");
            Potos.Add(Instantiate(PotoPrefab.gameObject,new Vector3(transform.position.x-(1.5f*(Potos.Count+1)),transform.position.y,transform.position.z),Quaternion.identity).GetComponent<PotoController>());
        }
    }

    public void DeletePoto()
    {
        if(Potos.Count > 0)
        {
            Destroy(Potos[Potos.Count-1].gameObject);
            Potos.RemoveAt(Potos.Count-1);
            return;
        }
        Death();
    }

    void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Heal"))
        {
            AddPoto();
            PlateformeStart.Instance.Bonus();
            return;  
        }

        DeletePoto();
    }

}
