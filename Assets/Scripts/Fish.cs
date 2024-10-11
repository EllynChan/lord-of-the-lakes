using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish
{
    public string name;
    public string speciesId; // this is also their sprite_id
    public Rarity rarity;
    public float weight;
    public int cost;

    public Fish(FishSpecies species, float weight)
    {
        name = species.name;
        speciesId = species.speciesId;
        rarity = species.rarity;
        this.weight = weight;
        this.cost = species.baseCost;
    }
}
