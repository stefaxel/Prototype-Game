using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceField : MonoBehaviour
{
    [SerializeField] public int forceFieldPower = 10;
    [SerializeField] protected GameObject forceField;
    public bool forceFieldActive = true;

    public void DamageForceField(int damage)
    {
        forceFieldPower -= damage;

        if (forceFieldPower < 0)
        {
            forceField.SetActive(false);
            forceFieldActive = false;
        }
    }
    
}
