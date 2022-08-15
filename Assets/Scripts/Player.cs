using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float fireRate;
    float m_curFireRate;
    bool m_isShoted;
    public GameObject viewFindder;
    GameObject m_viewFinderClone;
    private void Awake()
    {
        m_curFireRate = fireRate;
    }
    private void Start()
    {
        if (viewFindder)
         m_viewFinderClone =  Instantiate(viewFindder, Vector3.zero, Quaternion.identity);
    }
    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);// camera vao co tro chuot
        if (Input.GetMouseButtonDown(0) && !m_isShoted)
        {
            shot(mousePos);
        }
        if (m_isShoted)
        {
            m_curFireRate -= Time.deltaTime;
            if (m_curFireRate <= 0)
            {
                m_isShoted = false;
                m_curFireRate = fireRate;
            }
            GameGUIManager.Ins.UpdateFireRate(m_curFireRate / fireRate);
        }
        if (m_viewFinderClone)
        {
            m_viewFinderClone.transform.position = new Vector3(mousePos.x,mousePos.y,0);
        }
    }
    void shot(Vector3 mousePos)
    {
        m_isShoted = true;
        Vector3 shotDix = Camera.main.transform.position - mousePos;
        shotDix.Normalize();
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos,shotDix);
        if(hits!= null && hits.Length>0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];
                if (hit.collider != null && (Vector3.Distance((Vector2)hit.collider.transform.position, (Vector2)mousePos))<=0.4f)
                {
                    Debug.Log(hit.collider.name);
                    Bird bird = hit.collider.GetComponent<Bird>();
                    if (bird)
                    {
                        bird.die();
                    }
                }
            }
        }
        CineController.Ins.ShakeTrigger();
        AudioController.Ins.PlaySound(AudioController.Ins.shooting);
        
    }
    
}
