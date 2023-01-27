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

        public AdState CurrentState { get; private set; }

        public IList<AdState> AdStatesHistory { get; private set; }

        public Ad(string title, string description, Localization localization)
        {
            Title = title;
            Description = description;
            Localization = localization;
            CurrentState = AdState.WaitingForValidation;
            AdStatesHistory = new List<AdState>();
            this.UpdateState(CurrentState);
        }

        public void UpdateState(AdState newState)
        {
            CurrentState = newState;

            AdStatesHistory.Add(newState);
        }
    }
}