using System;
using UnityEngine;
using System.Collections.Generic;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform plateVisualPrefab;
    [SerializeField] private Transform counterTopPoint;

    private List<GameObject> plateVisualGameObjectList;

    private void Awake() {
        plateVisualGameObjectList = new List<GameObject>();
    }
    private void Start() {
       
        platesCounter.onPlateSpawned += PlatesCounter_onPlateSpawned;
        platesCounter.onPlateRemoved += PlatesCounter_onPlateRemoved;
    }

    private void PlatesCounter_onPlateRemoved(object sender, EventArgs e) {
        GameObject toptPlateGameObject = plateVisualGameObjectList[plateVisualGameObjectList.Count - 1];
        plateVisualGameObjectList.Remove(toptPlateGameObject);
        Destroy(toptPlateGameObject);
    }

    private void PlatesCounter_onPlateSpawned(object sender, EventArgs e) {
        //Instantiate first then locate before adding to list to save the count zero at the beginning not 1
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);

        float plateOffsetY = 0.1f;
        plateVisualTransform.localPosition = new Vector3(0, plateOffsetY* plateVisualGameObjectList.Count, 0);

        plateVisualGameObjectList.Add(plateVisualTransform.gameObject);

    }
}
