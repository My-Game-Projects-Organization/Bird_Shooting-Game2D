using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float xSpeed;
    public float minYspeed;
    public float maxYspeed;

    Rigidbody2D m_rb;
    public GameObject deathVFX;


    bool m_moveLeftOnStart;
    bool m_isDead;

    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();

        RandomMovingDirection();
    }

    private void Update()
    {
        m_rb.velocity = m_moveLeftOnStart ?
            new Vector2(- xSpeed,Random.Range(minYspeed,maxYspeed))
            : new Vector2(xSpeed, Random.Range(minYspeed, maxYspeed));
        Flip();
    }

    public void RandomMovingDirection()
    {
        // x > 0 => ở bên phải => di chuyển sang trái
        m_moveLeftOnStart = transform.position.x > 0 ? true : false;
    }

    // đổi hướng
    void Flip()
    {
        if (m_moveLeftOnStart)
        {
            if (transform.localScale.x < 0) return;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        else
        {
            if (transform.localScale.x > 0) return;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
    }

    public void Die()
    {
        m_isDead = true;
        GameManager.Ins.BirdKilled++;

        Destroy(gameObject);


        if (deathVFX)
        {
            Instantiate(deathVFX,transform.position, Quaternion.identity);
        }

        GameGUIManager.Ins.UpdateKilledCounting(GameManager.Ins.BirdKilled);
    }
}
