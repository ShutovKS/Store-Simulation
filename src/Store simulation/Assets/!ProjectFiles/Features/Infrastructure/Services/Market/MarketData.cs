using System;
using UniRx;

namespace Infrastructure.Services.Market
{
    [Serializable]
    public class MarketData
    {
        /// <summary>
        /// Баланс
        /// </summary>
        public IntReactiveProperty balance = new(0);

        /// <summary>
        /// Заработано
        /// </summary>
        public IntReactiveProperty earned = new(0);

        /// <summary>
        /// Потраченный
        /// </summary>
        public IntReactiveProperty spent = new(0);

        /// <summary>
        /// Количество продуктов
        /// </summary>
        public IntReactiveProperty productCount = new(0);

        /// <summary>
        /// количество покупателей
        /// </summary>
        public IntReactiveProperty buyerCount = new(0);
    }
}