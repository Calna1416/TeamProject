using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        [Header("GameObject")]
        public GameObject PlayerSpriteObj;

        [Header("MovementKey")]
        public KeyCode MoveUpKey = KeyCode.W;
        public KeyCode MoveDownKey = KeyCode.S;
        public KeyCode MoveLeftKey = KeyCode.A;
        public KeyCode MoveRightKey = KeyCode.D;

        [Header("MovementSpeed")]
        public float MoveSpeed = 1.0f;
        public float JumpSpeed = 20.0f;
        public float Gravity = 50.0f;
        public float AirResistans = 40.0f;

        private bool searchFaseFlg = true;

        //========================================
        // privete
        //========================================
        private CharacterController m_characterController;
        private Vector3 m_moveVec = Vector3.zero;


        //==========================================
        // デバッグ用変数
        //==========================================
#if UNITY_EDITOR


#endif

        // Start is called before the first frame update
        void Start()
        {
            m_characterController = GetComponent<CharacterController>();
        }

        // Update is called once per frame
        void Update()
        {
            //==========================================
            // デバッグ機能
            //==========================================
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.F))
            {
                searchFaseFlg = !searchFaseFlg;
            }
#endif

            Movement();

            // 重力
            m_moveVec.y -= Gravity * Time.deltaTime;

            Vector3 globaldir = transform.TransformDirection(m_moveVec);
            m_characterController.Move(globaldir * Time.deltaTime);

            //キャラクターの回転
            if (m_characterController.velocity.x < 0)
            {
                if (Mathf.Abs(PlayerSpriteObj.transform.rotation.y) <= 0.1)
                {
                    PlayerSpriteObj.transform.Rotate(new Vector3(0, 180, 0));
                }
            }
            else if (m_characterController.velocity.x > 0)
            {
                if (Mathf.Abs(PlayerSpriteObj.transform.rotation.y) >= 0.9)
                {
                    PlayerSpriteObj.transform.Rotate(new Vector3(0, -180, 0));
                }
            }
        }

        private void Movement()
        {
            // 通常歩行
            if (m_characterController.isGrounded)
            {
                m_moveVec = Vector3.zero;


                if (Input.GetKey(MoveRightKey))
                {
                    m_moveVec.x += MoveSpeed;
                }
                if (Input.GetKey(MoveLeftKey))
                {
                    m_moveVec.x += -MoveSpeed;
                }
                if (searchFaseFlg)
                {
                    if (Input.GetKey(MoveUpKey))
                    {
                        m_moveVec.z += MoveSpeed;
                    }
                    if (Input.GetKey(MoveDownKey))
                    {
                        m_moveVec.z += -MoveSpeed;
                    }

                }
                else
                {
                    if (Input.GetKey(MoveUpKey))
                    {
                        m_moveVec.y = JumpSpeed;
                    }
                }

            }
            else //ジャンプ中
            {
                if (searchFaseFlg)
                {
                    if (Input.GetKey(MoveUpKey))
                    {
                        m_moveVec.z += MoveSpeed / AirResistans;
                        if (Mathf.Abs(m_moveVec.z) >= MoveSpeed)
                        {
                            m_moveVec.z = MoveSpeed;
                        }
                    }
                    if (Input.GetKey(MoveDownKey))
                    {
                        m_moveVec.z += -MoveSpeed / AirResistans;
                        if (Mathf.Abs(m_moveVec.z) >= MoveSpeed)
                        {
                            m_moveVec.z = -MoveSpeed;
                        }
                    }
                }

                if (Input.GetKey(MoveRightKey))
                {
                    m_moveVec.x += MoveSpeed / AirResistans;
                    if (Mathf.Abs(m_moveVec.x) >= MoveSpeed)
                    {
                        m_moveVec.x = MoveSpeed;
                    }
                }
                if (Input.GetKey(MoveLeftKey))
                {
                    m_moveVec.x += -MoveSpeed / AirResistans;
                    if (Mathf.Abs(m_moveVec.x) >= MoveSpeed)
                    {
                        m_moveVec.x = -MoveSpeed;
                    }
                }
            }

        }

        public bool SearchFaseFlg
        {
            get
            {
                return searchFaseFlg;
            }
            set
            {
                searchFaseFlg = value;
            }
        }
    }
}

