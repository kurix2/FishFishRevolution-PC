using UnityEngine;
using System.Collections;


public class Fish
{

    public int id;
    private string name;
    private string prefab;
    private int rarity;
    private float minWeight;
    private float maxWeight;
    private int movePattern;
    private float speed;
    private float endurance;
    private string icon;

    private float weight;


    public Fish(int i, string n, string pf, int r, float minW, float maxW, int moveP, float spd, float end, string ic)
    {
        id = i;
        name = n;
        prefab = pf;
        rarity = r;
        minWeight = minW;
        maxWeight = maxW;
        movePattern = moveP;
        speed = spd;
        endurance = end;
        icon = ic;
    }

    public void setWeight(float wgt)
    {
        weight = wgt;
    }

    public float getMaxW()
    {
        return maxWeight;
    }
    public float getMinW()
    {
        return minWeight;
    }

    public string getPrefab()
    {
        return prefab;
    }

    public int getRarity()
    {
        return rarity;
    }

    public float getWeight()
    {
        return weight;
    }

    public string getName()
    {
        return name;
    }

    public float getResistance()
    {
        return endurance;
    }

    public float getSpeed()
    {
        return speed;
    }

    public int getMovePattern()
    {
        return movePattern;
    }

    public void randomize()
    {
        weight = Mathf.Floor(Random.Range(minWeight, maxWeight));
    }

    public string getIcon()
    {
        return icon;
    }

}
