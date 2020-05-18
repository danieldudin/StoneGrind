using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableController : MonoBehaviour
{
    CharacterStats stats;

    void Start()
    {
        stats = GetComponent<CharacterStats>();
    }

    public void ConsumeItem(Item item) {
        GameObject itemToConsume = Instantiate(Resources.Load<GameObject>("Prefabs/Items/Consumables/" + item.ObjectSlug));

        if (item.ItemModifier) {
            itemToConsume.GetComponent<IConsumable>().Consume(stats);
        } else {
            itemToConsume.GetComponent<IConsumable>().Consume();
        }
    }
}
