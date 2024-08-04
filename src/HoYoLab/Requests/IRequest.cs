namespace HoYoLab.Requests;

public interface IRequest
{
    HttpMethod Method { get; }
    public Uri RequestUri();
}

public interface IRequest<TResponse> : IRequest;
