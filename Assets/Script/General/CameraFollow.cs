using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class CameraFollow : MonoBehaviour
{
    public GameObject Camera;   
    public GameObject FollowObj;

    [SerializeField] bool followFlg = true;


    [Header("searchFase")]
    public Vector3 S_offsetPosition = new Vector3(0.0f, 1.5f, -7.0f);      //相対距離取得用
    public Vector3 S_offsetRotate = new Vector3(30.0f, 0.0f, 0.0f);      //相対距離取得用
    [Header("battleFase")]
    public Vector3 B_offsetPosition = new Vector3(0.0f, 0.0f, -5.0f);      //相対距離取得用
    public Vector3 B_offsetRotate = new Vector3(0.0f, 0.0f, 0.0f);      //相対距離取得用

    [Header("offsetSpeed")]
    public float offsetSpeed = 6.0f;


    // フェーズ確認用にプレイヤーの情報を取得(後に変更予定)
    private GameObject m_playerObj;
    private PlayerController m_playerInfo;


    private Vector3 m_offsetPosition;      //相対距離取得用
    private Vector3 m_offsetRotate;      //相対距離取得用
                                         // Use this for initialization
    void Start()
    {
        // プレイヤー情報を取得＆代入
        m_playerObj = GameObject.FindGameObjectWithTag("Player");
        m_playerInfo = m_playerObj.GetComponent<PlayerController>();
        // MainCamera(自分自身)とplayerとの相対距離を求める
        //offset = transform.position - Camera.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        // フェーズによってオフセットを変更
        if (m_playerInfo.SearchFaseFlg)
        {
            m_offsetPosition = S_offsetPosition;
            m_offsetRotate = S_offsetRotate;
        }
        else
        {
            m_offsetPosition = B_offsetPosition;
            m_offsetRotate = B_offsetRotate;
        }

        // 追従するか判定
        if (!followFlg) return;

        //新しい値を代入する
        Vector3 rotAngle = FollowObj.transform.rotation.eulerAngles + m_offsetRotate;
        Camera.transform.rotation = Quaternion.Euler(rotAngle.x, rotAngle.y, rotAngle.z);
        //
        var offPos = Quaternion.Euler(rotAngle.x, rotAngle.y, rotAngle.z) * m_offsetPosition;
        Camera.transform.position = Vector3.Lerp(Camera.transform.position, FollowObj.transform.position + offPos, offsetSpeed * Time.deltaTime);

    }

    public bool FollowFlg
    {
        get
        {
            return followFlg;
        }
        set
        {
            followFlg = value;
        }
    }
}

