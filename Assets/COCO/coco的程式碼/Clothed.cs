using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clothed : MonoBehaviour
{

    private Animator animator;
    private Rigidbody2D rb;
    private bool isWalking = false;// ��l��������
    private bool isRight = true;
    private float moveSpeed = 4f;

    public bool canMove = true;
    public float minX = -12f; // �����
    public float maxX = 13f;  // �k���

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = 0f;

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) // ����U D ��A����V�k��
        {
            isWalking = true;
            isRight = true;
            moveX = moveSpeed;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) // ����U A ��A����V����
        {
            isWalking = true;
            isRight = false;
            moveX = -moveSpeed;
        }
        else
        {
            isWalking = false; // �S�����U A �� D ��A���ⰱ���
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            isRight = true;  // ���⭱�V�k��
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            isRight = false; // ���⭱�V����
        }


        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isRight", isRight);

        rb.velocity = new Vector2(moveX, rb.velocity.y); // �u���� x �b���t�סAy �b�O������


        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        transform.position = new Vector3(clampedX, transform.position.y, 0f);  // �O�� z �b����

        // �T�O����u�|�b X �b����
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }
}
