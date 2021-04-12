using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bow : MonoBehaviour
{
    [SerializeField] private Arrow m_ArrowPrefab;
    [SerializeField] private Transform m_ArrowSpawn;
    [SerializeField] private Transform m_ArrowPull;
    [SerializeField] private Bowstring m_Bowstring;

    [Header("Parameters")]
    public float m_ReloadTime;
    public float m_DrawSpeed;
    public float m_Power = 50.0f;

    private bool m_IsReloading = false;
    private bool m_IsShooting = false;
    private Arrow m_CurrentArrow;

    private void Start()
    {
        Reload();
    }

    private void FixedUpdate()
    {
        DrawBowstring();
        AttackAction();

        /*
        if (!IsReady) return;

        Pull();
        ShootArrow();
        */
    }

    private void DrawBowstring()
    {
        if (!m_CurrentArrow) m_Bowstring.DrawBowstring(m_ArrowSpawn.transform);
        else m_Bowstring.DrawBowstring(m_CurrentArrow.transform);
    }

    private void AttackAction()
    {
        if (!m_IsShooting) return;

        Vector3 position = m_CurrentArrow.transform.position;
        Vector3 endPosition = m_ArrowPull.position;

        if (Vector3.Distance(position, endPosition) > 0.1f)
        {
            // Pull the arrow
            Vector3 step = (endPosition - position) * m_DrawSpeed * Time.fixedDeltaTime;
            //m_CurrentArrow.m_Body.MovePosition(position + step);
            m_CurrentArrow.transform.Translate(step);
        }
        else
        {
            // Shoot the arrow
            m_CurrentArrow.Fire(m_Power);
            m_CurrentArrow = null;
            //m_IsDrawn = false;
            m_IsShooting = false;

            GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;

            Reload();
        }
    }

    [ContextMenu("Shoot")]
    public void Shoot()
    {
        if (!IsReady) return;

        GetComponentInParent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        m_IsShooting = true;
    }

    /*
    [ContextMenu("Shoot Arrow")]
    private void ShootArrow()
    {
        
        if (!m_IsDrawn) return;

        m_CurrentArrow.Fire();
        m_CurrentArrow = null;
        m_IsDrawn = false;

        Reload();
    }

    [ContextMenu("Pull Arrow")]
    private void Pull()
    {
        if (m_IsDrawn) return;

        Vector3 position = m_CurrentArrow.transform.position;
        Vector3 endPosition = m_ArrowPull.position;

        if (Vector3.Distance(position, endPosition) > 0.1f)
        {
            Vector3 step = (endPosition - position) * m_DrawSpeed * Time.fixedDeltaTime;
            m_CurrentArrow.m_Body.MovePosition(position + step);
        }
        else m_IsDrawn = true;
    }
    */

    [ContextMenu("Reload")]
    public void Reload()
    {
        if (m_IsReloading || m_CurrentArrow != null) return;

        m_IsReloading = true;
        StartCoroutine(ReloadAfterTime());
    }

    private IEnumerator ReloadAfterTime()
    {
        yield return new WaitForSeconds(m_ReloadTime);

        m_CurrentArrow = Instantiate(m_ArrowPrefab, m_ArrowSpawn.position, m_ArrowSpawn.rotation);

        //m_CurrentArrow.m_Body.isKinematic = true;
        m_CurrentArrow.transform.SetParent(this.transform);

        m_IsReloading = false;
    }

    public bool IsReady => (!m_IsReloading && m_CurrentArrow != null);
}
