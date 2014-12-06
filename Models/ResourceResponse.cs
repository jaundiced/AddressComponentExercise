using System;
using System.Runtime.Serialization;

namespace Models
{
    [Serializable] //Required for Caching
    [DataContract] //Required for proper JSON serialization
    public class ResourceResponse<T> : IResourceResponse
    {
        [DataMember]
        public T Item { get; set; }

        [DataMember]
        public ResourceResponseStatusCode StatusCode { get; set; }

        [DataMember]
        public string StatusDescription { get; set; }

        public ResourceResponse()
        {
            StatusCode = ResourceResponseStatusCode.Ok;
        }

        public bool IsSuccess()
        {
            switch (StatusCode)
            {
                case ResourceResponseStatusCode.Ok:
                    return true;
                default:
                    return false;
            }
        }


        public ResourceResponse<TOutput> Convert<TOutput>(Converter<T, TOutput> converter)
        {
            return new ResourceResponse<TOutput>
            {
                Item = converter(Item),
                StatusCode = StatusCode,
                StatusDescription = StatusDescription
            };
        }

        object IResourceResponse.Item
        {
            get { return Item; }
            set { Item = (T)value; }
        }
    }
}
