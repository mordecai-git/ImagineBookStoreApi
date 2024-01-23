using System.Net.Mime;
using System.Text.Json;
using System.Text;

namespace ImagineBookStore.Core.Utilities;

public static class HttpUtilities
{
    public static StringContent ToJsonContent(this object obj)
    {
        StringContent jsonContent = new(JsonSerializer.Serialize(obj, new JsonSerializerOptions(JsonSerializerDefaults.Web)), Encoding.UTF8, MediaTypeNames.Application.Json);

        return jsonContent;
    }
}
