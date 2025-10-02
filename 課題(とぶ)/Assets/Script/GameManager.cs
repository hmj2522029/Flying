using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-5)] // 他のスクリプトよりも先に実行されるようにする
public class GameManager : MonoBehaviour
{
    [Header("ポータル関連")]
    [SerializeField] GameObject PortalPrefabGreen;
    [SerializeField] GameObject PortalPrefabPurple;

    [SerializeField] FadeUI GameClearUI;       //ゲームクリア時のUI
    [SerializeField] FadeUI GameSceneTransitionUI;   //シーン遷移を促すためのUI

    public bool isClear = false; 
    private GameObject[] Portal = new GameObject[2]; //0:緑 1:紫
    private int PortalIndex = 0;                     //次に出すポータルの色(0:緑 1:紫)

    private Quaternion rotation = Quaternion.identity;

    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); //シーンが変わっても破棄されないようにする
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        SceneTransition();
    }



    private void OnEnable()
    {
        Bullet.OnBulletHit += SpawnPortal; //PlayerスクリプトのOnShootイベントにCreatePortalメソッドを登録
        OpenDoor.OnClear += Clear; 

        //シーンが変わったら呼ばれる
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        Bullet.OnBulletHit -= SpawnPortal; //PlayerスクリプトのOnShootイベントからCreatePortalメソッドを解除
        OpenDoor.OnClear -= Clear;


        SceneManager.sceneLoaded -= OnSceneLoaded;
    }



    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //シーンが変わったらポータルをリセット
        Portal[0] = null;
        Portal[1] = null;
        PortalIndex = 0;

        //シーンが変わったらプレイヤーを探す
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
        if (player != null)
        {
            player.LastPortal = null; //プレイヤーの最後に通ったポータルをリセット
            player.PortalCooldownTime = 0.5f; //ポータルのクールダウン時間をリセット
        }

        if (SceneManager.GetActiveScene().name == "TitleScene")
        {
            isClear = false;
        }

        if (SceneManager.GetActiveScene().name == "Game")
        {

            GameClearUI = GameObject.Find("GameClearUI").GetComponent<FadeUI>();
            GameSceneTransitionUI = GameObject.Find("GameSceneTransition").GetComponent<FadeUI>();
        }

    }

    void SpawnPortal(Vector2 hit, Vector2 noraml)
    {
        if (Mathf.Abs(noraml.x) > Mathf.Abs(noraml.y)) //壁に対して水平に当たった場合
        {
            if (noraml.x > 0) //右の壁に当たった場合
            {
                rotation = Quaternion.Euler(0, 0, 180);
            }
            else //左の壁に当たった場合
            {
                rotation = Quaternion.Euler(0, 0, 0);
            }
        }
        else //床か天井に対して垂直に当たった場合
        {
            if (noraml.y > 0) //床に当たった場合
            {
                rotation = Quaternion.Euler(0, 0, 90);
            }
            else //天井に当たった場合
            {
                rotation = Quaternion.Euler(0, 0, 270);
            }
        }

        if (Portal[PortalIndex] != null) //すでにポータルが存在する場合は削除
        {
            Portal[PortalIndex].GetComponent<Portal>().SetDisappear(true); //消えるアニメーションを再生
        }


        if (PortalIndex == 0) //緑のポータルを出す
        {
            Portal[PortalIndex] = Instantiate(PortalPrefabGreen, hit, rotation);
            PortalIndex = 1; //次は紫のポータルを出す
        }
        else //紫のポータルを出す
        {
            Portal[PortalIndex] = Instantiate(PortalPrefabPurple, hit, rotation);
            PortalIndex = 0; //次は緑のポータルを出す
        }

        //相互リンクの設定
        if (Portal[0] != null && Portal[1] != null)
        {
            Portal[0].GetComponent<Portal>().OppositePortal = Portal[1].GetComponent<Portal>();
            Portal[1].GetComponent<Portal>().OppositePortal = Portal[0].GetComponent<Portal>();
        }
    }

    private void Clear()
    {
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        isClear = true;
        GameClearUI.FadeOut();
        yield return new WaitForSeconds(2.0f);
        GameSceneTransitionUI.FadeOut();
    }

    private void SceneTransition()
    {
        if (isClear && Input.GetKey(KeyCode.Space))
        {
            SceneManager.LoadScene("TitleScene");
        }
        else if (!isClear && Input.GetKey(KeyCode.Space) && SceneManager.GetActiveScene().name == "TitleScene")
        {
            SceneManager.LoadScene("Game");
        }
    }

}
  