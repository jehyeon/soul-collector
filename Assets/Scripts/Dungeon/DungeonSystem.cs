using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonSystem : MonoBehaviour
{
    private int floor;

    [SerializeField]
    DungeonGenerator generator;
    [SerializeField]
    private float roomWidth = 20f;      // DungeonGenerator와 동일하게 설정해야 함

    [SerializeField]
    private GameObject spawnerParent;

    // Portal prefab
    [SerializeField]
    private GameObject portalPref;
    
    // Spawner prefab
    [SerializeField]
    private GameObject spawnerPref;

    private GameManager gameManager;

    public int Floor { get { return floor; } }

    private void Start()
    {
        if (gameManager == null)
        {
            gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        }
        floor = gameManager.Floor;
        Load();
    }
    // -------------------------------------------------------------
    // Level design
    // -------------------------------------------------------------

    public void Load()
    {
        // 현재 Floor 기준으로 레벨 디자인
        // !!! 레벨 디자인 클래스 만들기
        CreateDungeon();

        CreatePortal(0, false);
        CreatePortal(1, true);

        CreateSpawners(1f);
    }

    // -------------------------------------------------------------
    // 던전 생성
    // -------------------------------------------------------------
    private void CreateDungeon()
    {
        // 맵 생성
        generator.Generate();
        generator.GenerateWalls();
    }

    private void ClearDungeon()
    {
        // 맵 삭제
        generator.Clear();
        generator.ClearWalls();
    }

    // -------------------------------------------------------------
    // 포탈 생성
    // -------------------------------------------------------------
    private void CreatePortal(int roomNumber, bool isNextFloorPortal = true)
    {
        GameObject portalObject = Instantiate(
            portalPref, 
            new Vector3(
                generator.Rooms[roomNumber].X * roomWidth,
                0f,
                generator.Rooms[roomNumber].Y * roomWidth
            ),
            Quaternion.Euler(new Vector3(0, 90f, 0))
        );

        if (isNextFloorPortal)
        {
            // 다음 층 포탈
            portalObject.GetComponent<Portal>().Set(2);
        }
        else
        {
            // 마을로 돌아가는 포탈
            portalObject.GetComponent<Portal>().Set(1, true);   // true: 플레이어가 한번 통과해야 활성화
        }
    }
    // -------------------------------------------------------------
    // 스포너 생성
    // -------------------------------------------------------------
    private void CreateSpawners(float percent)
    {
        // percent: Enemy가 나오는 방 비율
        for (int i = 2; i < generator.Rooms.Count; i++)
        {
            // !!! 임시로 2번방부터 마지막 방까지 스포너 생성
            if (Random.value < percent)
            {
                GameObject spawnerObject = Instantiate(
                    spawnerPref,
                    new Vector3(
                        generator.Rooms[i].X * roomWidth,
                        0f,
                        generator.Rooms[i].Y * roomWidth
                    ),
                    Quaternion.identity
                );
                spawnerObject.name = i.ToString();
                spawnerObject.transform.parent = spawnerParent.transform;
                spawnerObject.GetComponent<Spawner>().Set(gameManager, roomWidth, 1, 15f);
            }
        }
    }
}
