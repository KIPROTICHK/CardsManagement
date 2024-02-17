using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Reflection;

namespace CardsManagement.Application
{
    internal class JsonIgnoreResolver : DefaultContractResolver
    {
        private readonly HashSet<string> ignoreProps;

        public JsonIgnoreResolver(IEnumerable<string> propNamesToIgnore)
        {
            ignoreProps = new HashSet<string>(propNamesToIgnore);
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty jsonProperty = base.CreateProperty(member, memberSerialization);
            if (ignoreProps.Contains(jsonProperty.PropertyName))
            {
                jsonProperty.ShouldSerialize = (object _) => false;
            }

            return jsonProperty;
        }
    }
}
