namespace NongTrai
{
    using UnityEngine;
    using UnityEditor;

    public class ManagerDatas
    {
        [MenuItem("Data/Data/Seeds")]
        public static void DetailSeeds()
        {
            Seeds seed = ScriptableObject.CreateInstance<Seeds>();
            AssetDatabase.CreateAsset(seed, "Assets/Data/Data/Manager Seeds.asset");
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = seed;
        }


        [MenuItem("Data/Data/Facetorys")]
        public static void DetailFacetorys()
        {
            Facetorys facetory = ScriptableObject.CreateInstance<Facetorys>();
            AssetDatabase.CreateAsset(facetory, "Assets/Data/Data/Manager Facetorys.asset");
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = facetory;
        }

        [MenuItem("Data/Data/Cages")]
        public static void DetailCages()
        {
            Cages cage = ScriptableObject.CreateInstance<Cages>();
            AssetDatabase.CreateAsset(cage, "Assets/Data/Data/Manager Cages.asset");
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = cage;
        }

        [MenuItem("Data/Data/Pets")]
        public static void DetailPets()
        {
            PetCollection petCollection = ScriptableObject.CreateInstance<PetCollection>();
            AssetDatabase.CreateAsset(petCollection, "Assets/Data/Data/Manager Pets.asset");
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = petCollection;
        }


        [MenuItem("Data/Data/FacetoryItems")]
        public static void DetailFacetoryItems()
        {
            FacetoryItems facetoryitem = ScriptableObject.CreateInstance<FacetoryItems>();
            AssetDatabase.CreateAsset(facetoryitem, "Assets/Data/Data/Manager FacetoryItems.asset");
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = facetoryitem;
        }

        [MenuItem("Data/Data/MainHouses")]
        public static void DetailHouses()
        {
            Houses house = ScriptableObject.CreateInstance<Houses>();
            AssetDatabase.CreateAsset(house, "Assets/Data/Data/Manager MainHouses.asset");
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = house;
        }

        [MenuItem("Data/Data/HouseDepot")]
        public static void DetailWages()
        {
            HouseDepot wage = ScriptableObject.CreateInstance<HouseDepot>();
            AssetDatabase.CreateAsset(wage, "Assets/Data/Data/Manager HouseDepot.asset");
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = wage;
        }

        [MenuItem("Data/Data/BreadFeeds")]
        public static void DetailBreadFeeds()
        {
            BreadFeeds breadfeed = ScriptableObject.CreateInstance<BreadFeeds>();
            AssetDatabase.CreateAsset(breadfeed, "Assets/Data/Data/Manager BreadFeeds.asset");
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = breadfeed;
        }

        [MenuItem("Data/Data/HouseFarm")]
        public static void DetailStoreHouses()
        {
            HouseFarm storehouse = ScriptableObject.CreateInstance<HouseFarm>();
            AssetDatabase.CreateAsset(storehouse, "Assets/Data/Data/Manager HouseFarm.asset");
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = storehouse;
        }


        [MenuItem("Data/Data/Tree")]
        public static void DetailTree()
        {
            Trees tree = ScriptableObject.CreateInstance<Trees>();
            AssetDatabase.CreateAsset(tree, "Assets/Data/Data/Manager Trees.asset");
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = tree;
        }

        [MenuItem("Data/Data/Misson")]
        public static void DetailMission()
        {
            Missions mission = ScriptableObject.CreateInstance<Missions>();
            AssetDatabase.CreateAsset(mission, "Assets/Data/Data/Manager Missions.asset");
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = mission;
        }

        [MenuItem("Data/Data/Land")]
        public static void DetailLands()
        {
            Lands land = ScriptableObject.CreateInstance<Lands>();
            AssetDatabase.CreateAsset(land, "Assets/Data/Data/Manager Land.asset");
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = land;
        }


        [MenuItem("Data/Data/ItemBuilding")]
        public static void DetailItemBuildings()
        {
            ItemBuildings itembuilding = ScriptableObject.CreateInstance<ItemBuildings>();
            AssetDatabase.CreateAsset(itembuilding, "Assets/Data/Data/Manager ItemBuilding.asset");
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = itembuilding;
        }

        [MenuItem("Data/Data/ToolDecorate")]
        public static void DetailToolDecorate()
        {
            ToolDecorate toolDecorate = ScriptableObject.CreateInstance<ToolDecorate>();
            AssetDatabase.CreateAsset(toolDecorate, "Assets/Data/Data/Manager ToolDecorate.asset");
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = toolDecorate;
        }


        [MenuItem("Data/Data/Langauge")]
        public static void DetailLangauge()
        {
            Language language = ScriptableObject.CreateInstance<Language>();
            AssetDatabase.CreateAsset(language, "Assets/Data/Data/Manager Langauge.asset");
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = language;
        }

        [MenuItem("Data/Data/Decorate")]
        public static void DetailDecorate()
        {
            DataDecorates decorate = ScriptableObject.CreateInstance<DataDecorates>();
            AssetDatabase.CreateAsset(decorate, "Assets/Data/Data/Manager Decorate.asset");
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = decorate;
        }

        [MenuItem("Data/Data/Plot Of Lands")]
        public static void DetailPlotOfLand()
        {
            PlotOfLands plotOfLand = ScriptableObject.CreateInstance<PlotOfLands>();
            AssetDatabase.CreateAsset(plotOfLand, "Assets/Data/Data/Manager PlotOfLands.asset");
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = plotOfLand;
        }

        [MenuItem("Data/Data/Flowers")]
        public static void DetailFlower()
        {
            Flowers flower = ScriptableObject.CreateInstance<Flowers>();
            AssetDatabase.CreateAsset(flower, "Assets/Data/Data/Manager Flowers.asset");
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = flower;
        }
    }
}