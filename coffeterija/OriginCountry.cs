using System;
namespace coffeterija
{
    public class OriginCountry : PrimaryKey
    {
        public string Name;
        public Continent Continent { get; set; }
    }
}
