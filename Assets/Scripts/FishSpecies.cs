using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpecies
{
    public string name;
    public string speciesId;
    public Rarity rarity;
    public int baseCost;
    public float weightMean;
    public float weightStd;

    public FishSpecies(string name, string speciesId, Rarity rarity, int baseCost, float weightMean, float weightStd)
    {
        this.name = name;
        this.speciesId = speciesId;
        this.rarity = rarity;
        this.baseCost = baseCost;
        this.weightMean = weightMean;
        this.weightStd = weightStd;
    }
}

public enum Rarity
{
    common,
    uncommon,
    rare,
    epic,
    legendary
}