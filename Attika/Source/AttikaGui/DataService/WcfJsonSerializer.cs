using System.Text;
using Newtonsoft.Json;

namespace Infotecs.Attika.AttikaGui.DataService
{
    public sealed class WcfJsonSerializer : IDataSerializer
    {
        public byte[] Serialize(object dto)
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(dto,
                                                                      new JsonSerializerSettings
                                                                          {
                                                                              DateFormatHandling =
                                                                                  DateFormatHandling.MicrosoftDateFormat
                                                                          }));
        }

        public T Deserialize<T>(byte[] data)
        {
            return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(data),
                                                    new JsonSerializerSettings
                                                        {
                                                            DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                                                        });
        }
    }
}