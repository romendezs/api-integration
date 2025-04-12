using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DragonBall.Application.DTOs
{
    internal class Character
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("ki")]
        public string Ki { get; set; }

        [JsonProperty("maxKi")]
        public string MaxKi { get; set; }

        [JsonProperty("race")]
        public string Race { get; set; }

        [JsonProperty("gender")]
        public Gender Gender { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("image")]
        public Uri Image { get; set; }

        [JsonProperty("affiliation")]
        public Affiliation Affiliation { get; set; }

        [JsonProperty("deletedAt")]
        public object DeletedAt { get; set; }
    }
}
