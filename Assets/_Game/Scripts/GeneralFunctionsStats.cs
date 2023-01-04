using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralFunctionsStats : MonoBehaviour
{
    void Start()
    {
        
    }

    public int CheckMaxAmount(int amount, int to_add, int max)
    {
        if (amount <= max)
        {
            if (amount + to_add > max)
                amount = max;
            else
                amount += to_add;
        }
        return amount;
    }
    public int CheckMinAmount(int amount, int to_substract)
    {
        if (amount > 0)
        {
            if (amount - to_substract < 0)
                amount = 0;
            else
                amount -= to_substract;
        }
        return amount;
    }
}