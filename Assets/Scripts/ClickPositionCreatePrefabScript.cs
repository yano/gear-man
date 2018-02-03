using UnityEngine;
using System.Collections;

public class ClickPositionCreatePrefabScript : MonoBehaviour
{
    // 生成したいPrefab
    public GameObject gear8base;
    // クリックした位置座標
    Vector3 clickPosition;

    void Start() { }

    void Update()
    {
        // マウス入力で左クリックをした瞬間
        if (Input.GetMouseButtonDown(0))
        {
            //https://qiita.com/valbeat/items/799a18da3174a6af0b89
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // Rayの当たったオブジェクトの情報を格納する
            RaycastHit hit = new RaycastHit();
            // オブジェクトにrayが当たった時
            if (Physics.Raycast(ray, out hit, 10))
            {
                // rayが当たったオブジェクトの名前を取得
                string objectName = hit.collider.gameObject.name;
                Debug.Log(objectName);

                if (objectName == "Plane")
                {

                    #region 歯車生成

                    // ここでの注意点は座標の引数にVector2を渡すのではなく、Vector3を渡すことである。
                    // Vector3でマウスがクリックした位置座標を取得する
                    clickPosition = Input.mousePosition;
                    // Z軸修正
                    clickPosition.z = 0.0f;
                    // オブジェクト生成 : オブジェクト(GameObject), 位置(Vector3), 角度(Quaternion)
                    // ScreenToWorldPoint(位置(Vector3))：スクリーン座標をワールド座標に変換する
                    Vector3 worldPos = Camera.main.ScreenToWorldPoint(clickPosition);
                    worldPos = new Vector3(worldPos.x, 1.0f, worldPos.z);

                    Instantiate(gear8base, worldPos, Quaternion.identity);

                    #endregion

                }
            }



        }
    }
}
