using System.Collections;
using UnityEngine;

public class Enemy : CellData3D
{
    [SerializeField] private SpriteRenderer SpriteRenderer;
    [SerializeField] private Animator m_Animator;
    [SerializeField] private Collider m_SearchBox;
    private Rigidbody Rigidbody;
    private Vector3 SearchArea;
    public float m_MaxHealth;
    public int MoveSpeed;
    private Vector3 TarGetMove;
    private Vector3 ObjScale;
    private float m_CurrentHealth;
    private bool Is_Moving;
    private bool Is_Waiting;
    


    public override void Init(Vector3 coord)
    {
        base.Init(coord);
        m_CurrentHealth = m_MaxHealth;
    }

    public override void OnTrigger(Collider other, TriggerType Type)
    {
        base.OnTrigger(other, Type);
        switch (Type) 
        {
            case TriggerType.Follow:
                Follow();
                break;
            case TriggerType.Attack:
                Attack();
                break;
        }
    }


    Vector3 PlayerPos()
    {
        return GameManager.instance.PlayerController.transform.position;
    }



    public void MoveTo(Vector3 coord)
    {
        TarGetMove = coord;
        Is_Moving = true;
    }


    public void MoveAround()
    {
        float x = Random.Range(-SearchArea.x, SearchArea.x);
        float z = Random.Range(-SearchArea.z, SearchArea.z);
        x += m_BasePos.x;
        z += m_BasePos.z;

        MoveTo(new Vector3(x, 0, z));
    }

    void Follow()
    {
        MoveTo(PlayerPos());
    }

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        SearchArea = m_SearchBox.bounds.extents;
        ObjScale = transform.localScale;
    }

    private void Update()
    {
        m_Animator.SetBool("Moving",Is_Moving);
        m_Pos = transform.position;
        if (!Is_Moving && !Is_Waiting)
        {
            MoveAround();
            StartCoroutine(Delay(5f));
        }
        ShouldFlip();
    }

    private void FixedUpdate()
    {
        Vector3 NewPos = Vector3.MoveTowards(Rigidbody.position, TarGetMove, MoveSpeed * Time.fixedDeltaTime);
        Rigidbody.MovePosition(NewPos);

        if (NewPos == TarGetMove)
        {
            Is_Moving = false;
        }
    }

    public void Attack()
    {

    }

    IEnumerator Delay(float Delay)
    {
        var RandomSec = Random.Range(Delay - 2, Delay);
       Is_Waiting = true;
       yield return new WaitForSeconds(RandomSec);
       Is_Waiting = false;
    }

    void ShouldFlip()
    {
        if (m_Pos.x < TarGetMove.x) //กำลังจะเดินขวา
        {
            transform.localScale = new Vector3(-ObjScale.x, ObjScale.y, ObjScale.z);
        }
        else if (m_Pos.x > TarGetMove.x)
        {
            transform.localScale = ObjScale;
        }
    }

}
