using UnityEngine;

public class ManagerData : MonoBehaviour
{
    public static ManagerData instance;
    public Pets pets;
    public Lands lands;
    public Trees trees;
    public Seeds seeds;
    public Cages cages;
    public Houses mainHouses;
    public HouseFarm tower;
    public HouseDepot depot;
    public Facetorys facetorys;
    public ItemBuilding itemBuilding;
    public ToolDecorate toolDecorate;
    public FacetoryItems facetoryItems;
    public PlotOfLands plotOfLands;
    public Flowers flowers;
    public Language language;
    public DataDecorates decorate;
    void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
    }
}
