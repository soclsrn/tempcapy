using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameScene : BaseScene
{
    private GameObject towerPrefab;
    [SerializeField]
    private GameObject[,] gridObjects = new GameObject[4, 4];
    
    public void SetTowerPrefab(string towerPrefabPath)
    {
        // Debug.Log("Set Tower Prefab");
        towerPrefab = Resources.Load<GameObject>("Prefabs/"+towerPrefabPath);
    }

    protected override bool Init()
    {
        if (base.Init() == false)
			return false;
        // Debug.Log("Init Game Scene");

        Managers.UI.ShowPopupUI<UI_GamePopup>();
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                gridObjects[i, j] = GameObject.Find($"{i}_{j}");
            }
        }
        towerPrefab = Resources.Load<GameObject>("Prefabs/Tower");

        //



        //
        StartCoroutine(fuckingplayer());
        return true;
    }
    IEnumerator fuckingplayer()
    {
        yield return new WaitForSeconds(0.1f);
        FindObjectOfType<RoomNetworkManager>().setplayer();
    }
    private void Update()
    {
        //// Debug.Log("Update Game Scene");
        //if (Input.GetMouseButtonDown(0))
        //{
        //    // Debug.Log("Mouse Clicked");
        //    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //    RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
        //    if (hit.collider != null && hit.collider.tag == "Grid" && hit.collider.GetComponent<Grid>().isNull())
        //    {
        //        // Debug.Log(hit.collider.name);
        //        GameObject tower = Instantiate(towerPrefab, hit.collider.transform.position, Quaternion.identity);
        //        tower.GetComponent<Tower>().SetTile(hit.collider.gameObject);
        //        hit.collider.GetComponent<Grid>().setTileObject(tower);
        //    }
        //}
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    // Debug.Log("Space Key Pressed");
        //    //Managers.UI.ShowPopupUI<UI_SkillRewardPopup>();
        //    Managers.UI.ShowPopupUI<UI_CardGettingPopup>();
        //}
    }

    public override void Clear()
    {
        Debug.Log("Clear Game Scene");
    }

    public void SummonMonsterWave(int level)
    {
        Debug.Log("Summon Monster Wave");
        
        for (int i = 0; i < level; i++)
        {
            SummonMoster();
        }

    }

    public void SummonMoster(GameObject monsterPrefab= null){
        var spawnMargin = 0.1f;
        if(monsterPrefab == null)
            monsterPrefab = Resources.Load<GameObject>("Prefabs/Enemy");
        //summon monster off-screen
        
        // 화면 경계를 넘어서는 랜덤 위치 계산
        float randomValue = Random.value; // 0과 1 사이의 랜덤한 값
        Vector2 spawnPosition = Vector2.zero;

        if (randomValue < 0.25f) // 화면 위쪽
        {
            spawnPosition = Camera.main.ViewportToWorldPoint(new Vector2(Random.Range(0f, 1f), 1 + spawnMargin));
        }
        else if (randomValue < 0.5f) // 화면 아래쪽
        {
            spawnPosition = Camera.main.ViewportToWorldPoint(new Vector2(Random.Range(0f, 1f), 0 - spawnMargin));
        }
        else if (randomValue < 0.75f) // 화면 오른쪽
        {
            spawnPosition = Camera.main.ViewportToWorldPoint(new Vector2(1 + spawnMargin, Random.Range(0f, 1f)));
        }
        else // 화면 왼쪽
        {
            spawnPosition = Camera.main.ViewportToWorldPoint(new Vector2(0 - spawnMargin, Random.Range(0f, 1f)));
        }

        // 계산된 위치에 몬스터 소환
        Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
    }
}