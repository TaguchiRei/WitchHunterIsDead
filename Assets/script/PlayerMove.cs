using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField, Range(1, 15)] float _rotateSpeed = 1;
    [SerializeField] float _moveSpeed = 1;
    [SerializeField] Rigidbody _rig;
    Vector3 _move = Vector3.zero;
    float _mouseX = 0;
    
    void Update()
    {
        //マウスの動きでキャラを動かす
        _mouseX = Input.GetAxis("Mouse X");
        //キャラを動かす
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");
        if (vertical !=0)
        {
            if(vertical > 0)
            {
                _move += transform.forward;
            }else
            {
                _move += transform.forward *-0.5f;
            }
        }
        if (horizontal != 0)
        {
            if (horizontal < 0)
            {

            }
        }
    }
    private void FixedUpdate()
    {
        //プレイヤーの向きを変える
        if(Mathf.Abs(_mouseX) > 0.02)
        {
            transform.Rotate(0, _mouseX * _rotateSpeed, 0);
        }
        else if (Mathf.Abs(_mouseX) < 0.02)
        {
            transform.Rotate(0,_mouseX * _rotateSpeed , 0);
        }
        //プレイヤーを動かす
        _move.Normalize();
        _rig.velocity = _move * _moveSpeed;
    }
}
