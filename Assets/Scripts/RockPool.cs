using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockPool : MonoBehaviour
{

    public static RockPool rockPoolInstance;

    [SerializeField]
    private GameObject pooledRock;
    private bool notEnoughRocksInPool = true;

    private List<GameObject> rocks;

    private void Awake() {
        rockPoolInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        rocks = new List<GameObject>();
    }

    public GameObject GetRock() {
        if (rocks.Count > 0) {
            for (int i = 0; i < rocks.Count; i++) {
                if (!rocks[i].activeInHierarchy) {
                    return rocks[i];
                }
            }
        } 
        
        if (notEnoughRocksInPool) {
            GameObject rock = Instantiate(pooledRock);
            rock.SetActive(false);
            rocks.Add(rock);
            return rock;
        }
        
        return null;
        
    }

}
