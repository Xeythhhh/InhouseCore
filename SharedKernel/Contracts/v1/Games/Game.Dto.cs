namespace SharedKernel.Contracts.v1.Games;

public sealed class GameDto
{
    public long Id { get; set; }
    public string Name { get; set; }

    public GameDto() { }

    public GameDto(
        long id,
        string name)
    {
        Id = id;
        Name = name;
    }
}
