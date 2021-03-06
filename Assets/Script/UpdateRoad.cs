﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateRoad : MonoBehaviour
{

    public GameObject ball;
    public GameObject roadWithNoFloor;
    public GameObject starter;
    public float roadLength = 10;

    public GameObject floorNormal;
    public GameObject floorMetal;
    public GameObject floorWood;
    public GameObject floorLarger;
    public GameObject floorSmaller;
    public GameObject floorFaster;
    public GameObject floorSlower;

    public int probMetal = 1;
    public int probWood = 1;
    public int probNull = 5;
    public int probLarger = 1;
    public int probSmaller = 1;
    public int probFaster = 1;
    public int probSlower = 1;

    private int probNullOriginal;

    // Use this for initialization
    void Start()
    {
        probNullOriginal = probNull;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] roads = GameObject.FindGameObjectsWithTag("Road");

        float maxZ = 0;
        foreach (GameObject road in roads)
        {
            //找出最远的Road位置
            if (road.transform.position.z > maxZ)
            {
                maxZ = road.transform.position.z;
            }
            //超出2个Road单位长度，删除这个Road
            if (ball.transform.position.z - road.transform.position.z > roadLength * 2)
            {
                Destroy(road);
            }

        }

        while (maxZ < 5 * roadLength + ball.transform.position.z)
        {
            maxZ += roadLength;
            GameObject roadNew = Instantiate(roadWithNoFloor);
            roadNew.transform.position = new Vector3(0, 0, maxZ);
            generateFloor(roadNew);

            probNull++; //走的越远 出现洞的几率越大
        }


    }

    private void generateFloor(GameObject roadNew)
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                GameObject floorOne = null;
                int random = UnityEngine.Random.Range(0, 100);
                if (random < probMetal)
                {
                    floorOne = Instantiate(floorMetal);
                }
                else if (random < (probMetal + probWood))
                {
                    floorOne = Instantiate(floorWood);
                }
                else if (random < (probMetal + probWood + probNull))
                {

                }
                else if (random < (probMetal + probWood + probNull + probLarger))
                {
                    floorOne = Instantiate(floorLarger);
                }
                else if (random < (probMetal + probWood + probNull + probLarger + probSmaller))
                {
                    floorOne = Instantiate(floorSmaller);
                }
                else if (random < (probMetal + probWood + probNull + probLarger + probSmaller + probFaster))
                {
                    floorOne = Instantiate(floorFaster);
                }
                else if (random < (probMetal + probWood + probNull + probLarger + probSmaller + probFaster + probSlower))
                {
                    floorOne = Instantiate(floorSlower);
                }
                else
                {
                    floorOne = Instantiate(floorNormal);
                }
                if (floorOne != null)
                {
                    floorOne.transform.position = roadNew.transform.position;
                    floorOne.transform.parent = roadNew.transform;
                    floorOne.transform.localPosition = new Vector3(-4.5f + i, 0, -4.5f + j);
                }

            }
        }
    }

    public void resetRoad()
    {
        //删除所有的路
        GameObject[] roads = GameObject.FindGameObjectsWithTag("Road");
        if (roads != null && roads.Length > 0)
        {
            foreach (GameObject road in roads)
            {
                Destroy(road);
            }

            roads = null;
        }

        Instantiate(starter);

        //重置空洞几率
        probNull = probNullOriginal;
    }
}
