using AdDeposit.Core;
using AdDeposit.Domain.Ads;

namespace AdDeposit.Domain.Entities
{
    public sealed class Ad : IEntity
    {
        public long Id { get; set; } = 0;
        public string Title { get; private set; }

        public string Description { get; private set; }
        public Localization Localization { get; private set; }

        public AdState State { get; private set; }

        public Ad(string title, string description, Localization localization)
        {
            Title = title;
            Description = description;
            Localization = localization;
            State = AdState.WaitingForValidation;
        }
    }
}