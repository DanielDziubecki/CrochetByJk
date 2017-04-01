using Newtonsoft.Json;

namespace CrochetByJk
{
    public static class SerializerConfig
    {
        public static void Configure()
        {
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
        }
    }
}