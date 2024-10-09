namespace CleanArchitecture.Domain.Abstractions;

public class PaginationResult<TEntity, TEntityId>
where TEntity: Entity<TEntityId>
where TEntityId : class
{
    public int Count { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; } = 0;

    public IReadOnlyList<TEntity>? Data { get; set; }   

    public int PageCount { get; set; } = 0;

    public int ResultByPage {get; set;}
}