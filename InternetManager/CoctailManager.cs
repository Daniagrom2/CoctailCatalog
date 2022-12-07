using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Newtonsoft.Json.Linq;
using UI.Model;

namespace InternetManager
{
    public class CoctailManager
    {
        public ObservableCollection<Coctail> GetCharacters(string url)
        {
            string json = NetworkManager.DownloadJSON(url);
            JObject Coctails = JObject.Parse(json);
            IList<JToken> results = Coctails["drinks"].Children().ToList();

            var coctails = new ObservableCollection<Coctail>();
            List<string> list_ingridients;
            

            foreach (JToken result in results)
            {
                list_ingridients = new List<string>();
                for (int i = 1; i < 16; i++)
                {
                    if (result[$"strIngredient{i}"] != null)
                    {
                        list_ingridients.Add(result["strIngredient" + i].ToString());
                    }
                }
                coctails.Add(new Coctail()
                {
                    Id = (int)result["idDrink"],
                    Name = result["strDrink"].ToString(),
                    Image = result["strDrinkThumb"].ToString(),
                    Instructions = result["strInstructions"].ToString(),
                    Ingridients = list_ingridients,
                    GlassName = result["strGlass"].ToString(),
                    Type = result["strAlcoholic"].ToString()
                });
            }

            return coctails;

        }
    }
}