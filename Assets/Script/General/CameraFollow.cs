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
    public Vector3 S_offsetPosition = new Vector3(0.0f, 1.5f, -7.0f);      //���΋����擾�p
    public Vector3 S_offsetRotate = new Vector3(30.0f, 0.0f, 0.0f);      //���΋����擾�p
    [Header("battleFase")]
    public Vector3 B_offsetPosition = new Vector3(0.0f, 0.0f, -5.0f);      //���΋����擾�p
    public Vector3 B_offsetRotate = new Vector3(0.0f, 0.0f, 0.0f);      //���΋����擾�p

    [Header("offsetSpeed")]
    public float offsetSpeed = 6.0f;


    // �t�F�[�Y�m�F�p�Ƀv���C���[�̏����擾(��ɕύX�\��)
    private GameObject m_playerObj;
    private PlayerController m_playerInfo;


    private Vector3 m_offsetPosition;      //���΋����擾�p
    private Vector3 m_offsetRotate;      //���΋����擾�p
                                         // Use this for initialization
    void Start()
    {
        // �v���C���[�����擾�����
        m_playerObj = GameObject.FindGameObjectWithTag("Player");
        m_playerInfo = m_playerObj.GetComponent<PlayerController>();
        // MainCamera(�������g)��player�Ƃ̑��΋��������߂�
        //offset = transform.position - Camera.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        // �t�F�[�Y�ɂ���ăI�t�Z�b�g��ύX
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

        // �Ǐ]���邩����
        if (!followFlg) return;

        //�V�����l��������
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

