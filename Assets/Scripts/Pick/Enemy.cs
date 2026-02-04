using System.Collections;
using UnityEngine;

public class Enemy : CellData3D
{

    protected Animator m_Animator;
    [SerializeField] private Collider m_SearchBox;
    protected Rigidbody Rigidbody;
    private Vector3 SearchArea;
    public float m_MaxHealth;
    public int MoveSpeed;
    protected Vector3 TarGetMove;
    private Vector3 ObjScale;
    protected float m_CurrentHealth;
    protected bool Is_Moving;
    protected bool Is_Attack;
    protected bool Is_Waiting;
    


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
            case TriggerType.CanAttackArea:
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
        m_Animator = GetComponent<Animator>();
        SearchArea = m_SearchBox.bounds.extents;
        ObjScale = transform.localScale;
    }

    private void Update()
    {
        m_Animator.SetBool("Moving",Is_Moving);
        m_Pos = transform.position;
        if (!Is_Moving && !Is_Waiting && !Is_Attack)
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

    public virtual void Attack()
    {
        
    }

    public virtual void OnAttackFinished()
    {

    }

    public IEnumerator Delay(float Delay)
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
