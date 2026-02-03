using UnityEngine;

public class Trigger : MonoBehaviour 
{
    public TriggerType TriggerType;
    private CellData3D owner;

    private void Awake()
    {
        owner = GetComponentInParent<CellData3D>();
        if (owner == null)
        {
            owner = GetComponent<CellData3D>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
        owner.OnTrigger(other, TriggerType);
        }
    }
}
