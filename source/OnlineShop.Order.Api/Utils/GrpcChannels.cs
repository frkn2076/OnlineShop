using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace OnlineShop.Order.Api.Utils;

public record GrpcChannels
{
    [NotNull]
    public GrpcChannelDetail Customer { get; init; }

    [NotNull]
    public GrpcChannelDetail Product { get; init; }
}
