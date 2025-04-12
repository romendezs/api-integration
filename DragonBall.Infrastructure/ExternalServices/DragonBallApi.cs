using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DragonBall.Domain.Models
{
    public class DragonBallApiService
    {
        public partial class IResponse
        {
            [JsonProperty("items")]
            public Character[] Items { get; set; }

            [JsonProperty("meta")]
            public Meta Meta { get; set; }

            [JsonProperty("links")]
            public Links Links { get; set; }
        }

        public class Character
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

        public partial class Links
        {
            [JsonProperty("first")]
            public Uri First { get; set; }

            [JsonProperty("previous")]
            public string Previous { get; set; }

            [JsonProperty("next")]
            public Uri Next { get; set; }

            [JsonProperty("last")]
            public Uri Last { get; set; }
        }

        public partial class Meta
        {
            [JsonProperty("totalItems")]
            public long TotalItems { get; set; }

            [JsonProperty("itemCount")]
            public long ItemCount { get; set; }

            [JsonProperty("itemsPerPage")]
            public long ItemsPerPage { get; set; }

            [JsonProperty("totalPages")]
            public long TotalPages { get; set; }

            [JsonProperty("currentPage")]
            public long CurrentPage { get; set; }
        }

        public enum Gender { Female, Male };
        public enum Affiliation { ArmyOfFrieza, Freelancer, ZFighter };

        internal static class Converter
        {
            public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
            {
                MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
                DateParseHandling = DateParseHandling.None,
                Converters =
            {
                AffiliationConverter.Singleton,
                GenderConverter.Singleton,
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
            };
        }

        internal class AffiliationConverter : JsonConverter
        {
            public override bool CanConvert(Type t) => t == typeof(Affiliation) || t == typeof(Affiliation?);

            public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Null) return null;
                var value = serializer.Deserialize<string>(reader);
                switch (value)
                {
                    case "Army of Frieza":
                        return Affiliation.ArmyOfFrieza;
                    case "Freelancer":
                        return Affiliation.Freelancer;
                    case "Z Fighter":
                        return Affiliation.ZFighter;
                    default:
                        throw new Exception($"Cannot unmarshal type Affiliation: {value}");
                }
            }

            public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
            {
                if (untypedValue == null)
                {
                    serializer.Serialize(writer, null);
                    return;
                }
                var value = (Affiliation)untypedValue;
                switch (value)
                {
                    case Affiliation.ArmyOfFrieza:
                        serializer.Serialize(writer, "Army of Frieza");
                        return;
                    case Affiliation.Freelancer:
                        serializer.Serialize(writer, "Freelancer");
                        return;
                    case Affiliation.ZFighter:
                        serializer.Serialize(writer, "Z Fighter");
                        return;
                    default:
                        throw new Exception($"Cannot marshal type Affiliation: {value}");
                }
            }

            public static readonly AffiliationConverter Singleton = new AffiliationConverter();
        }

        internal class GenderConverter : JsonConverter
        {
            public override bool CanConvert(Type t) => t == typeof(Gender) || t == typeof(Gender?);

            public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
            {
                if (reader.TokenType == JsonToken.Null) return null;
                var value = serializer.Deserialize<string>(reader);
                switch (value)
                {
                    case "Female":
                        return Gender.Female;
                    case "Male":
                        return Gender.Male;
                }
                throw new Exception("Cannot unmarshal type Gender");
            }

            public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
            {
                if (untypedValue == null)
                {
                    serializer.Serialize(writer, null);
                    return;
                }
                var value = (Gender)untypedValue;
                switch (value)
                {
                    case Gender.Female:
                        serializer.Serialize(writer, "Female");
                        return;
                    case Gender.Male:
                        serializer.Serialize(writer, "Male");
                        return;
                }
                throw new Exception("Cannot marshal type Gender");
            }

            public static readonly GenderConverter Singleton = new GenderConverter();
        }
    }
}
