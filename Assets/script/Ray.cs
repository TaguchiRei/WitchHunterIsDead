using UniGLTF;
using UnityEngine;

public class Ray : MonoBehaviour
{
    [SerializeField] Transform _enemy;             // �������g
    [SerializeField] float distance = 0.8f;    // ���o�\�ȋ���
    [SerializeField] private float _sightAngle;//����p
    [SerializeField] private float _maxDistance = float.PositiveInfinity;//����̋���
    GameObject[] _target;
    void Update()
    {

    }
    private void FixedUpdate()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        
    }
    /// <summary>
    /// ���E���̍ł��ߋ����ł����F�\�ȃI�u�W�F�N�g�̍��W��Vector3�^�ŕԂ�
    /// </summary>
    /// <returns></returns>
    public Vector3 IsVisible()
    {
        _target = GameObject.FindGameObjectsWithTag("PlayerTrace");
        // ���g�̈ʒu
        var selfPos = _enemy.position;
        var targetPos = new Vector3(999, 999, 999);
        // �^�[�Q�b�g�̈ʒu�����߂�ۂ�_target�̒��̈�ԋ߂��I�u�W�F�N�g������
        for (int i = 0; i < _target.Length; i++)
        {
            float dis1 = Vector3.Distance(transform.position, _target[i].transform.position);
            float dis2 = Vector3.Distance(transform.position, targetPos);
            if (dis1 < dis2)
            {
                var see = CanSee();
                if(see == true)
                {
                    bool canSee = SeeRange(targetPos,selfPos);
                    if(canSee == true)
                    {
                        //�����ɓ����
                        targetPos = _target[i].transform.position;
                    }
                }
            }
        }

        if(targetPos != new Vector3(999, 999, 999))
        {
            return (targetPos);
        }
        else
        {
            return (new Vector3(0, 0, 0));
        }
    }

    /// <summary>
    /// �������I�u�W�F�N�g�����F�\���ǂ��������m����
    /// ���F�\�Ȃ�true�A�s�Ȃ�false��Ԃ�
    /// �m�F�Ώۂ̃I�u�W�F�N�g�̍��W�����Ďg�p�B
    /// </summary>
    /// <returns></returns>
    public bool CanSee(Vector3 vec3 = new Vector3())
    {
        // Ray�̓I�u�W�F�N�g�̊炮�炢�̍�������o���B
        var rayStartPosition = new Vector3(_enemy.transform.position.x, _enemy.transform.position.y + 1.3f, _enemy.transform.position.z);
        // Ray�̓J�����������Ă�����ɂƂ΂�
        var rayDirection = _enemy.transform.forward.normalized;

        // Hit�����I�u�W�F�N�g�i�[�p
        RaycastHit raycastHit;

        // Ray���΂��iout raycastHit ��Hit�����I�u�W�F�N�g���擾����j
        var isHit = Physics.Raycast(rayStartPosition, rayDirection, out raycastHit, distance);

        // Debug.DrawRay (Vector3 start(ray���J�n����ʒu), Vector3 dir(ray�̕����ƒ���), Color color(���C���̐F));
        Debug.DrawRay(rayStartPosition, rayDirection * distance, Color.red);
        Debug.DrawLine(rayStartPosition, rayDirection * distance, Color.red);

        // �Ȃɂ������o������
        if (isHit)
        {
            return true;
        }else
        {
            return false;
        }
    }

    /// <summary>
    /// ���E���Ɍ����邩�𔻒�
    /// �������ɂ̓^�[�Q�b�g�̍��W���A�������ɂ͎������g�̍��W��������
    /// </summary>
    /// <param name="targetPos"></param>
    /// <param name="selfPos"></param>
    /// <returns></returns>
    bool SeeRange(Vector3 targetPos,Vector3 selfPos)
    {
        // ���g�̌����i���K�����ꂽ�x�N�g���j
        var selfDir = _enemy.forward;

        //�x�N�g���̌v�Z
        var targetDir = targetPos - selfPos;
        targetDir.Normalize();
        //�I�u�W�F�N�g�܂ł̋���
        float targetDistance = Vector3.Distance(targetPos, selfPos);

        // cos(��/2)���v�Z
        var cosHalf = Mathf.Cos(_sightAngle / 2 * Mathf.Deg2Rad);

        // ���g�ƃ^�[�Q�b�g�ւ̌����̓��όv�Z
        // �^�[�Q�b�g�ւ̌����x�N�g���𐳋K������K�v�����邱�Ƃɒ���
        var innerProduct = Vector3.Dot(selfDir, targetDir.normalized);
        //�����邩�ǂ����𔻒�B������Ȃ�true�����B
        bool canSee = innerProduct > cosHalf && targetDistance < _maxDistance;
        return canSee;
    }

}
