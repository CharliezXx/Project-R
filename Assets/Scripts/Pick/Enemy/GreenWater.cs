using UnityEngine;

public class GreenWater : Enemy
{
    public float AttackSpeed;



    public override void Attack()
    {
        if (!Is_Waiting) 
        { 
            base.Attack();
            Rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
            m_Animator.speed = AttackSpeed;
            m_Animator.SetBool("Bite", true);
            Is_Attack = true;
        }
    }

    public override void OnAttackFinished()
    {
        base.OnAttackFinished();
        Is_Attack = false;
        Rigidbody.constraints =  RigidbodyConstraints.FreezeRotation;
        m_Animator.SetBool("Bite", false);
        StartCoroutine(Delay(3f));
    }
    public void CoolDown()
    {

    }
   

    private void Update()
    {
        //var AnimInfo = m_Animator.GetAnimatorTransitionInfo(0);
        //if (AnimInfo.normalizedTime >= 1f && AnimInfo.IsName("bite")) 
        //{
        //    m_Animator.SetBool("Bite",false);
        //    Is_Attack=false;
        //    StartCoroutine(Delay(5f));
        //}
    }
}
