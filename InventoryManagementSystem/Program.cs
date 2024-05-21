namespace InventoryManagementSystem
{
    internal class Item
    {
        //Class Attributes with getter setter methods
        public int ID { get; private set; } //Properties
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }

        //Default Constructor
        public Item()
        {

        }

        //Parameterized Constructor
        public Item(int id, string name, double price, int quantity)
        {
            ID = id;
            Name = name;
            Price = price;
            Quantity = quantity;
        }


        public override string ToString()
        {
            return $"[ID: {ID}, Name: {Name}, Price: {Price:C}, Quantity: {Quantity}]";
        }

        //List to Store instances of item class
        public static List<Item> items= new List<Item>();

        //Method to add item
        public static void addItem()
        {
            Console.WriteLine("Enter Item ID: ");
            int id=int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Item Name: ");
            String name=Console.ReadLine();
            Console.WriteLine("Enter Item Price: ");
            double price=double.Parse(Console.ReadLine());
            Console.WriteLine("Enter Item Quantity: ");
            int quantity=int.Parse(Console.ReadLine());

            items.Add(new Item(id, name, price, quantity));
            Console.WriteLine("Item Successfully Added!!");

        }


        //Method to display all items
        public static void getAllItems() {
            if(items.Count == 0)
            {
                Console.WriteLine("No items present!");
            }
            else
            {
                Console.WriteLine("List of items: ");
                foreach(Item item in items)
                {
                    Console.WriteLine(item.ToString);
                }
            }
        }

        //Method to find item by ID
        public static Item findItemById(int id)
        {
            Item foundItem = items.Find(item => item.ID == id);
            if (foundItem == null)
            {
                Console.WriteLine("No item found for the corresponding item ID");
            }
            else
            {
                Console.WriteLine($"Item with given item ID: {foundItem}");
            }

            return foundItem;
        }

        //Method to Update item's information
        public static void updateItem()
        {
            Console.WriteLine("Enter Item ID to update: ");
            int id = int.Parse(Console.ReadLine());

            Item foundItem = items.Find(item => item.ID == id);
            if (foundItem != null)
            {
                Console.WriteLine("Enter new Item name: ");
                foundItem.Name = Console.ReadLine();
                Console.WriteLine("Enter new Item Price: ");
                foundItem.Price = double.Parse(Console.ReadLine());
                Console.WriteLine("Enter new Item Quantity: ");
                foundItem.Quantity = int.Parse(Console.ReadLine());

                Console.WriteLine("Item Updated Successfully!!");
            }
            else
            {
                Console.WriteLine("Item not found!!");
            }

        }


        //Method to delete item
        public static void deleteItem() {
            Console.WriteLine("Enter Item ID to delete: ");
            int id = int.Parse(Console.ReadLine());

            Item foundItem = items.Find(item => item.ID == id);
            if(foundItem != null )
            {
                items.Remove(foundItem);
                Console.WriteLine("Item Deleted Successfully!!");
            }
            else
            {
                Console.WriteLine("Item not found!!");
            }

            

           
        }

    }
}
