using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionLog : MonoBehaviour, IConsumable
{

    public void Consume() {
        Debug.Log("Potion consumed");
    }

    public void Consume(CharacterStats stats) {
        Debug.Log("Potion gave the following stats" + stats);
    }
}
