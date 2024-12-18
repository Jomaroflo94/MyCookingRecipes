using Domain.Shared;
using Primitives.Guards;

namespace Domain.Tags;

public sealed class TagService(
    ITagRepository tagRepository) : ITagService
{
    public async Task<Ulid> GetTagAsync(Text Name, 
        CancellationToken cancellationToken)
    {
        Tag? tag = await tagRepository
            .GetByNameAsync(Name, cancellationToken);

        return tag == null ? CreateTag(Name) : tag.Id;
    }

    public async Task<IEnumerable<(Ulid Id, Text Name)>> GetTagsAsync(
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

        IEnumerable<(Ulid, Text)> createdTags = CreateTags(relation
            .Where(w => w.Id.Equals(Ulid.Empty))
            .Select(s => s.Name).ToList());

        return relation.Where(w => !w.Id.Equals(Ulid.Empty))
            .Select(s => (s.Id, s.Name))
            .Concat(createdTags);
    }

    public Ulid CreateTag(Text Name)
    {
        Ensure.NotNull(Name);

        var tag = Tag.Create(Ulid.NewUlid(), Name, DateTime.UtcNow);

        tagRepository.Insert(tag);

        return tag.Id;
    }

    public IEnumerable<(Ulid Id, Text Name)> CreateTags(
        List<Text> Names)
    {
        var tags = Names
            .Select(s => Tag.Create(Ulid.NewUlid(), s, DateTime.UtcNow))
            .ToList();

        tagRepository.Insert(tags);

        return tags.Select(s => (s.Id, s.Name));
    }
}
