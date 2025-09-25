using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float Speed = 1.0f;
    private Rigidbody2D rd;
    private Vector2 Direction;

    private float MoveX;

    private void Awake()
    {
        rd = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Operation();
    }

    private void FixedUpdate()
    {
        //�ړ����x�x�N�g�������ݒl����擾
        Direction = rd.velocity;

        //X�����̑��x����͂��猈��
        Direction.x = MoveX * Speed;

        rd.velocity = Direction;

    }

    private void Operation()
    {
        float MoveX = Input.GetAxisRaw("Horizontal");   //���E
        float MoveY = Input.GetKeyDown(KeyCode.Space) ? 10 : 0; //�W�����v 

        rd.velocity = new Vector2(rd.velocity.x, MoveY);

        if (MoveX == 1)
        {

        }
        else if (MoveX == -1)
        {
            
        }

    }

}
