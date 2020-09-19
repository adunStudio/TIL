using System;

namespace Example02
{
    public class PeriodCondition : DiscountCondition
    {
        private DayOfWeek m_DayOfWeek;
        private TimeSpan m_StartTime;
        private TimeSpan m_EndTime;

        public PeriodCondition(DayOfWeek dayOfWeek, TimeSpan startTime, TimeSpan endTime)
        {
            m_DayOfWeek = dayOfWeek;
            m_StartTime = startTime;
            m_EndTime = endTime;
        }

        public bool IsSatisfiedBy(Screening screening)
        {
            return
                screening.GetStartTime().DayOfWeek == m_DayOfWeek &&
                m_StartTime.CompareTo(screening.GetStartTime().ToLocalTime()) <= 0 &&
                m_EndTime.CompareTo(screening.GetStartTime().ToLocalTime()) >= 0;
        }
    }
}