using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody2D rgd;
    private Animator animator;
    private float horizontalInput;

    public int maxHealth;
    public int currentHealth;

    public Healthbar healthbar;

    [Header("移动设置")]
    public float moveSpeed;
    public float jumpForce;

    [Header("地面检测")]
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    public bool isGrounded; 

    public void ProcessDamage(int damage)
    {
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            SceneManager.LoadScene(0);
            // 这里可以添加玩家死亡的逻辑，例如播放死亡动画、重置位置等
        }
    }
    void Start()
    {
        rgd = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(currentHealth);
    }

    void Update()
    {
        // 1. 地面检测
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        // 2. 左右移动逻辑
        horizontalInput = Input.GetAxisRaw("Horizontal");
        rgd.velocity = new Vector2(horizontalInput * moveSpeed, rgd.velocity.y);

        // 3. 翻转人物朝向
        if (rgd.velocity.x > 0)
        {
            transform.localScale = new Vector3(1.0f, transform.localScale.y, transform.localScale.z);
        }
        else if (rgd.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1.0f, transform.localScale.y, transform.localScale.z);
        }

        // 4. 跳跃逻辑：只有在地面上 (isGrounded 为 true) 时才允许跳跃
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rgd.velocity = new Vector2(rgd.velocity.x, jumpForce);
        }

        // 5. 掉落重置
        if (transform.position.y < -10f)
        {
            transform.position = new Vector2(0, 0);
            rgd.velocity = Vector2.zero;
        }

        // 6. 动画更新：将代码里的 isGrounded 传给动画机的参数
        animator.SetFloat("Speed", Mathf.Abs(rgd.velocity.x));
        animator.SetBool("isGrounded", isGrounded);
    }

    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}