class Node
{
    public int Key;
    public Node Left;
    public Node Right;

    public Node(int key)
    {
        Key = key;
        Left = null;
        Right = null;
    }
}

class BinarySearchTree
{
    private Node root;

    public BinarySearchTree()
    {
        root = null;
    }

    public void Insert(int key)
    {
        root = InsertRec(root, key);
    }

    private Node InsertRec(Node root, int key)
    {
        if (root == null)
        {
            return new Node(key);
        }

        if (key < root.Key)
        {
            root.Left = InsertRec(root.Left, key);
        }
        else if (key > root.Key)
        {
            root.Right = InsertRec(root.Right, key);
        }

        return root;
    }

    public void Delete(int key)
    {
        root = DeleteRec(root, key);
    }

    private Node DeleteRec(Node root, int key)
    {
        if (root == null)
        {
            return root;
        }

        if (key < root.Key)
        {
            root.Left = DeleteRec(root.Left, key);
        }
        else if (key > root.Key)
        {
            root.Right = DeleteRec(root.Right, key);
        }
        else
        {
            if (root.Left == null)
            {
                return root.Right;
            }
            else if (root.Right == null)
            {
                return root.Left;
            }

            root.Key = FindMinValue(root.Right);
            root.Right = DeleteRec(root.Right, root.Key);
        }

        return root;
    }

    private int FindMinValue(Node root)
    {
        int minValue = root.Key;
        while (root.Left != null)
        {
            minValue = root.Left.Key;
            root = root.Left;
        }
        return minValue;
    }

    public void Search(int key)
    {
        if (SearchRec(root, key) != null)
        {
            Console.WriteLine($"Element {key} został znaleziony w drzewie.");
        }
        else
        {
            Console.WriteLine($"Element {key} nie został znaleziony w drzewie.");
        }
    }

    private Node SearchRec(Node root, int key)
    {
        if (root == null || root.Key == key)
        {
            return root;
        }

        if (key < root.Key)
        {
            return SearchRec(root.Left, key);
        }

        return SearchRec(root.Right, key);
    }

    public void PrintPreOrder()
    {
        Console.WriteLine("PreOrder traversal:");
        PreOrder(root);
        Console.WriteLine();
    }

    private void PreOrder(Node root)
    {
        if (root != null)
        {
            Console.Write($"{root.Key} ");
            PreOrder(root.Left);
            PreOrder(root.Right);
        }
    }

    public void PrintLevel(int key)
    {
        int level = GetLevel(root, key, 0);
        Console.WriteLine($"Level elementu {key} w drzewie: {level}");
    }

    private int GetLevel(Node root, int key, int level)
    {
        if (root == null)
        {
            return -1;
        }

        if (key == root.Key)
        {
            return level;
        }

        if (key < root.Key)
        {
            return GetLevel(root.Left, key, level + 1);
        }

        return GetLevel(root.Right, key, level + 1);
    }

    public void SaveToFile(string fileName)
    {
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            SaveToFileRec(root, writer);
        }
    }

    private void SaveToFileRec(Node root, StreamWriter writer)
    {
        if (root != null)
        {
            writer.Write($"{root.Key} ");
            SaveToFileRec(root.Left, writer);
            SaveToFileRec(root.Right, writer);
        }
    }

}

class PersonCarRecord
{
    public string Pesel;
    public string CarRegistrationNumber;

    public PersonCarRecord(string pesel, string carRegistrationNumber)
    {
        Pesel = pesel;
        CarRegistrationNumber = carRegistrationNumber;
    }
}

class PersonCarNode
{
    public string Key;
    public PersonCarNode Left;
    public PersonCarNode Right;
    public Node OtherTreeReference; // Referencja do węzła w drugim drzewie
    public PersonCarRecord Data;

    public PersonCarNode(string key, PersonCarRecord data)
    {
        Key = key;
        Left = null;
        Right = null;
        OtherTreeReference = null;
        Data = data;
    }
}

class PersonCarBST
{
    private PersonCarNode peselRoot;
    private PersonCarNode carRegistrationRoot;

    public PersonCarBST()
    {
        peselRoot = null;
        carRegistrationRoot = null;
    }

    public void Insert(string pesel, string carRegistrationNumber)
    {
        PersonCarRecord record = new PersonCarRecord(pesel, carRegistrationNumber);
        peselRoot = InsertRec(peselRoot, pesel, record);
        carRegistrationRoot = InsertRec(carRegistrationRoot, carRegistrationNumber, record);
    }

    private PersonCarNode InsertRec(PersonCarNode root, string key, PersonCarRecord data)
    {
        if (root == null)
        {
            return new PersonCarNode(key, data);
        }

        int compareResult = String.Compare(key, root.Key);
        if (compareResult < 0)
        {
            root.Left = InsertRec(root.Left, key, data);
        }
        else if (compareResult > 0)
        {
            root.Right = InsertRec(root.Right, key, data);
        }

        return root;
    }

    public void SearchByPesel(string pesel)
    {
        PersonCarNode resultNode = SearchRec(peselRoot, pesel);
        if (resultNode != null)
        {
            Console.WriteLine($"Pesel: {pesel}, Rejstracja: {resultNode.Data.CarRegistrationNumber}");
        }
        else
        {
            Console.WriteLine($"Pesel {pesel} nie znaleziono.");
        }
    }

    public void SearchByCarRegistrationNumber(string carRegistrationNumber)
    {
        PersonCarNode resultNode = SearchRec(carRegistrationRoot, carRegistrationNumber);
        if (resultNode != null)
        {
            Console.WriteLine($"Rejestracja: {carRegistrationNumber}, Pesel: {resultNode.Data.Pesel}");
        }
        else
        {
            Console.WriteLine($"Rejetracji {carRegistrationNumber} nie znaleziono.");
        }
    }

    private PersonCarNode SearchRec(PersonCarNode root, string key)
    {
        if (root == null || root.Key == key)
        {
            return root;
        }

        int compareResult = String.Compare(key, root.Key);
        if (compareResult < 0)
        {
            return SearchRec(root.Left, key);
        }

        return SearchRec(root.Right, key);
    }

    public void DeleteByPesel(string pesel)
    {
        peselRoot = DeleteRec(peselRoot, pesel);
        PersonCarNode peselNode = SearchRec(peselRoot, pesel);
        if (peselNode != null)
        {
            string carRegistrationNumber = peselNode.Data.CarRegistrationNumber;
            carRegistrationRoot = DeleteRec(carRegistrationRoot, carRegistrationNumber);
        }
    }

    public void DeleteByCarRegistrationNumber(string carRegistrationNumber)
    {
        carRegistrationRoot = DeleteRec(carRegistrationRoot, carRegistrationNumber);
        PersonCarNode carRegistrationNode = SearchRec(carRegistrationRoot, carRegistrationNumber);
        if (carRegistrationNode != null)
        {
            string pesel = carRegistrationNode.Data.Pesel;
            peselRoot = DeleteRec(peselRoot, pesel);
        }
    }

    private PersonCarNode DeleteRec(PersonCarNode root, string key)
    {
        if (root == null)
        {
            return root;
        }

        int compareResult = String.Compare(key, root.Key);
        if (compareResult < 0)
        {
            root.Left = DeleteRec(root.Left, key);
        }
        else if (compareResult > 0)
        {
            root.Right = DeleteRec(root.Right, key);
        }
        else
        {
            if (root.Left == null)
            {
                return root.Right;
            }
            else if (root.Right == null)
            {
                return root.Left;
            }

            root.Key = FindMinValue(root.Right);
            root.Right = DeleteRec(root.Right, root.Key);
        }

        return root;
    }

    private string FindMinValue(PersonCarNode root)
    {
        string minValue = root.Key;
        while (root.Left != null)
        {
            minValue = root.Left.Key;
            root = root.Left;
        }
        return minValue;
    }

    public void SaveToFile(string fileName, bool byPesel)
    {
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            SaveToFileRec(byPesel ? peselRoot : carRegistrationRoot, writer);
        }
    }
    private void SaveToFileRec(PersonCarNode root, StreamWriter writer)
    {
        if (root != null)
        {
            writer.WriteLine($"{root.Data.Pesel} {root.Data.CarRegistrationNumber}");
            SaveToFileRec(root.Left, writer);
            SaveToFileRec(root.Right, writer);
        }
    }

    /*private void SaveToFileRec(PersonCarNode root, StreamWriter writer)
    {
        if (root != null)
        {
            SaveToFileRec(root.Left, writer);
            writer.WriteLine($"{root.Data.Pesel} {root.Data.CarRegistrationNumber}");
            SaveToFileRec(root.Right, writer);
        }
    }*/
}

class Program
{
    static void Main()
    {
        BinarySearchTree bst = new BinarySearchTree();
        PersonCarBST personCarBST = new PersonCarBST();

        int choice;
        do
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Wczytaj elementy do drzewa BST z pliku InTest1.txt");
            Console.WriteLine("2. Losuj elementy i wczytaj do drzewa BST");
            Console.WriteLine("3. Zapisz elementy drzewa BST do pliku OutTest3.txt w kolejności KLP");
            Console.WriteLine("4. Podaj poziom wybranego elementu drzewa BST");
            Console.WriteLine("5. Dodaj element do drzewa BST");
            Console.WriteLine("6. Usuń element z drzewa BST");
            Console.WriteLine("7. Wypisz elementy drzewa BST");
            Console.WriteLine("8. Plik -> Wczytaj (dla numerów PESEL i rejestracyjnych)");
            Console.WriteLine("9. Plik -> Zapisz (według numeru PESEL)");
            Console.WriteLine("10. Plik -> Zapisz (według numeru rejestracyjnego)");
            Console.WriteLine("11. Wstaw -> numer PESEL");
            Console.WriteLine("12. Wstaw -> numer rejestracyjny");
            Console.WriteLine("13. Wyszukaj -> numer PESEL");
            Console.WriteLine("14. Wyszukaj -> numer rejestracyjny");
            Console.WriteLine("15. Usuń -> numer PESEL");
            Console.WriteLine("16. Usuń -> numer rejestracyjny");
            Console.WriteLine("0. Wyjście");

            Console.Write("Wybierz opcję: ");
            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        LoadFromFile(bst, "InTest1.txt");
                        Console.WriteLine("Elementy wczytane do drzewa BST.");
                        break;
                    case 2:
                        Random random = new Random();
                        int[] elementsToRandomize = new int[10];
                        for (int i = 0; i < elementsToRandomize.Length; i++)
                        {
                            elementsToRandomize[i] = random.Next(100, 200);
                        }
                        foreach (int element in elementsToRandomize)
                        {
                            bst.Insert(element);
                        }
                        Console.WriteLine("Losowe elementy dodane do drzewa BST.");
                        break;
                    case 3:
                        bst.SaveToFile("OutTest3.txt");
                        Console.WriteLine("Elementy drzewa BST zapisane do pliku OutTest3.txt.");
                        break;
                    case 4:
                        Console.Write("Podaj element do sprawdzenia poziomu: ");
                        int elementToFindLevel = int.Parse(Console.ReadLine());
                        bst.PrintLevel(elementToFindLevel);
                        break;
                    case 5:
                        Console.Write("Podaj element do dodania: ");
                        int elementToAdd = int.Parse(Console.ReadLine());
                        bst.Insert(elementToAdd);
                        Console.WriteLine($"Element {elementToAdd} dodany do drzewa BST.");
                        break;
                    case 6:
                        Console.Write("Podaj element do usunięcia: ");
                        int elementToDelete = int.Parse(Console.ReadLine());
                        bst.Delete(elementToDelete);
                        Console.WriteLine($"Element {elementToDelete} usunięty z drzewa BST.");
                        break;
                    case 7:
                        bst.PrintPreOrder();
                        break;
                    case 8:
                        Console.Write("Podaj nazwę pliku do wczytania (numer PESEL i numer rejestracyjny): ");
                        string fileNameToLoad = Console.ReadLine();
                        LoadFromFile(personCarBST, fileNameToLoad);
                        Console.WriteLine($"Dane wczytane z pliku {fileNameToLoad}.");
                        break;
                    case 9:
                        Console.Write("Podaj nazwę pliku do zapisania (według numeru PESEL): ");
                        string peselFileName = Console.ReadLine();
                        personCarBST.SaveToFile(peselFileName, true);
                        Console.WriteLine("Dane zapisane według numeru PESEL.");
                        break;
                    case 10:
                        Console.Write("Podaj nazwę pliku do zapisania (według numeru rejestracyjnego): ");
                        string carRegistrationFileName = Console.ReadLine();
                        personCarBST.SaveToFile(carRegistrationFileName, false);
                        Console.WriteLine("Dane zapisane według numeru rejestracyjnego.");
                        break;
                    case 11:
                        Console.Write("Podaj numer PESEL do wstawienia: ");
                        string peselToInsert = Console.ReadLine();
                        Console.Write("Podaj numer rejestracyjny do wstawienia: ");
                        string carRegistrationToInsert = Console.ReadLine();
                        personCarBST.Insert(peselToInsert, carRegistrationToInsert);
                        Console.WriteLine($"Numer PESEL {peselToInsert} wraz z numerem rejestracyjnym {carRegistrationToInsert} dodany.");
                        break;
                    case 12:
                        Console.Write("Podaj numer rejestracyjny do wstawienia: ");
                        string carRegistrationToInsert2 = Console.ReadLine();
                        Console.Write("Podaj numer PESEL do wstawienia: ");
                        string peselToInsert2 = Console.ReadLine();
                        personCarBST.Insert(peselToInsert2, carRegistrationToInsert2);
                        Console.WriteLine($"Numer rejestracyjny {carRegistrationToInsert2} wraz z numerem PESEL {peselToInsert2} dodany.");
                        break;
                    case 13:
                        Console.Write("Podaj numer PESEL do wyszukania: ");
                        string peselToSearch = Console.ReadLine();
                        personCarBST.SearchByPesel(peselToSearch);
                        break;
                    case 14:
                        Console.Write("Podaj numer rejestracyjny do wyszukania: ");
                        string carRegistrationToSearch = Console.ReadLine();
                        personCarBST.SearchByCarRegistrationNumber(carRegistrationToSearch);
                        break;
                    case 15:
                        Console.Write("Podaj numer PESEL do usunięcia: ");
                        string peselToDelete = Console.ReadLine();
                        personCarBST.DeleteByPesel(peselToDelete);
                        Console.WriteLine($"Numer PESEL {peselToDelete} usunięty.");
                        break;
                    case 16:
                        Console.Write("Podaj numer rejestracyjny do usunięcia: ");
                        string carRegistrationToDelete = Console.ReadLine();
                        personCarBST.DeleteByCarRegistrationNumber(carRegistrationToDelete);
                        Console.WriteLine($"Numer rejestracyjny {carRegistrationToDelete} usunięty.");
                        break;
                    case 0:
                        Console.WriteLine("Wyjście z programu.");
                        break;
                    default:
                        Console.WriteLine("Nieprawidłowy wybór. Spróbuj ponownie.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Nieprawidłowy format danych. Spróbuj ponownie.");
            }
        } while (choice != 0);
    }

    static void LoadFromFile(BinarySearchTree bst, string fileName)
    {
        string[] lines = File.ReadAllLines(fileName);
        foreach (string line in lines)
        {
            string[] elements = line.Split(' ');
            foreach (string element in elements)
            {
                if (int.TryParse(element, out int key))
                {
                    bst.Insert(key);
                }
            }
        }
    }

    static void LoadFromFile(PersonCarBST personCarBST, string fileName)
    {
        string[] lines = File.ReadAllLines(fileName);
        foreach (string line in lines)
        {
            string[] elements = line.Split(' ');
            if (elements.Length == 2)
            {
                string pesel = elements[0];
                string carRegistrationNumber = elements[1];
                personCarBST.Insert(pesel, carRegistrationNumber);
            }
        }
    }
}
