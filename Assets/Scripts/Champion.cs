using UnityEngine;

public class Champion : MonoBehaviour
{
    public Inventory    Inventory     { get; private set; }
    public ChampionStats Stats        { get; private set; }

    void Awake()
    {
        Inventory = GetComponent<Inventory>();
        Stats     = GetComponent<ChampionStats>();
    }
}