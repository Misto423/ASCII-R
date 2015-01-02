using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCIIMazeCS
{
    public enum Attribute
    {
        HP,
        Strength,
        Intellect,
        Defense,
        Fortitude,
        Speed,
    }

    class ItemData
    {
        string name;
        char rep;
        Attribute att;
        Attribute secAtt;
        int modifier;
        int secMod;
        Coord<int, int> location;
        byte quad;

        public ItemData() { }
        public ItemData(string name, char r, Attribute a, Attribute sa, int mod, int smod, byte quad)
        {
            this.name = name;
            rep = r;
            att = a;
            secAtt = sa;
            modifier = mod;
            secMod = smod;
            this.quad = quad;
        }

        public char MapChar
        {
            get { return rep; }
            set { rep = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Coord<int, int> Location
        {
            get
            {
                return location;
            }
            set
            {
                location = value;
            }
        }

        public byte Quadrant
        {
            get { return quad; }
            set { quad = value; }
        }
    }

    class Food
    {
        string name;
        char rep;
        int amt;

        public Food() { }
        public Food(string name, char r, int a)
        {
            this.name = name;
            rep = r;
            amt = a;
        }
    }

    class ItemFill
    {
        public static void fillData()
        {
            GameLib.armor.Add(new ItemData("Leather Vest", 'l', Attribute.Defense, Attribute.Fortitude, 3, 1, 1));
            GameLib.armor.Add(new ItemData("Iron Shield", 's', Attribute.Defense, Attribute.Fortitude, 5, 5, 3));
            GameLib.armor.Add(new ItemData("Chain Mail", 'c', Attribute.Defense, Attribute.Speed, 7, 2, 5));
            GameLib.armor.Add(new ItemData("Mg Barrier", 'b', Attribute.HP, Attribute.Fortitude, 20, 10, 7));
            GameLib.armor.Add(new ItemData("Plate Mail", 'P', Attribute.Defense, Attribute.Speed, 15, -5, 9));
            GameLib.armor.Add(new ItemData("Drg Scales", 'G', Attribute.Defense, Attribute.Fortitude, 13, 13, 11));
            GameLib.armor.Add(new ItemData("Tower Shield", 'S', Attribute.Defense, Attribute.Strength, 10, 7, 13));
            GameLib.armor.Add(new ItemData("Diamond Boot", 'U', Attribute.Defense, Attribute.Fortitude, 20, 20, 15)); 

            //weapons
            GameLib.weapons.Add(new ItemData("Wooden Stick", 'w', Attribute.Strength, Attribute.Strength, 2, 0, 0));
            GameLib.weapons.Add(new ItemData("Short Sword", 'W', Attribute.Strength, Attribute.Strength, 5, 0, 1));
            GameLib.weapons.Add(new ItemData("Long Bow", 'L', Attribute.Strength, Attribute.Speed, 8, 2, 5));
            GameLib.weapons.Add(new ItemData("Hammer", 'H', Attribute.Strength, Attribute.Defense, 15, 4, 8));
            GameLib.weapons.Add(new ItemData("Flail", 'J', Attribute.Strength, Attribute.Intellect, 25, -5, 11));
            GameLib.weapons.Add(new ItemData("Harpoon Gun", 'O', Attribute.Strength, Attribute.Speed, 30, 10, 15));

            //magic weapons
            GameLib.mWeapons.Add(new ItemData("Fire", 't', Attribute.Intellect, Attribute.Intellect, 3, 0, 1));
            GameLib.mWeapons.Add(new ItemData("Blizzard", 'i', Attribute.Intellect, Attribute.Fortitude, 7, 1, 4));
            GameLib.mWeapons.Add(new ItemData("Quake", 'Q', Attribute.Intellect, Attribute.Defense, 10, 5, 6));
            GameLib.mWeapons.Add(new ItemData("Virus", 'V', Attribute.Intellect, Attribute.Fortitude, 10, 10, 10));
            GameLib.mWeapons.Add(new ItemData("Energy Beam", 'R', Attribute.Intellect, Attribute.HP, 22, 15, 12));
            GameLib.mWeapons.Add(new ItemData("Black Hole", 'Z', Attribute.Intellect, Attribute.Fortitude, 30, 5, 14));

            //food
            GameLib.food.Add(new Food("Apple", 'a', 2));
            GameLib.food.Add(new Food("Candy", 'y', 7));
            GameLib.food.Add(new Food("Pizza", 'z', 10));
            GameLib.food.Add(new Food("Pie", 'i', 15));
            GameLib.food.Add(new Food("Steak", 'k', 25));
        }
    }
}
