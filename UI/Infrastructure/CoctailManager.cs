using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BLL.DTO;
using Newtonsoft.Json.Linq;


namespace InternetManager
{
    public class CoctailManager
    {
       
        public ObservableCollection<CoctailDTO> GetCharacters(string url)
        {
            string json = NetworkManager.DownloadJSON(url);
            JObject Coctails = JObject.Parse(json);
            IList<JToken> results = Coctails["drinks"].Children().ToList();

            var coctails = new ObservableCollection<CoctailDTO>();
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
                coctails.Add(new CoctailDTO()
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
        public ObservableCollection<CoctailDTO> GetAll()
        {
          ObservableCollection<CoctailDTO> boof = new ObservableCollection<CoctailDTO>();
          GetCharacters("https://www.thecocktaildb.com/api/json/v1/1/search.php?f=a").ToList().ForEach(boof.Add);
             GetCharacters("https://www.thecocktaildb.com/api/json/v1/1/search.php?f=b").ToList().ForEach(boof.Add);
             GetCharacters("https://www.thecocktaildb.com/api/json/v1/1/search.php?f=c").ToList().ForEach(boof.Add);
             GetCharacters("https://www.thecocktaildb.com/api/json/v1/1/search.php?f=d").ToList().ForEach(boof.Add);
             GetCharacters("https://www.thecocktaildb.com/api/json/v1/1/search.php?f=e").ToList().ForEach(boof.Add);
             GetCharacters("https://www.thecocktaildb.com/api/json/v1/1/search.php?f=f").ToList().ForEach(boof.Add);
             GetCharacters("https://www.thecocktaildb.com/api/json/v1/1/search.php?f=g").ToList().ForEach(boof.Add);
             GetCharacters("https://www.thecocktaildb.com/api/json/v1/1/search.php?f=h").ToList().ForEach(boof.Add);
             GetCharacters("https://www.thecocktaildb.com/api/json/v1/1/search.php?f=i").ToList().ForEach(boof.Add);
             GetCharacters("https://www.thecocktaildb.com/api/json/v1/1/search.php?f=j").ToList().ForEach(boof.Add);
             GetCharacters("https://www.thecocktaildb.com/api/json/v1/1/search.php?f=k").ToList().ForEach(boof.Add);
             GetCharacters("https://www.thecocktaildb.com/api/json/v1/1/search.php?f=o").ToList().ForEach(boof.Add);
             GetCharacters("https://www.thecocktaildb.com/api/json/v1/1/search.php?f=u").ToList().ForEach(boof.Add);
             GetCharacters("https://www.thecocktaildb.com/api/json/v1/1/search.php?f=y").ToList().ForEach(boof.Add);
             GetCharacters("https://www.thecocktaildb.com/api/json/v1/1/search.php?f=s").ToList().ForEach(boof.Add);
             GetCharacters("https://www.thecocktaildb.com/api/json/v1/1/search.php?f=p").ToList().ForEach(boof.Add);
             GetCharacters("https://www.thecocktaildb.com/api/json/v1/1/search.php?f=q").ToList().ForEach(boof.Add);

            return boof;
        }
    }
}