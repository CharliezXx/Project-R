using UnityEngine;

public class CellData3D : MonoBehaviour
{
    protected Vector3 m_Pos;
    protected Vector3 m_BasePos;

    public virtual void Init(Vector3 coord)
    {
        m_Pos = coord;
        m_BasePos = coord;
    }

    public virtual void OnTrigger(Collider other,TriggerType Type)
    {

    }


    public virtual void PlayerEntered()
    {

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
