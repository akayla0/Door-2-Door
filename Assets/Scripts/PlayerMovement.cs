using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movespeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if(animator == null)
            animator = GetComponent<Animator>();
        if (GameManager.Instance.playerData != null)
        {
            transform.position = GameManager.Instance.playerData.position;
        }
    }

    
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            movespeed = 8f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            movespeed = 5f;
        }

        bool isWalking = movement.sqrMagnitude > 0.01f;

        animator.SetBool("isWalking", isWalking);

    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * movespeed * Time.fixedDeltaTime);
    }
}
