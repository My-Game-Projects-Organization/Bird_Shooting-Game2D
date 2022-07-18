using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // tao do trễ khi bắn
    public float fireRate;
    float m_fireRate;

    bool m_isShooted;

    // tao ong ngam
    public GameObject viewFinder;

    GameObject m_viewFinderClone;

    private void Awake()
    {
        m_fireRate = fireRate;
    }

    private void Start()
    {
        if (viewFinder)
        {
            m_viewFinderClone = Instantiate(viewFinder, Vector3.zero, Quaternion.identity);
        }
    }

    private void Update()
    {

        // chuyen doi toa do cua ng choi ve cua unity
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) && !m_isShooted)
        {
            Shoot(mousePos);
        }

        if (m_isShooted)
        {
            m_fireRate -= Time.deltaTime;

            if(m_fireRate <= 0)
            {
                m_isShooted = false;

                m_fireRate = fireRate;
            }

            GameGUIManager.Ins.UpdateFireRate(m_fireRate / fireRate);
        }

        // di chuyen ong theo mouse
        if (m_viewFinderClone)
        {
            // do mousePos là vị trí của mouse nên trục x,y,z trùng vs ống ngắm => ko thể nhìn thấy điểm mù => tạo vector mới
            m_viewFinderClone.transform.position = new Vector3(mousePos.x,mousePos.y,0f);
        }
    }
    void Shoot(Vector3 mousePos)
    {

        m_isShooted = true;
        // lay huong tu camere toi con tro chuot
        Vector3 shootDir = Camera.main.transform.position - mousePos;

        shootDir.Normalize();

        //  Physics2D.RaycastAll() lấy all vị trí chạm phải 
        RaycastHit2D[] hits = Physics2D.RaycastAll(mousePos, shootDir);

        if(hits.Length > 0 && hits != null)
        {
            for(int i = 0;i< hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];
                
                // check xem hit va cham voi vat nao do chua va kc giua vat va cham va con tro chuot phai nho hon X
                if(hit.collider && (Vector3.Distance((Vector2)hit.collider.transform.position,(Vector2)mousePos) <= 0.4f))
                {
                    Bird bird = hit.collider.GetComponent<Bird>();

                    if (bird)
                    {
                        bird.Die();
                    }
                }
            }
        }

        AudioController.Ins.PlaySound(AudioController.Ins.shooting);
    }
}
