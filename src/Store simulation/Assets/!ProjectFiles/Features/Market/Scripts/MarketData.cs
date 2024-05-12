using System;
using UniRx;

namespace Market
{
    [Serializable]
    public class MarketData
    {
        /// <summary>
        /// Баланс
        /// </summary>
        public IntReactiveProperty balance;

        /// <summary>
        /// Заработано
        /// </summary>
        public IntReactiveProperty earned;

        /// <summary>
        /// Потраченный
        /// </summary>
        public IntReactiveProperty spent;

        /// <summary>
        /// Количество продуктов
        /// </summary>
        public IntReactiveProperty productCount;

        /// <summary>
        /// количество покупателей
        /// </summary>
        public IntReactiveProperty buyerCount;
    }
}