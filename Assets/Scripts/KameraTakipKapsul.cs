using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KameraTakipKapsul : MonoBehaviour
{
    private GameObject Player;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

    }

    void FixedUpdate()
    {
        transform.position = Player.transform.position;
    }
}
