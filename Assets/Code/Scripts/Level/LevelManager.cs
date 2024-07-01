using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;


    public Transform stratPoint;
    public Transform[] path;

    //New 29.06 Currency
    public int currency; //int(Integer) is a type of a variable. Ex: 1, 2, 3 etc.
    
    private void Start()
    {
        currency = 100;
    }
    //
    private void Awake()
    {
        main = this;
    }
    //New 29.06 Currency
    public void IncreaseCurrency(int amount)
    {
        currency += amount;
        Debug.Log("Currency: " + currency);
    }
    public bool SpendCurrency(int amount)
    {
        if (amount <= currency)
        {
            currency -= amount;
            return true;
        }
        else
        {
            Debug.Log("You do not have enough to purchase this item.");
            return false;
        }
    }
    //
}
