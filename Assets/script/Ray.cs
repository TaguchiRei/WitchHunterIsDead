using UnityEngine;

public class Ray : MonoBehaviour
{
    [SerializeField] Transform enemy;             // �J����
    [SerializeField] float distance = 0.8f;    // ���o�\�ȋ���
    void Update()
    {
        // Ray�̓I�u�W�F�N�g�̊炮�炢�̍�������o���B
        var rayStartPosition = new Vector3( enemy.transform.position.x,enemy.transform.position.y+1.3f,enemy.transform.position.z);
        // Ray�̓J�����������Ă�����ɂƂ΂�
        var rayDirection = enemy.transform.forward.normalized;

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
            // Log��Hit�����I�u�W�F�N�g�����o��
            Debug.Log("HitObject : " + raycastHit.collider.gameObject.name);
        }
    }
}
