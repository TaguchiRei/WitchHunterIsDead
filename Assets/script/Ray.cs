using UniGLTF;
using UnityEngine;

public class Ray : MonoBehaviour
{
    [SerializeField] Transform _enemy;             // 自分自身
    [SerializeField] float distance = 0.8f;    // 検出可能な距離
    [SerializeField] private float _sightAngle;//視野角
    [SerializeField] private float _maxDistance = float.PositiveInfinity;//視野の距離
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
    /// 視界内の最も近距離でかつ視認可能なオブジェクトの座標をVector3型で返す
    /// </summary>
    /// <returns></returns>
    public Vector3 IsVisible()
    {
        _target = GameObject.FindGameObjectsWithTag("PlayerTrace");
        // 自身の位置
        var selfPos = _enemy.position;
        var targetPos = new Vector3(999, 999, 999);
        // ターゲットの位置を決める際に_targetの中の一番近いオブジェクトを入れる
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
                        //ここに入れる
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
    /// 見つけたオブジェクトが視認可能かどうかを検知する
    /// 視認可能ならtrue、不可ならfalseを返す
    /// 確認対象のオブジェクトの座標を入れて使用。
    /// </summary>
    /// <returns></returns>
    public bool CanSee(Vector3 vec3 = new Vector3())
    {
        // Rayはオブジェクトの顔ぐらいの高さから出す。
        var rayStartPosition = new Vector3(_enemy.transform.position.x, _enemy.transform.position.y + 1.3f, _enemy.transform.position.z);
        // Rayはカメラが向いてる方向にとばす
        var rayDirection = _enemy.transform.forward.normalized;

        // Hitしたオブジェクト格納用
        RaycastHit raycastHit;

        // Rayを飛ばす（out raycastHit でHitしたオブジェクトを取得する）
        var isHit = Physics.Raycast(rayStartPosition, rayDirection, out raycastHit, distance);

        // Debug.DrawRay (Vector3 start(rayを開始する位置), Vector3 dir(rayの方向と長さ), Color color(ラインの色));
        Debug.DrawRay(rayStartPosition, rayDirection * distance, Color.red);
        Debug.DrawLine(rayStartPosition, rayDirection * distance, Color.red);

        // なにかを検出したら
        if (isHit)
        {
            return true;
        }else
        {
            return false;
        }
    }

    /// <summary>
    /// 視界内に見えるかを判定
    /// 第一引数にはターゲットの座標を、第二引数には自分自身の座標を代入する
    /// </summary>
    /// <param name="targetPos"></param>
    /// <param name="selfPos"></param>
    /// <returns></returns>
    bool SeeRange(Vector3 targetPos,Vector3 selfPos)
    {
        // 自身の向き（正規化されたベクトル）
        var selfDir = _enemy.forward;

        //ベクトルの計算
        var targetDir = targetPos - selfPos;
        targetDir.Normalize();
        //オブジェクトまでの距離
        float targetDistance = Vector3.Distance(targetPos, selfPos);

        // cos(θ/2)を計算
        var cosHalf = Mathf.Cos(_sightAngle / 2 * Mathf.Deg2Rad);

        // 自身とターゲットへの向きの内積計算
        // ターゲットへの向きベクトルを正規化する必要があることに注意
        var innerProduct = Vector3.Dot(selfDir, targetDir.normalized);
        //見えるかどうかを判定。見えるならtrueを代入。
        bool canSee = innerProduct > cosHalf && targetDistance < _maxDistance;
        return canSee;
    }

}
