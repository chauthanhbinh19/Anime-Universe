using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ButtonManagement : MonoBehaviour
{
    public Button GalleryButton;
    public Button CollectionButton;
    public Button EquipmentsButton;
    public Button CloseEquipmentsButton;
    public Button CloseMainMenuButton;
    public GameObject GalleryPanel;
    public GameObject CollectionPanel;
    public GameObject EquipmentsPanel;
    public GameObject EquipmentsContentPanel;
    public Transform MainContent;
    public GameObject ItemsPrefab;
    public GameObject EquipmentsPanelPrefab;
    public Transform content;
    public GameObject MainMenuPanel;
    public Transform MainMenuContent;
    public GameObject equipmentsPrefab;
    private int offset;
    private int currentPage;
    private int totalPage;
    private int pageSize;
    public Text PageText;
    public Button NextButton;
    public Button PreviousButton;
    public Text title;
    private List<Sprite> backgroundList;
    private int count = 1;
    private string subType = "";
    void Start()
    {
        offset = 0;
        currentPage = 1;
        pageSize = 100;
        GalleryButton.onClick.AddListener(OnGalleryButtonClick);
        CollectionButton.onClick.AddListener(OnCollectionButtonClick);
        EquipmentsButton.onClick.AddListener(onEquipmentsButtonClick);
        CloseEquipmentsButton.onClick.AddListener(Close);

        CloseMainMenuButton.onClick.AddListener(ClosePanel);
        NextButton.onClick.AddListener(ChangeNextPage);
        PreviousButton.onClick.AddListener(ChangePreviousPage);
    }

    void Update()
    {

    }
    public void OnGalleryButtonClick()
    {
        GalleryPanel.SetActive(true);
        CollectionPanel.SetActive(false);
        EquipmentsPanel.SetActive(false);
        Close();
    }
    public void OnCollectionButtonClick()
    {
        GalleryPanel.SetActive(false);
        CollectionPanel.SetActive(true);
        EquipmentsPanel.SetActive(false);
        Close();
    }
    public List<string> GetUniqueTypes()
    {
        // if (mainType.Equals("Equipments"))
        // {
        //     return Equipments.GetUniqueEquipmentsTypes();
        // }
        return Equipments.GetUniqueEquipmentsTypes();
    }
    public void LoadBackground()
    {
        backgroundList = new List<Sprite>();
        foreach (var sprite in Resources.LoadAll<Sprite>("UI/Background1"))
        {
            backgroundList.Add(sprite);
        }
    }
    public void onEquipmentsButtonClick()
    {
        Close();
        EquipmentsPanel.SetActive(true);
        GalleryPanel.SetActive(false);
        CollectionPanel.SetActive(false);
        List<string> uniqueTypes = GetUniqueTypes();
        if (uniqueTypes.Count > 0)
        {
            for (int i = 0; i < uniqueTypes.Count; i++)
            {
                string subtype = uniqueTypes[i];
                GameObject button = Instantiate(ItemsPrefab, content);

                Text Title = button.transform.Find("ItemName").GetComponent<Text>();
                Title.text = subtype.Replace("_", " ");

                RawImage buttonImage = button.transform.Find("ItemImage").GetComponent<RawImage>();
                string fileNameWithoutExtension = subtype.Replace(".png", "");
                string image = fileNameWithoutExtension;
                Texture texture = Resources.Load<Texture>($"UI/Button/{image}");
                buttonImage.texture = texture;

                Button btn = button.GetComponent<Button>();
                int localCount = count;
                btn.onClick.AddListener(() => OnButtonClick(subtype, localCount));
                count++;
            }
        }
    }
    public void OnButtonClick(string type, int count)
    {
        EquipmentsContentPanel.SetActive(true);
        subType = type;
        GameObject equipmentObject = Instantiate(EquipmentsPanelPrefab, MainContent);

        Text Title = equipmentObject.transform.Find("Title").GetComponent<Text>();
        Title.text = type.Replace("_", " ");

        RawImage Image = equipmentObject.transform.Find("Background").GetComponent<RawImage>();
        string image = "Background_" + count;
        Texture texture = Resources.Load<Texture>($"UI/Background1/{image}");
        Image.texture = texture;
        Button closeBtn = equipmentObject.transform.Find("CloseButton").GetComponent<Button>();
        closeBtn.onClick.AddListener(() => OnClose());

        Transform gridLayout = equipmentObject.transform.Find("GridLayout");
        if (gridLayout != null)
        {
            Button bagBtn = gridLayout.transform.Find("Bag").GetComponent<Button>();
            bagBtn.onClick.AddListener(() => GetBag(type));
        }
    }
    public int CalculateTotalPages(int totalRecords, int pageSize)
    {
        if (pageSize <= 0) return 0; // Đảm bảo pageSize không âm hoặc bằng 0
        return (int)Math.Ceiling((double)totalRecords / pageSize);
    }
    public void Close()
    {
        count = 1;
        subType = "";
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }
    public void OnClose()
    {
        EquipmentsContentPanel.SetActive(false);
        foreach (Transform child in MainContent)
        {
            Destroy(child.gameObject);
        }
    }
    public void ClosePanel()
    {
        ClearAllPrefabs();
        offset = 0;
        currentPage = 1;
        MainMenuPanel.SetActive(false);
    }
    private void createEquipments(List<Equipments> equipmentList)
    {
        foreach (var equipment in equipmentList)
        {
            GameObject equipmentObject = Instantiate(equipmentsPrefab, MainMenuContent);

            Text Title = equipmentObject.transform.Find("Title").GetComponent<Text>();
            Title.text = equipment.name.Replace("_", " ");

            RawImage Image = equipmentObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = equipment.image.Replace(".png", "");
            fileNameWithoutExtension = fileNameWithoutExtension.Replace(".jpg", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            // cardImage.SetNativeSize();
            // cardImage.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);

            RawImage rareImage = equipmentObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{equipment.rare}");
            rareImage.texture = rareTexture;

            GridLayoutGroup gridLayout = MainMenuContent.GetComponent<GridLayoutGroup>();
            if (gridLayout != null)
            {
                gridLayout.cellSize = new Vector2(200, 230);
            }
        }
    }
    public void GetBag(string type)
    {
        MainMenuPanel.SetActive(true);
        int totalRecord = 0;
        title.text = type.Replace("_", " ");
        Equipments equipmentsManager = new Equipments();
        List<Equipments> equipments = equipmentsManager.GetEquipments(type, pageSize, offset);
        createEquipments(equipments);

        totalRecord = equipmentsManager.GetEquipmentsCount(type);
        totalPage = CalculateTotalPages(totalRecord, pageSize);
        PageText.text = currentPage.ToString() + "/" + totalPage.ToString();
    }
    public void ClearAllPrefabs()
    {
        // Duyệt qua tất cả các con cái của cardsContent
        foreach (Transform child in MainMenuContent)
        {
            Destroy(child.gameObject);
        }
    }
    public void ChangeNextPage()
    {
        if (currentPage < totalPage)
        {
            ClearAllPrefabs();
            int totalRecord = 0;

            Equipments equipmentManager = new Equipments();
            totalRecord = equipmentManager.GetEquipmentsCount(subType);
            totalPage = CalculateTotalPages(totalRecord, pageSize);
            currentPage = currentPage + 1;
            offset = offset + pageSize;
            List<Equipments> equipments = equipmentManager.GetEquipments(subType, pageSize, offset);
            createEquipments(equipments);

            PageText.text = currentPage.ToString() + "/" + totalPage.ToString();

        }
    }
    public void ChangePreviousPage()
    {
        if (currentPage > 1)
        {
            ClearAllPrefabs();
            int totalRecord = 0;

            Equipments equipmentManager = new Equipments();
            totalRecord = equipmentManager.GetEquipmentsCount(subType);
            totalPage = CalculateTotalPages(totalRecord, pageSize);
            currentPage = currentPage - 1;
            offset = offset - pageSize;
            List<Equipments> equipments = equipmentManager.GetEquipments(subType, pageSize, offset);
            createEquipments(equipments);

            PageText.text = currentPage.ToString() + "/" + totalPage.ToString();

        }
    }
}
