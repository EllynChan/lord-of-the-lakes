using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public static class FishManager
{

    /// <summary>
    /// This script takes the information from the CSV file with all of the fish
    /// information and creates a list of the Fish class with an instance for
    /// each fish in the csv file!
    /// </summary>

    public static Dictionary<Rarity, List<FishSpecies>> fishSpeciesByRarity = new Dictionary<Rarity, List<FishSpecies>>();

    //Run this when we hit play always
    [RuntimeInitializeOnLoadMethod]
    public static void InitFishManager()
    {
        //Access the csv file that contains fish data
        var textAsset = Resources.Load<TextAsset>("CSV/fish");

        foreach (Rarity rarity in Enum.GetValues(typeof(Rarity)))
        {
            fishSpeciesByRarity[rarity] = new List<FishSpecies>();
        }

        //Split the text asset up into each line
        var splitData = textAsset.text.Split('\n');
        foreach (var line in splitData)
        { //Loop through the array we made of each line
            var lineData = line.Split(','); //Split each line up at the comma
            Console.WriteLine("line", lineData);
            if (lineData[0] != "Fish Name")
            { //Fish Name is contained on the first row, so we ignore that
                Console.WriteLine("line", lineData);
                //var newFish = new FishSpecies(lineData[0], lineData[1], (Rarity)line[2], Int32.Parse(lineData[3]), float.Parse(lineData[4]), float.Parse(lineData[5]));

                // fishSpeciesByRarity[newFish.rarity].Add(newFish);
            }
        }
    }

    public static (Fish, float) GetRandomFish(Rarity rarity)
    {
        //Picks out a random fish from the list
        FishSpecies species = fishSpeciesByRarity[rarity][Random.Range(0, fishSpeciesByRarity[rarity].Count)];
        float weight = LogNormalRandom.LogNormal(species.weightMean, species.weightStd);

        float min_timer_ms = 10;
        return (new Fish(species, weight), min_timer_ms);
    }

    //public static (Fish, float) GetRandomFishWeighted(FishingRod fishingRod, TileBase watertile)
    //{
    //    // water is Tile or TileBase or something idk
    //    Buff buff;
    //    if (watertile.name == "shiny spot TODO")
    //    {
    //        // should probably have a buffs compnoent
    //        //buff = registry.buffs.get(waterTile);
    //    }
    //    //if (registry.luresEquipped.has(fishingRod))
    //    //{
    //    //    // should probably have a buffs compnoent
    //    //    int lureIndex = registry.luresEquipped.get(fishingRod).lureIndex;
    //    //    if (lureIndex >= 0)
    //    //    {
    //    //        Entity lure = registry.lures.entities[lureIndex];
    //    //        Buff & lureBuff = registry.buffs.get(lure);
    //    //        buff.charm += lureBuff.charm;
    //    //        buff.strength += lureBuff.strength;
    //    //    }
    //    //}

    //    // loop through all fish in lake
    //    // how to track what lake we are currently in?
    //    //LakeId & lakeInfo = registry.lakes.get(player); // get lake player is currently in
    //    float min_timer_ms = -1;
    //    Fish fish; // set a dummy fish for no catch
    //    //Lake lake = id_to_lake.at(lakeInfo.id);
    //    //for (auto & it : lake.id_to_fishable)
    //    //{
    //    //    int key = it.first;
    //    //    Fishable value = it.second;
    //    //    float probability = value.probability;
    //    //    if (registry.shinySpots.has(waterTile) && value.probability <= UNCOMMON_FISH)
    //    //    {
    //    //        // decrease chance of catching common+uncommon fish if shiny spot is present
    //    //        probability *= 1.5f;
    //    //    }

    //    //    std::uniform_real_distribution<float> fish_uniform_real_dist(0, probability * (100.f - buff.charm) / 100.f);
    //    //// Do stuff
    //    //float ms_until_catch = fish_uniform_real_dist(rng) * 2000.f;
    //    //if (min_timer_ms < 0 || ms_until_catch < min_timer_ms)
    //    //{
    //    //    min_timer_ms = ms_until_catch;
    //    //    fish.species_id = key;
    //    //    tempSpiceID = key;
    //    //    fish_rarity = value.probability; // use original probability here
    //    //}
    //    return ( fish, min_timer_ms );
    //}
}