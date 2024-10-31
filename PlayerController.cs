using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static PlayerController.SkillDefine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private BoxCollider col;
    private Rigidbody rb;
    public float force;
    [SerializeField] GameController gameController;

    private bool isMoving = false;


    private float sceneCount;

    public int SCount = 5;
    public bool IsUsingSkill = false;

    [SerializeField]
    private string tagName = "Enemy"; //インスペクターで変更可能
    [HideInInspector]
    public GameObject searchNearObj;  //最も近いオブジェクト(public修飾子にすることで外部のクラスから参照できる)
    private float searchWaitTime = 0.1f;  //検索までの待機時間

    private float timer = 0f;  //検索までの待機時間検索用

    [Header("特技")]
    public SkillDefine.Skill skill;

    [SerializeField] GameObject barrierPrefab;

    private HighlightArrow highlightArrow;

    public bool IsTurnActive { get; private set; } = false;

    //太田が追加
    public AudioClip skillSE;
    AudioSource audioSource;
    //スキルのパーティクル
    public GameObject SkillParticle;

    public static class SkillDefine
    {
        public enum Skill
        {
            _None,//スキル無し
            DestroyTheNearEnemy,//一番近い敵を破壊
            TheWorld,//ターン遅延
            Barrier,//ヒール
        }
    }

    // Dictionaryで特技データと各データを紐づける
    // 特技名
    public static Dictionary<Skill, string> dic_SkillName = new Dictionary<Skill, string>()
    {
        {Skill._None, "スキル無し"},
        {Skill.DestroyTheNearEnemy, "一番近い敵を破壊" },
        {Skill.TheWorld, "ターン遅延" },
        {Skill.Barrier, "ウォールマリア" },
    };

    // 表示する説明文
    public static Dictionary<Skill, string> dic_SkillInfo = new Dictionary<Skill, string>()
    {
        {Skill._None, "____" },
        {Skill.DestroyTheNearEnemy, "いちばんちかいてきをはかい" },
        {Skill.TheWorld, "てきのたーんを１たーんちえんする" },
        {Skill.Barrier, "てきのこうげきをいっかいまもるばりあをはる" }
    };

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        col = GetComponent<BoxCollider>();
        col.material = new PhysicMaterial { staticFriction = 1, dynamicFriction = 0.1f };

        highlightArrow = GetComponent<HighlightArrow>();

        searchNearObj = Search();
        Debug.Log(searchNearObj);
        Debug.Log("SCount = :" + SCount);
        audioSource = GetComponent<AudioSource>();
    }
    void Update()
    {
        sceneCount -= Time.deltaTime;
        if (!IsTurnActive || isMoving) return;

        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            rb.AddForce(force * Vector3.up);
            isMoving = true;
            SCount--;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            rb.AddForce(force * Vector3.down);
            isMoving = true;
            SCount--;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            rb.AddForce(force * Vector3.right);
            isMoving = true;
            SCount--;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            rb.AddForce(force * Vector3.left);
            isMoving = true;
            SCount--;
        }

        if(SCount <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Alpha3) && gameObject.name == "Wizard")
            {
                UseSkill(Skill.TheWorld);
                IsUsingSkill = true;
                SCount = 5;
            }
        }

        if (SCount <= 2)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && gameObject.name == "Hero")
            {
                UseSkill(Skill.DestroyTheNearEnemy);
                IsUsingSkill = true;
                SCount = 5;
            }
        }

        if (SCount <= 3)
        {
             if (Input.GetKeyDown(KeyCode.Alpha2) && gameObject.name == "Monk")
            {
                UseSkill(Skill.Barrier);
                IsUsingSkill = true;
                SCount = 5;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && gameObject.name == "Hero")
            {
                Debug.Log("まだ使えない...");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && gameObject.name == "Monk")
            {
                Debug.Log("まだ使えない...");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) && gameObject.name == "Wizard")
            {
                Debug.Log("まだ使えない...");
            }
        }
        //太田が追加
        if(IsUsingSkill == true)
        {
            audioSource.PlayOneShot(skillSE);
            IsUsingSkill = false;
        }

        //時間の経過に合わせて自動的に取得する場合

        //時間を計測
        timer += Time.deltaTime;

        //検索の待機時間を経過したら
        if (timer >= searchWaitTime)
        {
            //指定したタグを持つゲームオブジェクトのうち、このゲームオブジェクトに最も近いゲームオブジェクトを1つ取得
            searchNearObj = Search();
            //Debug.Log(searchNearObj);

            //計測時間を初期化、再検索
            searchWaitTime = 0;
        }

        if (IsTurnActive)
        {
            highlightArrow.ShowArrow();
        }
        else
        {
            highlightArrow.HideArrow();
        }
    }

    /// <summary>
    /// 指定されたタグの中で最も近いものを取得
    /// </summary>
    /// <returns></returns>
    private GameObject Search()
    {
        return GameObject.FindGameObjectsWithTag(tagName)
            .OrderBy(Obj => Vector3.Distance(Obj.transform.position, transform.position))
            .FirstOrDefault();
    }

    public void Death()
    {
        //// プレイヤーを透明にし、当たり判定を無効にする
        //Renderer renderer = GetComponent<Renderer>();
        //Collider collider = GetComponent<Collider>();

        //if (renderer != null)
        //{
        //    renderer.enabled = false; // プレイヤーを透明にする
        //}

        //if (collider != null)
        //{
        //    collider.enabled = false; // 当たり判定を無効にする
        //}

        // プレイヤーが死んだことを知らせる
        Debug.Log($"プレイヤー{gameObject.name}が死にました");
        EndTurn();

        // プレイヤー数を減らす
        GameController gameController = FindObjectOfType<GameController>();
        if (gameController != null)
        {
            gameController.playerCount--;
        }

        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("壁に衝突");
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            EndTurn();
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("仲間に衝突");
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            EndTurn();
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Transform shield = transform.Find("Barrier");
            if (shield != null && shield.gameObject.activeSelf)
            {
                Debug.Log("Shield is active, destroying shield.");
                Destroy(shield.gameObject);
            }
            else
            {
                Debug.Log("No active shield, destroying player.");
                Death();
            }
        }

        if (collision.gameObject.CompareTag("Goal"))
        {
            gameController.nextSceneflag = true;
            force = 0;
            gameController.ClearCanvas.SetActive(true);
        }

    }


    void UseSkill(Skill skill)
    {
        Debug.Log("Using skill: " + dic_SkillName[skill]);
        switch (skill)
        {
            case Skill.DestroyTheNearEnemy:
                // 全敵にダメージ処理
                //Destroy(searchNearObj,1);
                //太田が追加
                //if(searchNearObj.GetComponent<EnemyPingPongController>())
                //{
                //    searchNearObj.GetComponent<EnemyPingPongController>().enabled = false;
                //}
                
                transform.GetChild(0).gameObject.SetActive(true);
                Invoke("HeroAttackReset", 0.5f);
                
                break;
            case Skill.TheWorld:
                // ターン遅延処理
                StopAllEnemies();
                break;
            case Skill.Barrier:
                //  バリア処理
                AddBarrierToAllPlayers();
                break;
            default:
                break;
        }
    }
    //太田が追加
    void HeroAttackReset()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    void StopAllEnemies()
    {
        EnemyPingPongController[] enemies = FindObjectsOfType<EnemyPingPongController>();
        foreach (EnemyPingPongController enemy in enemies)
        {
            enemy.StopEnemy(10f);
        }
    }

    void AddBarrierToAllPlayers()
    {
        GameObject[] allPlayers = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in allPlayers)
        {
            Transform existingBarrier = player.transform.Find("Barrier");
            if (existingBarrier != null)
            {
                existingBarrier.gameObject.SetActive(true);
            }
            else
            {
                GameObject barrierInstance = Instantiate(barrierPrefab, player.transform);
                barrierInstance.name = "Barrier";
                barrierInstance.transform.localPosition = Vector3.zero; // 必要に応じて位置を調整

            }
        }
    }

    public void StartTurn()
    {
        highlightArrow.ShowArrow();
        Debug.Log("SCount = :" + SCount);
        IsTurnActive = true;
    }

    void EndTurn()
    {
        IsTurnActive = false;
        highlightArrow.HideArrow();
        isMoving = false;
        gameObject.transform.position = new Vector3(Mathf.RoundToInt(gameObject.transform.position.x), Mathf.RoundToInt(gameObject.transform.position.y), gameObject.transform.position.z);
        GameController gameController = FindObjectOfType<GameController>();
        gameController.EndPlayerTurn();
    }

    public interface IDamageable
    {
        void Damage(int value);
        void Death();
    }
}
