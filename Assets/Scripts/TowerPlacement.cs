using UnityEngine;
using System.Collections;

public class TowerPlacement : MonoBehaviour
{
    public GameObject PlacementAreaPrefab;
    private bool _placingTower = false;
    private GameObject _towerToSpawn;
    private int _towerPrice;
    private GameObject _placementArea;
	
    public void SelectTower(GameObject Tower)
    {
        if (Tower.GetComponent<Tower>().Price <= GameObject.Find("GameController").GetComponent<GameController>().PlayerGold)
        {
            _towerToSpawn = Tower;
            _placingTower = true;
            _towerPrice = Tower.GetComponent<Tower>().Price;
            _placementArea = Instantiate(PlacementAreaPrefab) as GameObject;
            
        }
        
    }

    void Update()
    {

        if(_placingTower)
        {
            _placementArea.transform.position = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            Vector2 rayPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            RaycastHit2D[] hit = Physics2D.RaycastAll(rayPos, Vector2.zero, 0f);

            bool isInPlaceableArea = false, cantplace = false;

            foreach (RaycastHit2D r in hit)
            {

                if (r.collider.tag == "TowerPlacementArea")
                {
                    isInPlaceableArea = true;
                 
                }
                if (r.collider.tag == "CantPlaceHere")
                {
                    cantplace = true;
                }
            }



            if (cantplace == false && isInPlaceableArea)
            {
                _placementArea.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 0.5f);

            }
            else
            {
                _placementArea.GetComponent<SpriteRenderer>().color = new Color(1,0 , 0, 0.5f);
            }
        }

        if (_placingTower && Input.GetMouseButtonDown(0))
        {
            Vector2 rayPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            RaycastHit2D[] hit = Physics2D.RaycastAll(rayPos, Vector2.zero, 0f);

            Vector3 hitpointForPlacement = Vector3.zero;
            bool isInPlaceableArea = false, cantplace = false;

            foreach (RaycastHit2D r in hit)
            {

                if (r.collider.tag == "TowerPlacementArea")
                {
                    isInPlaceableArea = true;
                    hitpointForPlacement = r.point;
                }
                if (r.collider.tag == "CantPlaceHere")
                {
                    cantplace = true;
                }
            }

        

            if(cantplace == false && isInPlaceableArea)
            { 
                GameObject tower = Instantiate(_towerToSpawn) as GameObject;
                tower.transform.position = hitpointForPlacement;

                GameObject.Find("GameController").GetComponent<GameController>().ModifyGold(-_towerPrice);

                _placingTower = false;
                _towerToSpawn = null;
                _towerPrice = 0;
                Destroy(_placementArea);

            }

         
        }
    }
}
