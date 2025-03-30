using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool getItem = false;
    public int coinNum = 0;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetCoin()
    {
        getItem = true;
        coinNum = coinNum + 1;  
    }



}
