namespace FitnessApp.Common.Constants
{
    public static class GlobalConstants
    {
        public const int MALE_MULTIPLIER = 66;
        public const decimal MALE_WEIGHT_MULTIPLIER = 13.7m;
        public const decimal MALE_HEIGHT_MULTIPLIER = 5;
        public const decimal MALE_AGE_MULTIPLIER = 6.8m;

        public const int FEMALE_MULTIPLIER = 655;
        public const decimal FEMALE_WEIGHT_MULTIPLIER = 9.6m;
        public const decimal FEMALE_HEIGHT_MULTIPLIER = 1.8m;
        public const decimal FEMALE_AGE_MULTIPLIER = 4.7m;


        public const decimal SEDENTARY_MULTIPLIER = 1.2m;
        public const decimal LIGHTLYACTIVE_MUTLIPLIER = 1.375m;
        public const decimal ACTIVE_MUTLIPLIER = 1.55m;
        public const decimal VERYACTIVE_MULTIPLIER = 1.725m;

        public const int BULKING_CALORIES = 500;
        public const int CUTTING_CALORIES = -500;
        public const int MAINTAIN_CALORIES = 0;

        public const decimal DEFAULT_GOAL_WEIGHT = 0;
        public const decimal DEFAULT_GOAL_HEIGHT = 0;
        public const int DEFAULT_GOAL_AGE = 0;
    }
}
