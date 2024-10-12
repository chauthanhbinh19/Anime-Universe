using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CollectionManagement : MonoBehaviour
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
    public Button MilitaryGalleryButton;
    public Button SpellGalleryButton;
    public GameObject buttonPrefab;
    public GameObject DictionaryPanel;
    public Transform content;
    public GameObject cardsPrefab;
    public Transform mainContent;
    public Button CloseButton;
    public GameObject equipmentsPrefab;
    //Variable for pagination
    private int offset;
    private int currentPage;
    private int totalPage;
    private int pageSize;
    public Text PageText;
    public Button NextButton;
    public Button PreviousButton;
    private string mainType;
    private string subType;
    public Text titleText;
    void Start()
    {
        offset = 0;
        currentPage = 1;
        pageSize = 100;
        CardsGalleryButton.onClick.AddListener(GetCardsType);
        BooksGalleryButton.onClick.AddListener(GetBooksType);
        ArmyGalleryButton.onClick.AddListener(GetArmyType);
        CollaborationGalleryButton.onClick.AddListener(GetCollaborationType);
        CollaborationEquipmentGalleryButton.onClick.AddListener(GetCollaborationEquipmentsType);
        EquipmentsGalleryButton.onClick.AddListener(GetEquipmentsType);
        MedalsGalleryButton.onClick.AddListener(GetMedalsType);
        MonstersGalleryButton.onClick.AddListener(GetMonstersType);
        PetsGalleryButton.onClick.AddListener(GetPetsType);
        SkillsGalleryButton.onClick.AddListener(GetSkillsType);
        SymbolsGalleryButton.onClick.AddListener(GetSymbolsType);
        TitlesGalleryButton.onClick.AddListener(GetTitlesType);
        MilitaryGalleryButton.onClick.AddListener(GetMilitaryType);
        SpellGalleryButton.onClick.AddListener(GetSpellType);

        CloseButton.onClick.AddListener(ClosePanel);
        NextButton.onClick.AddListener(ChangeNextPage);
        PreviousButton.onClick.AddListener(ChangePreviousPage);
        // GetCardsType();
    }

    void Update()
    {

    }
    public void GetCardsType()
    {
        mainType = "Cards";
        GetButtonType();
        titleText.text = "Cards";
    }
    public void GetBooksType()
    {
        mainType = "Books";
        GetButtonType();
        titleText.text = "Books";
    }
    public void GetArmyType()
    {
        mainType = "Army";
        GetButtonType();
        titleText.text = "Army";
    }
    public void GetCollaborationType()
    {
        mainType = "Collaboration";
        GetButtonType();
        titleText.text = "Collaboration";
    }
    public void GetCollaborationEquipmentsType()
    {
        mainType = "CollaborationEquipments";
        GetButtonType();
        titleText.text = "Collaboration Equipments";
    }
    public void GetEquipmentsType()
    {
        mainType = "Equipments";
        GetButtonType();
        titleText.text = "Equipments";
    }
    public void GetMedalsType()
    {
        mainType = "Medals";
        GetButtonType();
        titleText.text = "Medals";
    }
    public void GetMonstersType()
    {
        mainType = "Monsters";
        GetButtonType();
        titleText.text = "Monsters";
    }
    public void GetPetsType()
    {
        mainType = "Pets";
        GetButtonType();
        titleText.text = "Pets";
    }
    public void GetSkillsType()
    {
        mainType = "Skills";
        GetButtonType();
        titleText.text = "Skills";
    }
    public void GetSymbolsType()
    {
        mainType = "Symbols";
        GetButtonType();
        titleText.text = "Symbols";
    }
    public void GetTitlesType()
    {
        mainType = "Titles";
        GetButtonType();
        titleText.text = "Titles";
    }
    public void GetMilitaryType()
    {
        mainType = "Military";
        GetButtonType();
        titleText.text = "Military";
    }
    public void GetSpellType()
    {
        mainType = "Spell";
        GetButtonType();
        titleText.text = "Spell";
    }
    public List<string> GetUniqueTypes()
    {
        if (mainType.Equals("Cards"))
        {
            return Cards.GetUniqueCardTypes();
        }
        else if (mainType.Equals("Books"))
        {
            return Books.GetUniqueBookTypes();
        }
        else if (mainType.Equals("Army"))
        {
            return Army.GetUniqueArmyTypes();
        }
        else if (mainType.Equals("CollaborationEquipments"))
        {
            return CollaborationEquipment.GetUniqueCollaborationEquipmentTypes();
        }
        else if (mainType.Equals("Equipments"))
        {
            return Equipments.GetUniqueEquipmentsTypes();
        }
        else if (mainType.Equals("Pets"))
        {
            return Pets.GetUniquePetsTypes();
        }
        else if (mainType.Equals("Skills"))
        {
            return Skills.GetUniqueSkillsTypes();
        }
        else if (mainType.Equals("Symbols"))
        {
            return Symbols.GetUniqueSymbolsTypes();
        }
        else if (mainType.Equals("Military"))
        {
            return Military.GetUniqueMilitaryTypes();
        }
        else if (mainType.Equals("Spell"))
        {
            return Spell.GetUniqueSpellTypes();
        }
        return new List<string>();
    }
    public void GetButtonType()
    {
        DictionaryPanel.SetActive(true);
        List<string> uniqueTypes = GetUniqueTypes();
        if (uniqueTypes.Count > 0)
        {
            for (int i = 0; i < uniqueTypes.Count; i++)
            {
                // Tạo một nút mới từ prefab
                string subtype = uniqueTypes[i];
                GameObject button = Instantiate(buttonPrefab, content);

                Text buttonText = button.GetComponentInChildren<Text>();
                buttonText.text = subtype.Replace("_", " ");

                Button btn = button.GetComponent<Button>();
                btn.onClick.AddListener(() => OnButtonClick(button, subtype));

                if (i == 0)
                {
                    subType = subtype;
                    ChangeButtonBackground(button, "Button3");
                    int totalRecord = 0;
                    if (mainType.Equals("Cards"))
                    {
                        Cards cardsManager = new Cards();
                        List<Cards> cards = cardsManager.GetCardsCollection(subtype, pageSize, offset);
                        createCards(cards);

                        totalRecord = cardsManager.GetCardsCount(subtype);
                    }
                    else if (mainType.Equals("Books"))
                    {
                        Books booksManager = new Books();
                        List<Books> books = booksManager.GetBooksCollection(subtype, pageSize, offset);
                        createBooks(books);

                        totalRecord = booksManager.GetBooksCount(subtype);
                    }
                    else if (mainType.Equals("Army"))
                    {
                        Army armyManager = new Army();
                        List<Army> army = armyManager.GetArmyCollection(subtype, pageSize, offset);
                        createArmy(army);

                        totalRecord = armyManager.GetArmyCount(subtype);
                    }
                    else if (mainType.Equals("CollaborationEquipments"))
                    {
                        CollaborationEquipment collaborationEquipmentManager = new CollaborationEquipment();
                        List<CollaborationEquipment> collaborationEquipments = collaborationEquipmentManager.GetCollaborationEquipmentsCollection(subtype, pageSize, offset);
                        createCollaborationEquipments(collaborationEquipments);

                        totalRecord = collaborationEquipmentManager.GetCollaborationEquipmentCount(subtype);
                    }
                    else if (mainType.Equals("Equipments"))
                    {
                        Equipments equipmentsManager = new Equipments();
                        List<Equipments> equipments = equipmentsManager.GetEquipmentsCollection(subtype, pageSize, offset);
                        createEquipments(equipments);

                        totalRecord = equipmentsManager.GetEquipmentsCount(subtype);
                    }
                    else if (mainType.Equals("Pets"))
                    {
                        Pets petsManager = new Pets();
                        List<Pets> pets = petsManager.GetPetsCollection(subtype, pageSize, offset);
                        createPets(pets);

                        totalRecord = petsManager.GetPetsCount(subtype);
                    }
                    else if (mainType.Equals("Skills"))
                    {
                        Skills skillsManager = new Skills();
                        List<Skills> skills = skillsManager.GetSkillsCollection(subtype, pageSize, offset);
                        createSkills(skills);

                        totalRecord = skillsManager.GetSkillsCount(subtype);
                    }
                    else if (mainType.Equals("Symbols"))
                    {
                        Symbols symbolsManager = new Symbols();
                        List<Symbols> symbols = symbolsManager.GetSymbolsCollection(subtype, pageSize, offset);
                        createSymbols(symbols);

                        totalRecord = symbolsManager.GetSymbolsCount(subtype);
                    }
                    else if (mainType.Equals("Military"))
                    {
                        Military militaryManager = new Military();
                        List<Military> militaryList = militaryManager.GetMilitaryCollection(subtype, pageSize, offset);
                        createMilitary(militaryList);

                        totalRecord = militaryManager.GetMilitaryCount(subType);
                    }
                    else if (mainType.Equals("Spell"))
                    {
                        Spell spellManager = new Spell();
                        List<Spell> spellList = spellManager.GetSpellCollection(subtype, pageSize, offset);
                        createSpell(spellList);

                        totalRecord = spellManager.GetSpellCount(subType);
                    }

                    totalPage = CalculateTotalPages(totalRecord, pageSize);
                    PageText.text = currentPage.ToString() + "/" + totalPage.ToString();

                }
                else
                {
                    ChangeButtonBackground(button, "Button4");
                }
            }
        }
        else
        {
            int totalRecord = 0;
            if (mainType.Equals("Collaboration"))
            {
                Collaboration collaborationManager = new Collaboration();
                List<Collaboration> collaborations = collaborationManager.GetCollaborationCollection(pageSize, offset);
                createCollaboration(collaborations);

                totalRecord = collaborationManager.GetCollaborationCount();
            }
            else if (mainType.Equals("Medals"))
            {
                Medals medalsManager = new Medals();
                List<Medals> medalsList = medalsManager.GetMedalsCollection(pageSize, offset);
                createMedals(medalsList);

                totalRecord = medalsManager.GetMedalsCount();
            }
            else if (mainType.Equals("Monsters"))
            {
                Monsters monstersManager = new Monsters();
                List<Monsters> monstersList = monstersManager.GetMonstersCollection(pageSize, offset);
                createMonsters(monstersList);

                totalRecord = monstersManager.GetMonstersCount();
            }
            else if (mainType.Equals("Titles"))
            {
                Titles titleManager = new Titles();
                List<Titles> titlesList = titleManager.GetTitlesCollection(pageSize, offset);
                createTitles(titlesList);

                totalRecord = titleManager.GetTitlesCount();
            }
            totalPage = CalculateTotalPages(totalRecord, pageSize);
            PageText.text = currentPage.ToString() + "/" + totalPage.ToString();
        }

    }
    void OnButtonClick(GameObject clickedButton, string type)
    {
        foreach (Transform child in content)
        {
            // Lấy component Button từ con cái
            Button button = child.GetComponent<Button>();
            if (button != null)
            {
                // Gọi hàm ChangeButtonBackground với màu trắng
                ChangeButtonBackground(button.gameObject, "Button4"); // Giả sử bạn có texture trắng
            }
        }

        subType = type;
        currentPage = 1;
        offset = 0;
        ClearAllPrefabs();
        ChangeButtonBackground(clickedButton, "Button3");
        int totalRecord = 0;

        if (mainType.Equals("Cards"))
        {
            Cards cardsManager = new Cards();
            List<Cards> cards = cardsManager.GetCardsCollection(type, pageSize, offset);
            createCards(cards);

            totalRecord = cardsManager.GetCardsCount(type);
        }
        else if (mainType.Equals("Books"))
        {
            Books booksManager = new Books();
            List<Books> books = booksManager.GetBooksCollection(type, pageSize, offset);
            createBooks(books);

            totalRecord = booksManager.GetBooksCount(type);
        }
        else if (mainType.Equals("Army"))
        {
            Army armyManager = new Army();
            List<Army> army = armyManager.GetArmyCollection(type, pageSize, offset);
            createArmy(army);

            totalRecord = armyManager.GetArmyCount(type);
        }
        else if (mainType.Equals("CollaborationEquipments"))
        {
            CollaborationEquipment collaborationEquipmentManager = new CollaborationEquipment();
            List<CollaborationEquipment> collaborationEquipments = collaborationEquipmentManager.GetCollaborationEquipmentsCollection(type, pageSize, offset);
            createCollaborationEquipments(collaborationEquipments);

            totalRecord = collaborationEquipmentManager.GetCollaborationEquipmentCount(type);
        }
        else if (mainType.Equals("Equipments"))
        {
            Equipments equipmentsManager = new Equipments();
            List<Equipments> equipments = equipmentsManager.GetEquipmentsCollection(type, pageSize, offset);
            createEquipments(equipments);

            totalRecord = equipmentsManager.GetEquipmentsCount(type);
        }
        else if (mainType.Equals("Pets"))
        {
            Pets petsManager = new Pets();
            List<Pets> pets = petsManager.GetPetsCollection(type, pageSize, offset);
            createPets(pets);

            totalRecord = petsManager.GetPetsCount(type);
        }
        else if (mainType.Equals("Skills"))
        {
            Skills skillsManager = new Skills();
            List<Skills> skills = skillsManager.GetSkillsCollection(type, pageSize, offset);
            createSkills(skills);

            totalRecord = skillsManager.GetSkillsCount(type);
        }
        else if (mainType.Equals("Symbols"))
        {
            Symbols symbolsManager = new Symbols();
            List<Symbols> symbols = symbolsManager.GetSymbolsCollection(type, pageSize, offset);
            createSymbols(symbols);

            totalRecord = symbolsManager.GetSymbolsCount(type);
        }
        else if (mainType.Equals("Military"))
        {
            Military militaryManager = new Military();
            List<Military> militaryList = militaryManager.GetMilitaryCollection(type, pageSize, offset);
            createMilitary(militaryList);

            totalRecord = militaryManager.GetMilitaryCount(type);
        }
        else if (mainType.Equals("Spell"))
        {
            Spell spellManager = new Spell();
            List<Spell> spellList = spellManager.GetSpellCollection(type, pageSize, offset);
            createSpell(spellList);

            totalRecord = spellManager.GetSpellCount(type);
        }

        totalPage = CalculateTotalPages(totalRecord, pageSize);
        PageText.text = currentPage.ToString() + "/" + totalPage.ToString();
        // Debug.Log($"Button for type '{type}' clicked!");
    }
    private void ChangeButtonBackground(GameObject button, string image)
    {
        RawImage buttonImage = button.GetComponent<RawImage>();
        if (buttonImage != null)
        {
            Texture texture = Resources.Load<Texture>($"UI/UI/{image}");
            if (texture != null)
            {
                buttonImage.texture = texture;
            }
            else
            {
                Debug.LogError($"Texture '{image}' not found in Resources.");
            }
        }
        else
        {
            Debug.LogError("Button does not have a RawImage component.");
        }
    }
    private void createCards(List<Cards> cards)
    {
        foreach (var card in cards)
        {
            GameObject cardObject = Instantiate(cardsPrefab, mainContent);

            Text Title = cardObject.transform.Find("Title").GetComponent<Text>();
            Title.text = card.name.Replace("_", " ");

            RawImage Image = cardObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = card.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            RawImage rareImage = cardObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{card.rare}");
            rareImage.texture = rareTexture;

            RawImage blockImage = cardObject.transform.Find("Block").GetComponent<RawImage>();
            Button Unlock = cardObject.transform.Find("Unlock").GetComponent<Button>();
            if (card.status.Equals("available"))
            {
                blockImage.gameObject.SetActive(false);
                Unlock.gameObject.SetActive(false);
            }
            else if (card.status.Equals("pending"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(true);
            }
            else if (card.status.Equals("block"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(false);
            }
            GridLayoutGroup gridLayout = mainContent.GetComponent<GridLayoutGroup>();
            if (gridLayout != null)
            {
                gridLayout.cellSize = new Vector2(200, 270);
            }
        }
    }
    private void createBooks(List<Books> books)
    {
        foreach (var book in books)
        {
            GameObject bookObject = Instantiate(cardsPrefab, mainContent);

            Text Title = bookObject.transform.Find("Title").GetComponent<Text>();
            Title.text = book.name.Replace("_", " ");

            RawImage Image = bookObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = book.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            RawImage rareImage = bookObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{book.rare}");
            rareImage.texture = rareTexture;
            // Đặt kích thước gốc
            Image.SetNativeSize();

            RawImage blockImage = bookObject.transform.Find("Block").GetComponent<RawImage>();
            Button Unlock = bookObject.transform.Find("Unlock").GetComponent<Button>();
            if (book.status.Equals("available"))
            {
                blockImage.gameObject.SetActive(false);
                Unlock.gameObject.SetActive(false);
            }
            else if (book.status.Equals("pending"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(true);
            }
            else if (book.status.Equals("block"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(false);
            }

            // Thay đổi tỉ lệ
            if (texture.width < 1400 && texture.height < 1400 && texture.width > 700 && texture.height > 700)
            {
                Image.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
            }
            else if (texture.width > 1000 && texture.height <= 2100 && texture.width < 2000 && texture.height > 1000)
            {
                Image.transform.localScale = new Vector3(0.20f, 0.20f, 0.20f);
            }
            else if (texture.width <= 700 && texture.height <= 700)
            {
                Image.transform.localScale = new Vector3(0.65f, 0.65f, 0.65f);
            }
            else if (texture.width <= 700 && texture.height <= 1100)
            {
                Image.transform.localScale = new Vector3(0.45f, 0.45f, 0.45f);
            }
            else if (texture.width > 700 && texture.height <= 700)
            {
                Image.transform.localScale = new Vector3(0.35f, 0.45f, 0.35f);
            }
            else
            {
                Image.transform.localScale = new Vector3(0.17f, 0.17f, 0.17f);
            }


            GridLayoutGroup gridLayout = mainContent.GetComponent<GridLayoutGroup>();
            if (gridLayout != null)
            {
                gridLayout.cellSize = new Vector2(280, 300);
            }
        }
    }
    private void createArmy(List<Army> armyList)
    {
        foreach (var army in armyList)
        {
            GameObject armyObject = Instantiate(cardsPrefab, mainContent);

            Text Title = armyObject.transform.Find("Title").GetComponent<Text>();
            Title.text = army.name.Replace("_", " ");

            RawImage Image = armyObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = army.image.Replace(".png", "");
            fileNameWithoutExtension = fileNameWithoutExtension.Replace(".jpg", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            RawImage rareImage = armyObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{army.rare}");
            rareImage.texture = rareTexture;

            RawImage blockImage = armyObject.transform.Find("Block").GetComponent<RawImage>();
            Button Unlock = armyObject.transform.Find("Unlock").GetComponent<Button>();
            if (army.status.Equals("available"))
            {
                blockImage.gameObject.SetActive(false);
                Unlock.gameObject.SetActive(false);
            }
            else if (army.status.Equals("pending"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(true);
            }
            else if (army.status.Equals("block"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(false);
            }

            GridLayoutGroup gridLayout = mainContent.GetComponent<GridLayoutGroup>();
            if (gridLayout != null)
            {
                gridLayout.cellSize = new Vector2(200, 270);
            }
        }
    }
    private void createCollaboration(List<Collaboration> collaborationList)
    {
        foreach (var collaboration in collaborationList)
        {
            GameObject collaborationObject = Instantiate(cardsPrefab, mainContent);

            Text Title = collaborationObject.transform.Find("Title").GetComponent<Text>();
            Title.text = collaboration.name.Replace("_", " ");

            RawImage Image = collaborationObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = collaboration.image.Replace(".png", "");
            fileNameWithoutExtension = fileNameWithoutExtension.Replace(".jpg", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            RawImage rareImage = collaborationObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>("UI/UI/LG");
            rareImage.texture = rareTexture;

            Image.SetNativeSize();
            Image.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);

            RawImage blockImage = collaborationObject.transform.Find("Block").GetComponent<RawImage>();
            Button Unlock = collaborationObject.transform.Find("Unlock").GetComponent<Button>();
            if (collaboration.status.Equals("available"))
            {
                blockImage.gameObject.SetActive(false);
                Unlock.gameObject.SetActive(false);
            }
            else if (collaboration.status.Equals("pending"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(true);
            }
            else if (collaboration.status.Equals("block"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(false);
            }

            GridLayoutGroup gridLayout = mainContent.GetComponent<GridLayoutGroup>();
            if (gridLayout != null)
            {
                gridLayout.cellSize = new Vector2(280, 230);
            }
        }
    }
    private void createCollaborationEquipments(List<CollaborationEquipment> collaborationEquipmentList)
    {
        foreach (var collaborationEquipment in collaborationEquipmentList)
        {
            GameObject collaborationEquipmentObject = Instantiate(equipmentsPrefab, mainContent);

            Text Title = collaborationEquipmentObject.transform.Find("Title").GetComponent<Text>();
            Title.text = collaborationEquipment.name.Replace("_", " ");

            RawImage Image = collaborationEquipmentObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = collaborationEquipment.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            RawImage rareImage = collaborationEquipmentObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{collaborationEquipment.rare}");
            rareImage.texture = rareTexture;

            RawImage blockImage = collaborationEquipmentObject.transform.Find("Block").GetComponent<RawImage>();
            Button Unlock = collaborationEquipmentObject.transform.Find("Unlock").GetComponent<Button>();
            if (collaborationEquipment.status.Equals("available"))
            {
                blockImage.gameObject.SetActive(false);
                Unlock.gameObject.SetActive(false);
            }
            else if (collaborationEquipment.status.Equals("pending"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(true);
            }
            else if (collaborationEquipment.status.Equals("block"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(false);
            }

            GridLayoutGroup gridLayout = mainContent.GetComponent<GridLayoutGroup>();
            if (gridLayout != null)
            {
                gridLayout.cellSize = new Vector2(200, 230);
            }
        }
    }
    private void createEquipments(List<Equipments> equipmentList)
    {
        foreach (var equipment in equipmentList)
        {
            GameObject equipmentObject = Instantiate(equipmentsPrefab, mainContent);

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

            RawImage blockImage = equipmentObject.transform.Find("Block").GetComponent<RawImage>();
            Button Unlock = equipmentObject.transform.Find("Unlock").GetComponent<Button>();
            if (equipment.status.Equals("available"))
            {
                blockImage.gameObject.SetActive(false);
                Unlock.gameObject.SetActive(false);
            }
            else if (equipment.status.Equals("pending"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(true);
            }
            else if (equipment.status.Equals("block"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(false);
            }

            GridLayoutGroup gridLayout = mainContent.GetComponent<GridLayoutGroup>();
            if (gridLayout != null)
            {
                gridLayout.cellSize = new Vector2(200, 230);
            }
        }
    }
    private void createMedals(List<Medals> medalsList)
    {
        foreach (var medal in medalsList)
        {
            GameObject medalObject = Instantiate(equipmentsPrefab, mainContent);

            Text Title = medalObject.transform.Find("Title").GetComponent<Text>();
            Title.text = medal.name.Replace("_", " ");

            RawImage Image = medalObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = medal.image.Replace(".png", "");
            fileNameWithoutExtension = fileNameWithoutExtension.Replace(".jpg", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            RawImage rareImage = medalObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>("UI/UI/LG");
            rareImage.texture = rareTexture;

            RawImage blockImage = medalObject.transform.Find("Block").GetComponent<RawImage>();
            Button Unlock = medalObject.transform.Find("Unlock").GetComponent<Button>();
            if (medal.status.Equals("available"))
            {
                blockImage.gameObject.SetActive(false);
                Unlock.gameObject.SetActive(false);
            }
            else if (medal.status.Equals("pending"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(true);
            }
            else if (medal.status.Equals("block"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(false);
            }

            RawImage rareBackgroundImage = medalObject.transform.Find("RareBackground").GetComponent<RawImage>();
            rareImage.gameObject.SetActive(false);
            rareBackgroundImage.gameObject.SetActive(false);
        }
    }
    private void createMonsters(List<Monsters> monstersList)
    {
        foreach (var monster in monstersList)
        {
            GameObject monstersObject = Instantiate(cardsPrefab, mainContent);

            Text Title = monstersObject.transform.Find("Title").GetComponent<Text>();
            Title.text = monster.name.Replace("_", " ");

            RawImage Image = monstersObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = monster.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            RawImage rareImage = monstersObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{monster.rare}");
            rareImage.texture = rareTexture;

            RawImage blockImage = monstersObject.transform.Find("Block").GetComponent<RawImage>();
            Button Unlock = monstersObject.transform.Find("Unlock").GetComponent<Button>();
            if (monster.status.Equals("available"))
            {
                blockImage.gameObject.SetActive(false);
                Unlock.gameObject.SetActive(false);
            }
            else if (monster.status.Equals("pending"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(true);
            }
            else if (monster.status.Equals("block"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(false);
            }
            // Đặt kích thước gốc
            Image.SetNativeSize();
            RawImage Background = monstersObject.transform.Find("Background").GetComponent<RawImage>();
            Background.gameObject.SetActive(true);

            // Thay đổi tỉ lệ
            if (texture.width < 1400 && texture.height < 1400 && texture.width > 700 && texture.height > 700)
            {
                Image.transform.localScale = new Vector3(0.30f, 0.30f, 0.30f);
            }
            else if (texture.width > 1000 && texture.height <= 2100 && texture.width < 2000 && texture.height > 1000)
            {
                Image.transform.localScale = new Vector3(0.20f, 0.20f, 0.20f);
            }
            else if (texture.width <= 700 && texture.height <= 700)
            {
                Image.transform.localScale = new Vector3(0.55f, 0.55f, 0.55f);
            }
            else if (texture.width <= 700 && texture.height <= 1100)
            {
                Image.transform.localScale = new Vector3(0.40f, 0.40f, 0.40f);
            }
            else if (texture.width > 700 && texture.height <= 700)
            {
                Image.transform.localScale = new Vector3(0.30f, 0.40f, 0.30f);
            }
            else
            {
                Image.transform.localScale = new Vector3(0.17f, 0.17f, 0.17f);
            }


            GridLayoutGroup gridLayout = mainContent.GetComponent<GridLayoutGroup>();
            if (gridLayout != null)
            {
                gridLayout.cellSize = new Vector2(280, 280);
            }
        }
    }
    private void createPets(List<Pets> petsList)
    {
        foreach (var pet in petsList)
        {
            GameObject petsObject;
            if (pet.type.Equals("Legendary_Dragon") || pet.type.Equals("Naruto_Bijuu") || pet.type.Equals("Naruto_Susanoo") || pet.type.Equals("One_Piece_Ship") || pet.type.Equals("Prime_Monster"))
            {
                petsObject = Instantiate(cardsPrefab, mainContent);
                RawImage Background = petsObject.transform.Find("Background").GetComponent<RawImage>();
                Background.gameObject.SetActive(true);

                GridLayoutGroup gridLayout = mainContent.GetComponent<GridLayoutGroup>();
                if (gridLayout != null)
                {
                    gridLayout.cellSize = new Vector2(280, 280);
                }
            }
            else
            {
                petsObject = Instantiate(equipmentsPrefab, mainContent);

                GridLayoutGroup gridLayout = mainContent.GetComponent<GridLayoutGroup>();
                if (gridLayout != null)
                {
                    gridLayout.cellSize = new Vector2(200, 230);
                }
            }

            Text Title = petsObject.transform.Find("Title").GetComponent<Text>();
            Title.text = pet.name.Replace("_", " ");

            RawImage Image = petsObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = pet.image.Replace(".png", "");
            fileNameWithoutExtension = fileNameWithoutExtension.Replace(".jpg", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            RawImage blockImage = petsObject.transform.Find("Block").GetComponent<RawImage>();
            Button Unlock = petsObject.transform.Find("Unlock").GetComponent<Button>();
            if (pet.status.Equals("available"))
            {
                blockImage.gameObject.SetActive(false);
                Unlock.gameObject.SetActive(false);
            }
            else if (pet.status.Equals("pending"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(true);
            }
            else if (pet.status.Equals("block"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(false);
            }

            if (pet.type.Equals("Prime_Monster"))
            {
                Image.SetNativeSize();
                Image.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            }

            RawImage rareImage = petsObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>("UI/UI/LG");
            rareImage.texture = rareTexture;

        }
    }
    private void createSkills(List<Skills> skillsList)
    {
        foreach (var skill in skillsList)
        {
            GameObject skillObject = Instantiate(equipmentsPrefab, mainContent);

            Text Title = skillObject.transform.Find("Title").GetComponent<Text>();
            Title.text = skill.name.Replace("_", " ");

            RawImage Image = skillObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = skill.image.Replace(".png", "");
            fileNameWithoutExtension = fileNameWithoutExtension.Replace(".jpg", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            // cardImage.SetNativeSize();
            // cardImage.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);

            RawImage rareImage = skillObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{skill.rare}");
            rareImage.texture = rareTexture;

            RawImage blockImage = skillObject.transform.Find("Block").GetComponent<RawImage>();
            Button Unlock = skillObject.transform.Find("Unlock").GetComponent<Button>();
            if (skill.status.Equals("available"))
            {
                blockImage.gameObject.SetActive(false);
                Unlock.gameObject.SetActive(false);
            }
            else if (skill.status.Equals("pending"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(true);
            }
            else if (skill.status.Equals("block"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(false);
            }

            GridLayoutGroup gridLayout = mainContent.GetComponent<GridLayoutGroup>();
            if (gridLayout != null)
            {
                gridLayout.cellSize = new Vector2(200, 230);
            }
        }
    }
    private void createSymbols(List<Symbols> symbolsList)
    {
        foreach (var symbol in symbolsList)
        {
            GameObject symbolObject = Instantiate(equipmentsPrefab, mainContent);

            Text Title = symbolObject.transform.Find("Title").GetComponent<Text>();
            Title.text = symbol.name.Replace("_", " ");

            RawImage Image = symbolObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = symbol.image.Replace(".png", "");
            fileNameWithoutExtension = fileNameWithoutExtension.Replace(".jpg", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            // cardImage.SetNativeSize();
            // cardImage.transform.localScale = new Vector3(0.35f, 0.35f, 0.35f);

            RawImage rareImage = symbolObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{symbol.rare}");
            rareImage.texture = rareTexture;

            RawImage blockImage = symbolObject.transform.Find("Block").GetComponent<RawImage>();
            Button Unlock = symbolObject.transform.Find("Unlock").GetComponent<Button>();
            if (symbol.status.Equals("available"))
            {
                blockImage.gameObject.SetActive(false);
                Unlock.gameObject.SetActive(false);
            }
            else if (symbol.status.Equals("pending"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(true);
            }
            else if (symbol.status.Equals("block"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(false);
            }

            RawImage rareBackgroundImage = symbolObject.transform.Find("RareBackground").GetComponent<RawImage>();
            rareImage.gameObject.SetActive(false);
            rareBackgroundImage.gameObject.SetActive(false);

            GridLayoutGroup gridLayout = mainContent.GetComponent<GridLayoutGroup>();
            if (gridLayout != null)
            {
                gridLayout.cellSize = new Vector2(200, 230);
            }
        }
    }
    private void createTitles(List<Titles> titlesList)
    {
        foreach (var title in titlesList)
        {
            GameObject titleObject = Instantiate(equipmentsPrefab, mainContent);

            Text Title = titleObject.transform.Find("Title").GetComponent<Text>();
            Title.text = title.name.Replace("_", " ");

            RawImage Image = titleObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = title.image.Replace(".png", "");
            fileNameWithoutExtension = fileNameWithoutExtension.Replace(".jpg", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;
            Image.SetNativeSize();
            Image.transform.localScale = new Vector3(0.55f, 0.55f, 0.55f);

            RawImage rareImage = titleObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{title.rare}");
            rareImage.texture = rareTexture;

            RawImage blockImage = titleObject.transform.Find("Block").GetComponent<RawImage>();
            Button Unlock = titleObject.transform.Find("Unlock").GetComponent<Button>();
            if (title.status.Equals("available"))
            {
                blockImage.gameObject.SetActive(false);
                Unlock.gameObject.SetActive(false);
            }
            else if (title.status.Equals("pending"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(true);
            }
            else if (title.status.Equals("block"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(false);
            }

            RawImage rareBackgroundImage = titleObject.transform.Find("RareBackground").GetComponent<RawImage>();
            rareImage.gameObject.SetActive(false);
            rareBackgroundImage.gameObject.SetActive(false);

            GridLayoutGroup gridLayout = mainContent.GetComponent<GridLayoutGroup>();
            if (gridLayout != null)
            {
                gridLayout.cellSize = new Vector2(200, 230);
            }
        }
    }
    private void createMilitary(List<Military> militaryList)
    {
        foreach (var military in militaryList)
        {
            GameObject militaryObject = Instantiate(cardsPrefab, mainContent);

            Text Title = militaryObject.transform.Find("Title").GetComponent<Text>();
            Title.text = military.name.Replace("_", " ");

            RawImage Image = militaryObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = military.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            RawImage rareImage = militaryObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{military.rare}");
            rareImage.texture = rareTexture;

            RawImage blockImage = militaryObject.transform.Find("Block").GetComponent<RawImage>();
            Button Unlock = militaryObject.transform.Find("Unlock").GetComponent<Button>();
            if (military.status.Equals("available"))
            {
                blockImage.gameObject.SetActive(false);
                Unlock.gameObject.SetActive(false);
            }
            else if (military.status.Equals("pending"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(true);
            }
            else if (military.status.Equals("block"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(false);
            }

            GridLayoutGroup gridLayout = mainContent.GetComponent<GridLayoutGroup>();
            if (gridLayout != null)
            {
                gridLayout.cellSize = new Vector2(200, 270);
            }
        }
    }
    private void createSpell(List<Spell> spellList)
    {
        foreach (var spell in spellList)
        {
            GameObject spellObject = Instantiate(cardsPrefab, mainContent);

            Text Title = spellObject.transform.Find("Title").GetComponent<Text>();
            Title.text = spell.name.Replace("_", " ");

            RawImage Image = spellObject.transform.Find("Image").GetComponent<RawImage>();
            string fileNameWithoutExtension = spell.image.Replace(".png", "");
            Texture texture = Resources.Load<Texture>($"{fileNameWithoutExtension}");
            Image.texture = texture;

            RawImage rareImage = spellObject.transform.Find("Rare").GetComponent<RawImage>();
            Texture rareTexture = Resources.Load<Texture>($"UI/UI/{spell.rare}");
            rareImage.texture = rareTexture;

            RawImage blockImage = spellObject.transform.Find("Block").GetComponent<RawImage>();
            Button Unlock = spellObject.transform.Find("Unlock").GetComponent<Button>();
            if (spell.status.Equals("available"))
            {
                blockImage.gameObject.SetActive(false);
                Unlock.gameObject.SetActive(false);
            }
            else if (spell.status.Equals("pending"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(true);
            }
            else if (spell.status.Equals("block"))
            {
                blockImage.gameObject.SetActive(true);
                Unlock.gameObject.SetActive(false);
            }

            GridLayoutGroup gridLayout = mainContent.GetComponent<GridLayoutGroup>();
            if (gridLayout != null)
            {
                gridLayout.cellSize = new Vector2(200, 270);
            }
        }
    }
    public void ClearAllPrefabs()
    {
        // Duyệt qua tất cả các con cái của cardsContent
        foreach (Transform child in mainContent)
        {
            Destroy(child.gameObject);
        }
    }
    public void ClearAllButton()
    {
        // Duyệt qua tất cả các con cái của cardsContent
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
    }
    public int CalculateTotalPages(int totalRecords, int pageSize)
    {
        if (pageSize <= 0) return 0; // Đảm bảo pageSize không âm hoặc bằng 0
        return (int)Math.Ceiling((double)totalRecords / pageSize);
    }
    public void ChangeNextPage()
    {
        if (currentPage < totalPage)
        {
            ClearAllPrefabs();
            int totalRecord = 0;

            if (mainType.Equals("Cards"))
            {
                Cards cardsManager = new Cards();
                totalRecord = cardsManager.GetCardsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Cards> cards = cardsManager.GetCardsCollection(subType, pageSize, offset);
                createCards(cards);
            }
            else if (mainType.Equals("Books"))
            {
                Books booksManager = new Books();
                totalRecord = booksManager.GetBooksCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Books> books = booksManager.GetBooksCollection(subType, pageSize, offset);
                createBooks(books);
            }
            else if (mainType.Equals("Army"))
            {
                Army armyManager = new Army();
                totalRecord = armyManager.GetArmyCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Army> army = armyManager.GetArmyCollection(subType, pageSize, offset);
                createArmy(army);
            }
            else if (mainType.Equals("CollaborationEquipments"))
            {
                CollaborationEquipment collaborationEquipmentManager = new CollaborationEquipment();
                totalRecord = collaborationEquipmentManager.GetCollaborationEquipmentCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<CollaborationEquipment> collaborationEquipments = collaborationEquipmentManager.GetCollaborationEquipmentsCollection(subType, pageSize, offset);
                createCollaborationEquipments(collaborationEquipments);
            }
            else if (mainType.Equals("Collaboration"))
            {
                Collaboration collaborationManager = new Collaboration();
                totalRecord = collaborationManager.GetCollaborationCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Collaboration> collaboration = collaborationManager.GetCollaborationCollection(pageSize, offset);
                createCollaboration(collaboration);
            }
            else if (mainType.Equals("Equipments"))
            {
                Equipments equipmentManager = new Equipments();
                totalRecord = equipmentManager.GetEquipmentsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Equipments> equipments = equipmentManager.GetEquipmentsCollection(subType, pageSize, offset);
                createEquipments(equipments);
            }
            else if (mainType.Equals("Medals"))
            {
                Medals medalsManager = new Medals();
                totalRecord = medalsManager.GetMedalsCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Medals> medalsList = medalsManager.GetMedalsCollection(pageSize, offset);
                createMedals(medalsList);
            }
            else if (mainType.Equals("Monsters"))
            {
                Monsters monstersManager = new Monsters();
                totalRecord = monstersManager.GetMonstersCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Monsters> monstersList = monstersManager.GetMonstersCollection(pageSize, offset);
                createMonsters(monstersList);
            }
            else if (mainType.Equals("Pets"))
            {
                Pets petsManager = new Pets();
                totalRecord = petsManager.GetPetsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Pets> petsList = petsManager.GetPetsCollection(subType, pageSize, offset);
                createPets(petsList);
            }
            else if (mainType.Equals("Skills"))
            {
                Skills skillsManager = new Skills();
                totalRecord = skillsManager.GetSkillsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Skills> skillsList = skillsManager.GetSkillsCollection(subType, pageSize, offset);
                createSkills(skillsList);
            }
            else if (mainType.Equals("Symbols"))
            {
                Symbols symbolsManager = new Symbols();
                totalRecord = symbolsManager.GetSymbolsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Symbols> symbolsList = symbolsManager.GetSymbolsCollection(subType, pageSize, offset);
                createSymbols(symbolsList);
            }
            else if (mainType.Equals("Titles"))
            {
                Titles symbolsManager = new Titles();
                totalRecord = symbolsManager.GetTitlesCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Titles> titlesList = symbolsManager.GetTitlesCollection(pageSize, offset);
                createTitles(titlesList);
            }
            else if (mainType.Equals("Military"))
            {
                Military militaryManager = new Military();
                totalRecord = militaryManager.GetMilitaryCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Military> militaryList = militaryManager.GetMilitaryCollection(subType, pageSize, offset);
                createMilitary(militaryList);
            }
            else if (mainType.Equals("Spell"))
            {
                Spell spellManager = new Spell();
                totalRecord = spellManager.GetSpellCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage + 1;
                offset = offset + pageSize;
                List<Spell> spellList = spellManager.GetSpellCollection(subType, pageSize, offset);
                createSpell(spellList);
            }

            PageText.text = currentPage.ToString() + "/" + totalPage.ToString();

        }
    }
    public void ChangePreviousPage()
    {
        if (currentPage > 1)
        {
            ClearAllPrefabs();
            int totalRecord = 0;

            if (mainType.Equals("Cards"))
            {
                Cards cardsManager = new Cards();
                totalRecord = cardsManager.GetCardsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Cards> cards = cardsManager.GetCardsCollection(subType, pageSize, offset);
                createCards(cards);
            }
            else if (mainType.Equals("Books"))
            {
                Books booksManager = new Books();
                totalRecord = booksManager.GetBooksCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Books> books = booksManager.GetBooksCollection(subType, pageSize, offset);
                createBooks(books);
            }
            else if (mainType.Equals("Army"))
            {
                Army armyManager = new Army();
                totalRecord = armyManager.GetArmyCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Army> army = armyManager.GetArmyCollection(subType, pageSize, offset);
                createArmy(army);
            }
            else if (mainType.Equals("CollaborationEquipments"))
            {
                CollaborationEquipment collaborationEquipmentManager = new CollaborationEquipment();
                totalRecord = collaborationEquipmentManager.GetCollaborationEquipmentCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<CollaborationEquipment> collaborationEquipments = collaborationEquipmentManager.GetCollaborationEquipmentsCollection(subType, pageSize, offset);
                createCollaborationEquipments(collaborationEquipments);
            }
            else if (mainType.Equals("Collaboration"))
            {
                Collaboration collaborationManager = new Collaboration();
                totalRecord = collaborationManager.GetCollaborationCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Collaboration> collaboration = collaborationManager.GetCollaborationCollection(pageSize, offset);
                createCollaboration(collaboration);
            }
            else if (mainType.Equals("Equipments"))
            {
                Equipments equipmentManager = new Equipments();
                totalRecord = equipmentManager.GetEquipmentsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Equipments> equipments = equipmentManager.GetEquipmentsCollection(subType, pageSize, offset);
                createEquipments(equipments);
            }
            else if (mainType.Equals("Medals"))
            {
                Medals medalsManager = new Medals();
                totalRecord = medalsManager.GetMedalsCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Medals> medalsList = medalsManager.GetMedalsCollection(pageSize, offset);
                createMedals(medalsList);
            }
            else if (mainType.Equals("Monsters"))
            {
                Monsters monstersManager = new Monsters();
                totalRecord = monstersManager.GetMonstersCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Monsters> monstersList = monstersManager.GetMonstersCollection(pageSize, offset);
                createMonsters(monstersList);
            }
            else if (mainType.Equals("Pets"))
            {
                Pets petsManager = new Pets();
                totalRecord = petsManager.GetPetsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Pets> petsList = petsManager.GetPetsCollection(subType, pageSize, offset);
                createPets(petsList);
            }
            else if (mainType.Equals("Skills"))
            {
                Skills skillsManager = new Skills();
                totalRecord = skillsManager.GetSkillsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Skills> skillsList = skillsManager.GetSkillsCollection(subType, pageSize, offset);
                createSkills(skillsList);
            }
            else if (mainType.Equals("Symbols"))
            {
                Symbols symbolsManager = new Symbols();
                totalRecord = symbolsManager.GetSymbolsCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Symbols> symbolsList = symbolsManager.GetSymbolsCollection(subType, pageSize, offset);
                createSymbols(symbolsList);
            }
            else if (mainType.Equals("Titles"))
            {
                Titles symbolsManager = new Titles();
                totalRecord = symbolsManager.GetTitlesCount();
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Titles> titlesList = symbolsManager.GetTitlesCollection(pageSize, offset);
                createTitles(titlesList);
            }
            else if (mainType.Equals("Military"))
            {
                Military militaryManager = new Military();
                totalRecord = militaryManager.GetMilitaryCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Military> militaryList = militaryManager.GetMilitaryCollection(subType, pageSize, offset);
                createMilitary(militaryList);
            }
            else if (mainType.Equals("Spell"))
            {
                Spell spellManager = new Spell();
                totalRecord = spellManager.GetSpellCount(subType);
                totalPage = CalculateTotalPages(totalRecord, pageSize);
                currentPage = currentPage - 1;
                offset = offset - pageSize;
                List<Spell> spellList = spellManager.GetSpellCollection(subType, pageSize, offset);
                createSpell(spellList);
            }

            PageText.text = currentPage.ToString() + "/" + totalPage.ToString();

        }
    }
    public void ClosePanel()
    {
        ClearAllButton();
        ClearAllPrefabs();
        offset = 0;
        currentPage = 1;
        DictionaryPanel.SetActive(false);
    }
}
