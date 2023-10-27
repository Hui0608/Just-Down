using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floormanager : MonoBehaviour
{
   [SerializeField]GameObject[] FloorPrefabs;
   public void Spwanfloor()
   {
     int x = Random.Range(0,FloorPrefabs.Length);
     GameObject floor = Instantiate(FloorPrefabs[x], transform);
     floor.transform.position = new Vector3(Random.Range(-3.8f,3.8f),-6f,0f);
   }
}
