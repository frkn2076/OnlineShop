using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace OnlineShop.Order.Api.Utils;

public record GrpcChannelDetail
{
    [Url]
    public string Url { get; init; }

    [NotNull]
    public string CertificatePassword { get; init; }
}
