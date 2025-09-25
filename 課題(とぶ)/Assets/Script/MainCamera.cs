using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] Transform Player;
    private Vector3 pos;
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    void Update()
    {
        pos = Player.position;
        pos.z = -10;
        transform.position = pos;
    }
}
