using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TestsApp.Models.Test.Json;

namespace TestsApp.JsonUtils
{
    public static class MyJsonConvert
    {
        private static JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Objects,
            SerializationBinder = new KnownTypesBinder(new List<(Type, string)>()
            {
                (typeof(TextAnswerVariant), "text_answer"),
                (typeof(ImageAnswerVariant), "image_answer"),
            })
        };

        public static string SerializeObject(object value) =>
            JsonConvert.SerializeObject(value, _jsonSerializerSettings);

        public static T DeserializeObject<T>(string value) =>
            JsonConvert.DeserializeObject<T>(value, _jsonSerializerSettings);
    }
}
