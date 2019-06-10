﻿namespace AbstractGiftShopModel
{
    /// <summary>
    /// Сколько материалов хранится на складе
    /// </summary>
    public class StockMaterials
    {
        public int Id { get; set; }
        public int SStockId { get; set; }
        public int MaterialsId { get; set; }
        public int Count { get; set; }
    }
}