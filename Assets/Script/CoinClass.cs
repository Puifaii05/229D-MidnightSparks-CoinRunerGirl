using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinClass : MonoBehaviour
{
    GameController gameController;
    public bool getItem = false;
    void Start()
    {
        // เชื่อม class
        if (gameController == null)
        {
            GameObject _tempGameController = GameObject.FindGameObjectWithTag("GameController") as GameObject;
            gameController = _tempGameController.GetComponent<GameController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.getItem)
        {
            Destroy(this.gameObject);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<TimerController>().StopTimer();
        }
    }
}
