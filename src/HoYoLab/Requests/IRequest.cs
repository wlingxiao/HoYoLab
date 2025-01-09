namespace HoYoLab.Requests;

public interface IRequest
{
    public HttpRequestMessage Request();
}

public interface IRequest<TResponse> : IRequest;

public abstract class BaseRequest : IRequest
{
    protected virtual HttpMethod Method => HttpMethod.Get;
    protected abstract string RequestUri { get; }
    protected abstract void UpdateRequest(HttpRequestMessage request);

    public HttpRequestMessage Request()
    {
        var request = new HttpRequestMessage(Method, RequestUri);
        UpdateRequest(request);
        return request;
    }
}
