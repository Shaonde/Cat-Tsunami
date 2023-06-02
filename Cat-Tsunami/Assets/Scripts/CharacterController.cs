using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] float JumpForce = 5f;
    [SerializeField] float jumpTime = 5f;
    float jumpTimeCounter;
    private Rigidbody _rb;
    private RaycastHit _hit;
    bool isJumping;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(Input.GetButtonDown("Fire1")
        && Physics.Raycast(transform.position,Vector3.down,out _hit,1f,LayerMask.GetMask("Ground")))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            _rb.velocity = Vector3.up * JumpForce * Time.deltaTime;
        }

        if(Input.GetButtonDown("Fire1") && isJumping)
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
}
