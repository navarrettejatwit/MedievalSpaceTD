using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField] private Tower StandardTowerPrefab;

    [SerializeField] private Tower RapidFireTowerPrefab;

    [SerializeField] private Tower SplashTowerPrefab;

    [SerializeField] private Tower GaussTowerPrefab;

    [SerializeField] private Tower RayTowerPrefab;

    [SerializeField] private GameObject towers;

    [SerializeField] private GameObject BallistaPool;
    
    [SerializeField] private GameObject CatapultPool;
    
    [SerializeField] private GameObject CannonPool;
    
    [SerializeField] private GameObject GaussPool;
    
    [SerializeField] private GameObject WizardPool;

    public GameObject player;

    private TowerFactory[] TowerFactoryArray = new TowerFactory[5];

    private GameObject[] TowerPool = new GameObject[5];

    private GameObject currentTower;

    private bool canBuildTower = false;

    private int towerType = 0;

    private bool canSellTower = false;

    private int cashBack;
    
    private SlotGrid sg;
    
    private int layermask;
    
    private int towerCost;

    // Start is called before the first frame update
    void Start()
    {
        layermask = LayerMask.GetMask("Towers");
        sg = (SlotGrid) GameObject.Find("SlotMap").GetComponent(typeof(SlotGrid));
        TowerPool[0] = BallistaPool;
        TowerPool[1] = CatapultPool;
        TowerPool[2] = CannonPool;
        TowerPool[3] = GaussPool;
        TowerPool[4] = WizardPool;
        TowerFactoryArray[0] = new TowerFactory(StandardTowerPrefab, BallistaPool);
        TowerFactoryArray[1] = new TowerFactory(RapidFireTowerPrefab, CatapultPool);
        TowerFactoryArray[2] = new TowerFactory(SplashTowerPrefab, CannonPool);
        TowerFactoryArray[3] = new TowerFactory(GaussTowerPrefab, GaussPool);
        TowerFactoryArray[4] = new TowerFactory(RayTowerPrefab, WizardPool);
    }

    // Update is called once per frame
    void Update()
    {
        //To do if build tower buttons pressed and wait or not pressed.
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (canBuildTower)
            {
                if (Physics.Raycast(ray, out hit))
                {
                    Transform objectHit = hit.transform;
                    int i = (int) objectHit.position.x;
                    int j = (int) objectHit.position.y;
                    if (sg.setSlot(i,j)){
                        spawnTower(objectHit);
                        player.GetComponent<Player>().updateCash(towerCost * -1);
                    }
                }
            }

            if (canSellTower)
            {
                if (Physics.Raycast(ray, out hit, 10f, layermask))
                {
                    Transform objectHit = hit.transform;
                    cashBack = objectHit.GetComponent<Tower>().cost / 2;
                    int i = (int) objectHit.position.x;
                    int j = (int) objectHit.position.y;
                    bool notFilled = sellTower();
                    hit.transform.gameObject.SetActive(false);
                    player.GetComponent<Player>().updateCash(cashBack);
                    canSellTower = false;
                }
            }


        }
    }

    public void BallistaTowerButton()
    {
        towerCost = StandardTowerPrefab.cost;
        if (player.GetComponent<Player>().hasCashRequired(towerCost))
        {
            canBuildTower = true;
            towerType = 0;
        }
    }

    public void RapidFireTowerButton()
    {
        towerCost = RapidFireTowerPrefab.cost;
        if (player.GetComponent<Player>().hasCashRequired(towerCost))
        {
            canBuildTower = true;

            towerType = 1;
        }
    }

    public void SplashTowerButton()
    {
        towerCost = SplashTowerPrefab.cost;
        if (player.GetComponent<Player>().hasCashRequired(towerCost))
        {
            canBuildTower = true;

            towerType = 2;
        }
    }

    public void GaussTowerButton()
    {
        towerCost = GaussTowerPrefab.cost;
        if (player.GetComponent<Player>().hasCashRequired(towerCost))
        {
            canBuildTower = true;

            towerType = 3;
        }
    }

    public void RayTowerButton()
    {
        towerCost = RayTowerPrefab.cost;
        if (player.GetComponent<Player>().hasCashRequired(towerCost))
        {
            canBuildTower = true;

            towerType = 4;
        }
    }

    private void spawnTower(Transform coord)
    {
        currentTower = getTowerFromAPool();
        if(currentTower != null)
        {
            currentTower.transform.position = new Vector3(coord.position.x,coord.position.y,coord.position.z);
            currentTower.transform.rotation = Quaternion.Euler(270f, 0f,0f);
			currentTower.GetComponent<Tower>().resetTower();
            currentTower.SetActive(true);
        }
        else
        {
            TowerFactoryArray[towerType].setSpawnPoint(coord);
            Tower NewTower = (Tower) TowerFactoryArray[towerType].produce();
            NewTower.transform.rotation = Quaternion.Euler(270, 0, 0);
        }
        canBuildTower = false;
    }

    private GameObject getTowerFromAPool()
    {
        if (towers.gameObject.transform.GetChild(towerType).transform.childCount != 0)
        {
            for (int i = 0; i < towers.gameObject.transform.GetChild(towerType).transform.childCount; i++)
            {
                if (!towers.gameObject.transform.GetChild(towerType).GetChild(i).gameObject.activeInHierarchy)
                {
                    return towers.gameObject.transform.GetChild(towerType).GetChild(i).gameObject;
                }
            }
        }
        return null;
    }

    public void sellTowerButtonClicked()
    {
        canSellTower = true;
    }

    public bool sellTower()
    {
        return false;
    }
}
