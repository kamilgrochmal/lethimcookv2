using LetHimCookV2.API.Domain.Abstractions;

namespace LetHimCookV2.API.Infrastructure.Time;

internal sealed class Clock : IClock
{
    public DateTime Current() => DateTime.UtcNow;
}