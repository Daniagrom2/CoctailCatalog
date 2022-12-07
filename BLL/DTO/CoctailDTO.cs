using System.Collections.Generic;

namespace BLL.DTO
{
    public class CoctailDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Ingridients { get; set; }
        public string Image { get; set; }
        public string Instructions { get; set; }
        public string GlassName { get; set; }
        public string Type { get; set; }
        public bool Favorite { get; set; } = false;
    }
}