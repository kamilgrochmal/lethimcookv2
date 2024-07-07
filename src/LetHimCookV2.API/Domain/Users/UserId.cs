namespace LetHimCookV2.API.Domain.Users;

public sealed record UserId
{
    public long Value { get; }

    public UserId(long value)
    {
        Value = value;
    }

    public static implicit operator long(UserId date) => date.Value;
    
    public static implicit operator UserId(long value) => new(value);
}