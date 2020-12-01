using MongoDB.Bson.Serialization;
using PollProject.Models;

namespace PollProject.Database.Map
{ 
    public class PollMap
    {
        public static void Configure()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Poll)))
            {
                BsonClassMap.RegisterClassMap<Poll>(cm =>
                {
                    cm.MapMember(c => c.PollId).SetElementName("poll_id");
                    cm.MapMember(c => c.PollDescription).SetElementName("poll_description");
                    cm.MapMember(c => c.Views).SetElementName("views");
                    cm.MapMember(c => c.Options).SetElementName("options");
                    cm.MapMember(c => c.Votes).SetElementName("votes");
                    cm.SetIgnoreExtraElements(true);
                });
            }
        }
    }
}