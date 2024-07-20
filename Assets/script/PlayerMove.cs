using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField, Range(1, 15)] float _rotateSpeed = 1;
    [SerializeField] float _moveSpeed = 1;
    [SerializeField] float _dashSpeed = 1.2f;
    [SerializeField] Rigidbody _rig;
    [SerializeField] Animator _anim;
    Vector3 _move = Vector3.zero;
    float _mouseX = 0;
    bool _dash = false;

    void Update()
    {
        //マウスの動きでキャラの視点を動かす
        _mouseX = Input.GetAxis("Mouse X");
        //キャラを動かす
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");
        if (vertical != 0)
        {
            if (vertical > 0)
            {
                _move += transform.forward;
            }
            else
            {
                _move += transform.forward * -0.9f;
            }
        }
        if (horizontal != 0)
        {
            _move += transform.right * horizontal;
        }
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            _dash = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _dash = false;
        }
    }
    private void FixedUpdate()
    {
        //プレイヤーの向きを変える
        if (Mathf.Abs(_mouseX) > 0.02)
        {
            transform.Rotate(0, _mouseX * _rotateSpeed, 0);
        }
        else if (Mathf.Abs(_mouseX) < 0.02)
        {
            transform.Rotate(0, _mouseX * _rotateSpeed, 0);
        }
        //プレイヤーを動かす
        var speed = _moveSpeed;
        if(_dash)
        {
            speed = speed * _dashSpeed;
            _anim.SetInteger("run", 2);
        }
        else
        {
            _anim.SetInteger("run", 1);
        }
        if (_move.x ==0||_move.z == 0)
        {
            _anim.SetInteger("run",0);
        }
        _move.Normalize();
        _rig.velocity = new Vector3(_move.x * speed, _rig.velocity.y, _move.z * speed);
        _move = new Vector3(0, _rig.velocity.y, 0);
    }
}
