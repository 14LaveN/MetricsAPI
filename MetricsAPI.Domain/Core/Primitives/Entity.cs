﻿using MetricsAPI.Domain.Core.Utility;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MetricsAPI.Domain.Common.Core.Primitives;

/// <summary>
/// Represents the base class that all entities derive from.
/// </summary>
public abstract class Entity : IEquatable<Entity>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Entity"/> class.
    /// </summary>
    /// <param name="id">The entity identifier.</param>
    protected Entity(string id)
        : this()
    {
        Ensure.NotEmpty(id, "The identifier is required.", nameof(id));

        Id = id;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Entity"/> class.
    /// </summary>
    /// <remarks>
    /// Required by EF Core.
    /// </remarks>
    protected Entity()
    {
    }
    
    /// <summary>
    /// Gets or sets identifier.
    /// </summary>
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; }
    
    /// <summary>
    /// Gets or sets date/time created at.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public static bool operator ==(Entity a, Entity b)
    {
        if (a is null && b is null)
        {
            return true;
        }

        if (a is null || b is null)
        {
            return false;
        }

        return a.Equals(b);
    }

    public static bool operator !=(Entity a, Entity b) => !(a == b);

    /// <inheritdoc />
    public bool Equals(Entity other)
    {
        if (other is null)
        {
            return false;
        }

        return ReferenceEquals(this, other) || Id == other.Id;
    }

    /// <inheritdoc />
    public override bool Equals(object obj)
    {
        if (obj is null)
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        if (obj.GetType() != GetType())
        {
            return false;
        }

        if (!(obj is Entity other))
        {
            return false;
        }

        if (Id == string.Empty || other.Id == string.Empty)
        {
            return false;
        }

        return Id == other.Id;
    }

    /// <inheritdoc />
    public override int GetHashCode() => Id!.GetHashCode() * 41;
}