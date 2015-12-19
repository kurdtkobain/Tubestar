using System;

namespace TubeStar
{
    public class AdvertistingIncomeAttribute : Attribute
    {
        public Double IncomePerView { get; private set; }

        public AdvertistingIncomeAttribute(double incomePerView)
        {
            IncomePerView = incomePerView;
        }
    }
}