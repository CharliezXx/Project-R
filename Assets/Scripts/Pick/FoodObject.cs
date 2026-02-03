using UnityEngine;

public class FoodObject : CellData3D
{
    public float HpEffect;

    public override void OnTrigger(Collider other, TriggerType Type)
    {
        if ( Type == TriggerType.Collect )
        {
            GameManager.instance.ChangePlayerHp(HpEffect);
            Destroy(gameObject);
        }
    }

}
