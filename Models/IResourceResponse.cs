namespace Models
{
    public interface IResourceResponse
    {
        string StatusDescription { get; set; }
        ResourceResponseStatusCode StatusCode { get; set; }

        bool IsSuccess();

        object Item { get; set; }
    }
}
