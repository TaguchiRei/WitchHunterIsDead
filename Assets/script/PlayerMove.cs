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
        //�}�E�X�̓����ŃL�����𓮂���
        _mouseX = Input.GetAxis("Mouse X");
        //�L�����𓮂���
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
        //�v���C���[�̌�����ς���
        if(Mathf.Abs(_mouseX) > 0.02)
        {
            transform.Rotate(0, _mouseX * _rotateSpeed, 0);
        }
        else if (Mathf.Abs(_mouseX) < 0.02)
        {
            transform.Rotate(0,_mouseX * _rotateSpeed , 0);
        }
        //�v���C���[�𓮂���
        _move.Normalize();
        _rig.velocity = _move * _moveSpeed;
    }
}
