using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GalleryManagement : MonoBehaviour
{
    public Button CardsGalleryButton;
    public Button BooksGalleryButton;
    public Button ArmyGalleryButton;
    public Button CollaborationGalleryButton;
    public Button CollaborationEquipmentGalleryButton;
    public Button EquipmentsGalleryButton;
    public Button MedalsGalleryButton;
    public Button MonstersGalleryButton;
    public Button PetsGalleryButton;
    public Button SkillsGalleryButton;
    public Button SymbolsGalleryButton;
    public Button TitlesGalleryButton;
    public GameObject buttonPrefab;
    public GameObject DictionaryPanel;
    public Transform content;
    public GameObject cardsPrefab;
    public Transform cardsContent;
    void Start()
    {
        CardsGalleryButton.onClick.AddListener(GetCardsType);
        // GetCardsType();
    }

    void Update()
    {

    }
    public void GetCardsType()
    {
        DictionaryPanel.SetActive(true);
        List<string> uniqueTypes = Cards.GetUniqueCardTypes();
        for (int i = 0; i < uniqueTypes.Count; i++)
        {
            // Tạo một nút mới từ prefab
            GameObject button = Instantiate(buttonPrefab, content);

            Text buttonText = button.GetComponentInChildren<Text>();
            buttonText.text = uniqueTypes[i];

            Button btn = button.GetComponent<Button>();
            btn.onClick.AddListener(() => OnButtonClick(uniqueTypes[i]));

            if (i == 0)
            {
                ChangeButtonBackground(button);
                Cards cardsManager=new Cards();
                List<Cards> cards=cardsManager.GetCards(uniqueTypes[i]);
                createCards(cards);
            }
        }
    }
    void OnButtonClick(string type)
    {
        Debug.Log($"Button for type '{type}' clicked!");
    }
    private void ChangeButtonBackground(GameObject button)
    {
        RawImage buttonImage = button.GetComponent<RawImage>();
        Texture texture = Resources.Load<Texture>("UI/UI/Button3");
        buttonImage.texture = texture;
    }
    private void createCards(List<Cards> cards){
        foreach(var card in cards){
            GameObject cardObject = Instantiate(cardsPrefab, cardsContent);

            Text cardTitle = cardObject.transform.Find("CardTitle").GetComponent<Text>();
            cardTitle.text=card.name;

            RawImage cardImage = cardObject.transform.Find("CardImage").GetComponent<RawImage>();
            string fileNameWithoutExtension = card.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            cardImage.texture = texture;
        }
    }
}
