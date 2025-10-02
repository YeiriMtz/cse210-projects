using System;
using System.Collections.Generic;

// Address class
class Address
{
    private string street;
    private string city;
    private string stateOrProvince;
    private string country;

    public Address(string street, string city, string stateOrProvince, string country)
    {
        this.street = street;
        this.city = city;
        this.stateOrProvince = stateOrProvince;
        this.country = country;
    }

    public bool IsInUSA()
    {
        return country.Trim().ToUpper() == "USA";
    }

    public string GetFullAddress()
    {
        return $"{street}\n{city}, {stateOrProvince}\n{country}";
    }
}

// Customer class
class Customer
{
    private string name;
    private Address address;

    public Customer(string name, Address address)
    {
        this.name = name;
        this.address = address;
    }

    public string GetName()
    {
        return name;
    }

    public Address GetAddress()
    {
        return address;
    }

    public bool LivesInUSA()
    {
        return address.IsInUSA();
    }
}

// Product class ----------------------------------------
class Product
{
    private string name;
    private string productId;
    private double price;
    private int quantity;

    public Product(string name, string productId, double price, int quantity)
    {
        this.name = name;
        this.productId = productId;
        this.price = price;
        this.quantity = quantity;
    }

    public string GetName()
    {
        return name;
    }

    public string GetProductId()
    {
        return productId;
    }

    public double GetTotalCost()
    {
        return price * quantity;
    }

    public override string ToString()
    {
        return $"{name} (ID: {productId}) - ${price} x {quantity} = ${GetTotalCost()}";
    }
}

// Order class ----------------------------------------
class Order
{
    private List<Product> products;
    private Customer customer;

    public Order(Customer customer)
    {
        this.customer = customer;
        products = new List<Product>();
    }

    public void AddProduct(Product product)
    {
        products.Add(product);
    }

    public double GetShippingCost()
    {
        return customer.LivesInUSA() ? 5.0 : 35.0;
    }

    public double GetTotalPrice()
    {
        double total = 0;
        foreach (var product in products)
        {
            total += product.GetTotalCost();
        }
        return total + GetShippingCost();
    }

    public string GetPackingLabel()
    {
        string label = "Packing Label:\n";
        foreach (var product in products)
        {
            label += $"- {product.GetName()} (ID: {product.GetProductId()})\n";
        }
        return label;
    }

    public string GetShippingLabel()
    {
        return $"Shipping Label:\n{customer.GetName()}\n{customer.GetAddress().GetFullAddress()}";
    }
}

// Program class ----------------------------------------
class Program
{
    static void Main()
    {
        // Order 1
        Address address1 = new Address("Tundra 1832", "Nuevo Casas Grandes", "CHIH", "MEX");
        Customer customer1 = new Customer("Yeiri Martinez", address1);
        Order order1 = new Order(customer1);
        order1.AddProduct(new Product("Laptop", "P001", 1200.00, 1));
        order1.AddProduct(new Product("Mouse", "P002", 25.00, 2));

        // Order 2
        Address address2 = new Address("2649 Hollister Rd.", "Riverton", "UT", "USA");
        Customer customer2 = new Customer("Zulie Langarica", address2);
        Order order2 = new Order(customer2);
        order2.AddProduct(new Product("Headphones", "P003", 80.00, 1));
        order2.AddProduct(new Product("Keyboard", "P004", 45.00, 1));
        order2.AddProduct(new Product("Monitor", "P005", 200.00, 2));

        // Display orders
        List<Order> orders = new List<Order> { order1, order2 };

        foreach (var order in orders)
        {
            Console.WriteLine(order.GetPackingLabel());
            Console.WriteLine(order.GetShippingLabel());
            Console.WriteLine($"Total Price: ${order.GetTotalPrice()}\n");
            Console.WriteLine(new string('-', 40));
        }
    }
}
