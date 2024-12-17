using Domain.Shared;
using Primitives;

namespace Domain.Tags;

public sealed class TagService(
    ITagRepository tagRepository) : ITagService
{
    public async Task<Guid> GetTagAsync(Text Name, 
        CancellationToken cancellationToken)
    {
        Tag? tag = await tagRepository
            .GetByNameAsync(Name, cancellationToken);

        return tag == null ? CreateTag(Name) : tag.Id;
    }

    public async Task<IEnumerable<(Guid Id, Text Name)>> GetTagsAsync(
        List<Text> Names, 
        CancellationToken cancellationToken)
    {
        List<Tag>? tags = await tagRepository
            .GetByNamesAsync(Names, cancellationToken);

        if (tags is not null && tags.Count.Equals(Names.Count))
        {
            return tags.Select(s => (s.Id, s.Name));
        }
        else if (tags is null || tags.Count == 0)
        {
            return CreateTags(Names);
        }

        var relation = Names.Join(tags, name => name, tag => tag.Name,
                (a, b) => new { Name = a, b.Id });

        IEnumerable<(Guid, Text)> createdTags = CreateTags(relation
            .Where(w => w.Id.Equals(Guid.Empty))
            .Select(s => s.Name).ToList());

        return relation.Where(w => !w.Id.Equals(Guid.Empty))
            .Select(s => (s.Id, s.Name))
            .Concat(createdTags);
    }

    public Guid CreateTag(Text Name)
    {
        Ensure.NotNull(Name);

        var tag = Tag.Create(Guid.NewGuid(), Name, DateTime.UtcNow);

        tagRepository.Insert(tag);

        return tag.Id;
    }

    public IEnumerable<(Guid Id, Text Name)> CreateTags(
        List<Text> Names)
    {
        var tags = Names
            .Select(s => Tag.Create(Guid.NewGuid(), s, DateTime.UtcNow))
            .ToList();

        tagRepository.Insert(tags);

        return tags.Select(s => (s.Id, s.Name));
    }
}
