namespace FitnessApp.Web.Infrastructure.Extensions
{
    using FitnessApp.Models.Enums;

    public static class EnumExtensions
    {
        public static string ToDisplayName(this ActivityLevel activityLevel)
        {
            switch (activityLevel)
            {
                case ActivityLevel.Sedentary:
                    return "Sedentary: Spend most of the day sitting (e.g. bank teller, desk job)";
                case ActivityLevel.LightlyActive:
                    return "Lightly Active: Spend a good part of the day on your feet (e.g. teacher, salesperson)";
                case ActivityLevel.Active:
                    return "Active: Spend a good part of the day doing some physical activity (e.g. food server, postal carrier)";
                case ActivityLevel.VeryActive:
                    return "Very Active: Spend most of the day doing heavy physical activity (e.g. bike messenger, carpenter)";
                default:
                    break;
            }

            return activityLevel.ToString();
        }

        public static string ToDisplayName(this WeightChangeType weightChangeType)
        {
            switch (weightChangeType)
            {
                case WeightChangeType.Maintaing:
                    return "Maintain Weight: Keep your current weight";
                case WeightChangeType.Bulk:
                    return "Gain Weight: Get bigger and stronger but a little bit fatter.";
                case WeightChangeType.Cut:
                    return "Lose Weight: Get shredded and become aesthetic but a little bit smaller";
                default:
                    break;
            }

            return weightChangeType.ToString();
        }
    }
}
