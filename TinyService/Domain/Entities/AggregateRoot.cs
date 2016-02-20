using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinyService.DomainEvent;
using TinyService.Infrastructure;
using TinyService.Service;

namespace TinyService.Domain.Entities
{
    [Serializable]
    public abstract class AggregateRoot<TPrimaryKey> : IAggregateRoot<TPrimaryKey>
    {
        private readonly List<IDomainEvent> _changes = new List<IDomainEvent>();
 
        public virtual TPrimaryKey Id
        {
            get;
            private set;
        }
         public AggregateRoot()
        {

        }
        public AggregateRoot(TPrimaryKey id)
        {
            this.Id = id;
        }

        public int Version { get; internal set; }

        public IEnumerable<IDomainEvent> GetUncommittedChanges()
        {
            return _changes;
        }

        public void MarkChangesAsCommitted()
        {
            _changes.Clear();
        }


        public void LoadsFromHistory(IEnumerable<IDomainEvent> history)
        {
            foreach (var e in history) ApplyChange(e, false);
        }

        protected void ApplyChange(IDomainEvent @event)
        {
            ApplyChange(@event, true);
        }

        private void ApplyChange(IDomainEvent @event, bool isNew)
        {
            this.AsDynamic().Apply(@event);

            if (isNew)
            {
                _changes.Add(@event);
            }

        }

        #region Equals
        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is AggregateRoot<TPrimaryKey>))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            var other = (AggregateRoot<TPrimaryKey>)obj;


            var typeOfThis = GetType();
            var typeOfOther = other.GetType();
            if (!typeOfThis.IsAssignableFrom(typeOfOther) && !typeOfOther.IsAssignableFrom(typeOfThis))
            {
                return false;
            }

            return Id.Equals(other.Id) && Version.Equals(other.Version);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode() ^ Version.GetHashCode();
        }

        public static bool operator ==(AggregateRoot<TPrimaryKey> left, AggregateRoot<TPrimaryKey> right)
        {
            if (Equals(left, null))
            {
                return Equals(right, null);
            }

            return left.Equals(right);
        }

        public static bool operator !=(AggregateRoot<TPrimaryKey> left, AggregateRoot<TPrimaryKey> right)
        {
            return !(left == right);
        }

        #endregion



    }
}
