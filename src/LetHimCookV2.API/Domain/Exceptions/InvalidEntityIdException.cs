namespace LetHimCookV2.API.Domain.Exceptions;

public sealed class InvalidEntityIdException : CustomException
{
    public object Id { get; }

    public InvalidEntityIdException(object id) : base($"Cannot set: {id}  as entity identifier.")
        => Id = id;
}