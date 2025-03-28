using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpForce = 5f;  // ���D�O�D
    public LayerMask groundLayer; // �a���ϼh
    public Transform groundCheck; // �Ω��˴��O�_��Ĳ�a��
    public float groundCheckRadius = 0.2f; // �˴��d��

    private Rigidbody2D rb;
    public bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // �ˬd����O�_��Ĳ�a��
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // ���U�ť���åB����b�a���W�~���
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
}
