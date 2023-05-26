﻿using Newtonsoft.Json;

namespace DatabaseController.Models
{
    //Таблица "Побочные эффекты"
    public class SideEffect
    {
        public int Id { get; set; }

        public string Name { get; set; }


        [JsonIgnore]
        public ICollection<Product> Products { get; set; }
    }
}
