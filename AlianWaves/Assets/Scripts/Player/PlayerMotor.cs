using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMotor : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 12;
    [SerializeField] private float jumpForce = 12;
    [SerializeField] private float groundCheckRadius = .2f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;

    [Header("Graphics")]
    [SerializeField] private Transform bodyGFX;
    [SerializeField] private Transform armGFX;

    private float horizontal;
    public float Horizontal { get { return horizontal; } }
    private bool jump;
    private bool thrust;

    private bool isGrounded;
    public bool IsGrounded { get { return isGrounded; } }
    private bool wasGrounded;
    private bool facingRight = true;

    public float GroundCheckRadias { get { return groundCheckRadius; } }
    public LayerMask WhatIsGround { get { return whatIsGround; } }

    private Rigidbody2D rb;
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = bodyGFX.GetComponent<Animator>();
    }

	private void FixedUpdate()
	{
        isGrounded = false;
        wasGrounded = isGrounded;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, whatIsGround);
		foreach (Collider2D _collider in colliders)
		{
            if (_collider.gameObject != gameObject)
			{
                isGrounded = true;
                if (!wasGrounded)
				{
                    //When You Leave Or Land On The Ground.
				}
			}
		}

        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("speed", Mathf.Abs(InputManager.Horizontal));

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);

        if (facingRight && transform.position.x > InputManager.MousePosition.x)
            Flip();
        if (!facingRight && transform.position.x < InputManager.MousePosition.x)
            Flip();

        if (jump && isGrounded)
        {
            isGrounded = false;
            rb.AddForce(Vector2.up * jumpForce);
        }

        if (thrust)
            rb.AddForce(Vector2.up * 50f);
    }

    public void Move(float _hori, bool _jump, bool _thrust)
    {
        horizontal = _hori;
        jump = _jump;
        thrust = _thrust;
    }

    private void Flip()
	{
        Vector3 scale = armGFX.localScale;
        armGFX.localScale = new Vector3(scale.x, -scale.y, scale.z);

        if (facingRight)
            bodyGFX.rotation = Quaternion.Euler(0f, 180f, 0f);
		else bodyGFX.rotation = Quaternion.Euler(0f, 0f, 0f);
		
        facingRight = !facingRight;
	}
}
