using Newtonsoft.Json;

namespace Lecture8Demo
{
    public static class DeserializationHelper
    {
        public static PostModel Deserialize(string SerializedJSONString)
        {
            return JsonConvert.DeserializeObject<PostModel>(SerializedJSONString);
        }
    }
}
