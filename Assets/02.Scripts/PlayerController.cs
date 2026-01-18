using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator animator;
    float jumpForce = 680.0f;
    float walkForce = 30.1f;
    float maxWalkSpeed = 2.0f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && rb.velocity.y.Equals(0))
        {
            rb.AddForce(transform.up * jumpForce);   // 0,-1,0
            animator.SetTrigger("Jump");
        }

        int key = 0;
        if (Input.GetKey(KeyCode.D)) key = 1;
        if (Input.GetKey(KeyCode.A)) key = -1;

        float speedx = Mathf.Abs(rb.velocity.x);

        if (speedx < maxWalkSpeed)
        {
            rb.AddForce(transform.right * key * walkForce);
        }
        if (key != 0)
        {
            transform.localScale = new Vector3(key, 1, 1);    // 2D를 좌우반전 시키는법. 1. scale을 조정하기. 2. SpriteRenderer에서 플립을 바꾸기. 3. y축을 180도 회전시키기.
        }

        //플레이어 속도에 맞춰서 애니매이션 속도를 지정한다.
        animator.speed = speedx / 2.0f;

        if (transform.position.y <= -5)
        {
            SceneManager.LoadScene("GameScene");
        }

        if (rb.velocity.x == 0 && rb.velocity.y == 0)
        {
            animator.SetTrigger("Stay");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("골인");
        SceneManager.LoadScene("ClearScene");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("닿음");
    }

    
}
