using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{

    public float xSpeed;
    public float minYSpeed;
    public float maxYSpeed;
    Rigidbody2D m_rb;
    bool m_moveLeftOnStart;
    bool m_isDead;
    public GameObject deathVFX;
    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        RandomMovingDirection();
    }
    private void Update()
    {
        m_rb.velocity = m_moveLeftOnStart 
                         ? new Vector2(-xSpeed, Random.Range(minYSpeed, maxYSpeed))
                         : new Vector2(xSpeed, Random.Range(minYSpeed, maxYSpeed));
        flip();
    }
    public void RandomMovingDirection()
    {
        m_moveLeftOnStart = transform.position.x > 0
                            ? true
                            : false;
    }
    void flip()
    {
        if (m_moveLeftOnStart)
        {
            if (transform.localScale.x < 0)
                return;
                transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y,transform.localScale.z);
        }
        else
        {
            if (transform.localScale.x > 0)
                return;
                transform.localScale = new Vector3(transform.localScale.x *-1, transform.localScale.y, transform.localScale.z);
        }
    }
    public void die()
    {
        m_isDead = true;
        GameManager.Ins.BirdKilled++;
        Destroy(gameObject);
        if (deathVFX)
        {
            Instantiate(deathVFX, transform.position, Quaternion.identity);
            GameGUIManager.Ins.UpdateKillerCounting(GameManager.Ins.BirdKilled);
        }
    }
}
