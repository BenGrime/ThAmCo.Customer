using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace ThAmCo.Customer.Web.Services
{

    public class ProductsServiceFake : IProductService
    {
        private readonly ProductDto[] _products =
        {
            new ProductDto { Id = 1, Name = "Tony Stark's Sunglasses", Description = "Stylish sunglasses equipped with advanced AI systems", Cost = 199.99 },
            new ProductDto { Id = 2, Name = "Captain America's Motorcycle", Description = "Replica of Steve Rogers' custom Harley-Davidson", Cost = 699.99 },
            new ProductDto { Id = 3, Name = "Thor's Stormbreaker Keychain", Description = "Miniature replica of the mighty Stormbreaker", Cost = 14.99 },
            new ProductDto { Id = 4, Name = "Black Widow's Grappling Hook", Description = "Tactical grappling hook used by Natasha Romanoff", Cost = 149.99 },
            new ProductDto { Id = 5, Name = "Thanos Funko Pop", Description = "Collectible vinyl figure of the Mad Titan", Cost = 19.99 },
            new ProductDto { Id = 6,  Name = "Iron Man Armor", Description = "Advanced suit tech from Stark Industries", Cost = 999.99 },
            new ProductDto { Id = 7,  Name = "Captain America Shield", Description = "Virtually indestructible shield made of Vibranium", Cost = 499.99 },
            new ProductDto { Id = 8,  Name = "Mjolnir Hammer", Description = "Thor's enchanted hammer, for the worthy", Cost = 899.00 },
            new ProductDto { Id = 9,  Name = "Black Panther Suit", Description = "High-tech protective suit of Wakandan design", Cost = 1200.00 },
            new ProductDto { Id = 10, Name = "Hawkeye Bow", Description = "Precision bow with custom features", Cost = 250.50 },
            new ProductDto { Id = 11, Name = "Web-Shooters", Description = "Spider-Man's wrist-mounted web-slinging devices", Cost = 79.99 },
            new ProductDto { Id = 12, Name = "Infinity Gauntlet", Description = "A powerful gauntlet that holds six Infinity Stones", Cost = 3000.00 },
            new ProductDto { Id = 13, Name = "Ant-Man Suit", Description = "Suit with size-altering capabilities", Cost = 850.75 },
            new ProductDto { Id = 14, Name = "Wolverine Claws", Description = "Replica adamantium claws", Cost = 199.99 },
            new ProductDto { Id = 15, Name = "Doctor Strange Cloak", Description = "Mystical cloak with levitation abilities", Cost = 650.00 },
            new ProductDto { Id = 16, Name = "Black Widow Stingers", Description = "High-tech wrist-mounted weapon", Cost = 150.00 },
            new ProductDto { Id = 17, Name = "Hulkbuster Armor", Description = "Heavy-duty suit designed to take on Hulk", Cost = 1500.00 },
            new ProductDto { Id = 18, Name = "Scarlet Witch Tiara", Description = "Enhances magical abilities", Cost = 129.99 },
            new ProductDto { Id = 19, Name = "Groot Sapling", Description = "Miniature plant with growing potential", Cost = 15.99 },
            new ProductDto { Id = 20, Name = "Rocket's Blaster", Description = "Custom heavy-duty blaster", Cost = 200.00 },
            new ProductDto { Id = 21, Name = "Star-Lord Helmet", Description = "Helmet with advanced targeting and communication features", Cost = 299.99 },
            new ProductDto { Id = 22, Name = "Vibranium Suit", Description = "Armor suit crafted from Wakandan Vibranium", Cost = 1200.00 },
            new ProductDto { Id = 23, Name = "Shuri's Panther Gauntlets", Description = "Energy weapon gauntlets designed by Shuri", Cost = 225.00 },
            new ProductDto { Id = 24, Name = "Iron Spider Suit", Description = "Advanced suit designed by Tony Stark for Spider-Man", Cost = 999.99 },
            new ProductDto { Id = 25, Name = "Winter Soldier Arm", Description = "Cybernetic arm made of Vibranium", Cost = 799.99 },
            new ProductDto { Id = 26, Name = "Quinjet Model", Description = "Replica model of the Avengers' high-tech jet", Cost = 499.99 },
            new ProductDto { Id = 27, Name = "Arc Reactor", Description = "Portable power source invented by Tony Stark", Cost = 399.99 },
            new ProductDto { Id = 28, Name = "Yondu's Yaka Arrow", Description = "Whistle-controlled arrow with deadly precision", Cost = 299.50 },
            new ProductDto { Id = 29, Name = "Agent Coulson's Vintage Cards", Description = "Collectible Captain America trading cards", Cost = 89.99 },
            new ProductDto { Id = 30, Name = "Loki's Scepter", Description = "Scepter with the power of the Mind Stone", Cost = 599.99 },
            new ProductDto { Id = 31, Name = "Captain Marvel Gloves", Description = "High-energy photon gloves for enhanced combat", Cost = 199.99 },
            new ProductDto { Id = 32, Name = "E.D.I.T.H. Glasses", Description = "Augmented reality glasses created by Tony Stark", Cost = 749.99 },
            new ProductDto { Id = 33, Name = "Stormbreaker Axe", Description = "Thor's powerful weapon forged in Nidavellir", Cost = 1099.99 },
            new ProductDto { Id = 34, Name = "Nebula's Cybernetic Arm", Description = "Advanced cybernetic arm for enhanced strength", Cost = 699.00 },
            new ProductDto { Id = 35, Name = "Vision's Cape", Description = "Elegant cape designed for the synthetic Avenger", Cost = 299.99 },
            new ProductDto { Id = 36, Name = "Peggy Carter's Compass", Description = "Vintage compass with a photo inside", Cost = 49.99 },
            new ProductDto { Id = 37, Name = "Sling Ring", Description = "Magical ring used for opening portals", Cost = 149.99 },
            new ProductDto { Id = 38, Name = "Hela's Headdress", Description = "Antlered headdress symbolizing the Goddess of Death", Cost = 450.00 },
            new ProductDto { Id = 39, Name = "Drax's Knives", Description = "Dual knives wielded by the Destroyer", Cost = 175.00 },
            new ProductDto { Id = 40, Name = "Rescue Armor", Description = "Pepper Potts' custom Stark armor suit", Cost = 975.00 },


                
            };
        public Task<ProductDto?> GetProductAsync(int id) //get specific product using ID
        {
            var product = _products.FirstOrDefault(r => r.Id == id);
            return Task.FromResult(product);
        }

        public Task<IEnumerable<ProductDto>> GetProductsAsync() // get all products
        {
            var products = _products.AsEnumerable();
            return Task.FromResult(products);
        }
    }
}