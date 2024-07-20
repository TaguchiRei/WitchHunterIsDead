using UnityEngine;

public class Ray : MonoBehaviour
{
    [SerializeField] Transform enemy;             // カメラ
    [SerializeField] float distance = 0.8f;    // 検出可能な距離
    void Update()
    {
        // Rayはオブジェクトの顔ぐらいの高さから出す。
        var rayStartPosition = new Vector3( enemy.transform.position.x,enemy.transform.position.y+1.3f,enemy.transform.position.z);
        // Rayはカメラが向いてる方向にとばす
        var rayDirection = enemy.transform.forward.normalized;

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
            // LogにHitしたオブジェクト名を出力
            Debug.Log("HitObject : " + raycastHit.collider.gameObject.name);
        }
    }
}
