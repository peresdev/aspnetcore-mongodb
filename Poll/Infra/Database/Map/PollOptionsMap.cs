using MongoDB.Bson.Serialization;
using PollProject.Models;

namespace PollProject.Database.Map
{
    public class PollOptionsMap
    {
        public static void Configure()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(PollOptions)))
            {
                BsonClassMap.RegisterClassMap<PollOptions>(cm =>
                {
                    cm.MapMember(c => c.OptionId).SetElementName("option_id");
                    cm.MapMember(c => c.OptionDescription).SetElementName("option_description");
                    cm.SetIgnoreExtraElements(true);
                });
            }

        }
    }
}