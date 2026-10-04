using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    public int damage; // 每次碰撞检测的伤害值

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 检查撞到的是不是玩家
        if (collision.gameObject.CompareTag("Player"))
        {
            
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            
            if (playerHealth != null)
            {
                playerHealth.takeDamage(damage);
            }
        }
    }
}