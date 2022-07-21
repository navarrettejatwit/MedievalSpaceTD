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

    [SerializeField] private GameObject towers = null;

    public GameObject player;

    private TowerFactory[] TowerFactoryArray = new TowerFactory[5];

    private bool canBuildTower = false;

    private int towerType = 0;

    private bool canSellTower = false;
    
    private SlotGrid sg;
    

    private int layermask;
    private int towerCost;

    // Start is called before the first frame update
    void Start()
    {
        layermask = LayerMask.GetMask("Towers");
        sg = (SlotGrid) GameObject.Find("SlotMap").GetComponent(typeof(SlotGrid));
        TowerFactoryArray[0] = new TowerFactory(StandardTowerPrefab, towers);
        TowerFactoryArray[1] = new TowerFactory(RapidFireTowerPrefab, towers);
        TowerFactoryArray[2] = new TowerFactory(SplashTowerPrefab, towers);
        TowerFactoryArray[3] = new TowerFactory(GaussTowerPrefab, towers);
        TowerFactoryArray[4] = new TowerFactory(RayTowerPrefab, towers);
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
                        spawnTower(objectHit, i, j);
                        player.GetComponent<Player>().updateCash(towerCost * -1);
                    }
                }
            }

            if (canSellTower)
            {
                if (Physics.Raycast(ray, out hit, 10f, layermask))
                {
                    Transform objectHit = hit.transform;
                    int cashBack = objectHit.GetComponent<Tower>().cost / 2;
                    int i = (int) objectHit.position.x;
                    int j = (int) objectHit.position.y;
                    //to do get towers income at reduced income price.
                    bool notFilled = sellTower();
                    //to do object pool get rif of Destroy.
                    Destroy(hit.transform.gameObject);
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

    public void spawnTower(Transform coord, int i, int j)
    {
        TowerFactoryArray[towerType].setSpawnPoint(coord);
        Tower NewTower = (Tower) TowerFactoryArray[towerType].produce();
        NewTower.transform.rotation = Quaternion.Euler(270, 0, 0);
        canBuildTower = false;
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
