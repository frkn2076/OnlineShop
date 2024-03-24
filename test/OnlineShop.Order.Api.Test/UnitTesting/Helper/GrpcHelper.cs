using Grpc.Core;

namespace OnlineShop.Order.Api.Test.UnitTesting.Helper;

public static class GrpcHelper
{
    public static AsyncUnaryCall<T> CreateAsyncUnaryCall<T>(T response)
    {
        return new AsyncUnaryCall<T>(Task.FromResult(response), Task.FromResult(new Metadata()), () => Status.DefaultSuccess, () => new Metadata(), () => { });
    }
}
