using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public Healthbar healthbar;
    public float immortalTime;
    public float immortalCounter;
    public GameObject immortalEffect;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        if (healthbar != null)
        {
            healthbar.SetMaxHealth(maxHealth);
        }

        // 游戏开始时，确保特效是关闭的
        if (immortalEffect != null)
        {
            immortalEffect.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (immortalCounter > 0)
        {
            immortalCounter -= Time.deltaTime;
            if (immortalCounter <= 0)
            {
                
                if (immortalEffect != null)
                {
                    immortalEffect.SetActive(false); //禁用特效
                }
            }
        }
    }

    public void takeDamage(int damage)
    {
        if (immortalCounter <= 0)
        {
            currentHealth -= damage;

            if (healthbar != null)
            {
                healthbar.SetHealth(currentHealth);
            }

            if (currentHealth <= 0)
            {
                gameObject.SetActive(false);
            }
            else
            {
                immortalCounter = immortalTime;

                if (immortalEffect != null)
                {
                    immortalEffect.SetActive(true); //启用特效
                }
            }
        }
    }
}