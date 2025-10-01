using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Transform Player;
    private Vector3 offset;


    public void Follow(Transform playerT, Vector3 posOffset )
    {
        Player = playerT;
        offset = posOffset;
    }

    void Update()
    {
        if (Player != null)
        {
            transform.position = Player.position + offset;
        }
    }
}
