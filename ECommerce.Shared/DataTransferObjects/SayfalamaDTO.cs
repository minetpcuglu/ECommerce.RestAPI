using System.Collections.Generic;

namespace ECommerce.Shared.DataTransferObjects
{
    public class SayfalamaDTO<T> where T : class, new()
    {
        /// <summary>
        /// Request yapılan, First
        /// </summary>
        public bool IlkSayfaMi { get; set; }

        /// <summary>
        /// Request yapılan Page == totalPages , Last
        /// </summary>
        public bool SonSayfaMi { get; set; }

        /// <summary>
        /// Request Yapılan Page, Number
        /// </summary>
        public int SayfaNo { get; set; }

        /// <summary>
        /// Geri dönen listenin eleman sayısı, NumberOfElements
        /// </summary>
        public int SayfaKayitSayisi { get; set; }

        /// <summary>
        /// paging sayısı 25,50 gibi, Size
        /// </summary>
        public int SayfaAdet { get; set; }

        /// <summary>
        /// Siralama, Sort
        /// </summary>
        public string Siralama { get; set; }

        /// <summary>
        /// Toplam eleman Sayısı, TotalElements
        /// </summary>
        public int ToplamKayitSayisi { get; set; }

        /// <summary>
        /// Toplam sayfa sayısı (Count), TotalPages
        /// </summary>
        public int ToplamSayfaSayisi { get; set; }

        /// <summary>
        /// Datanın kendisi, Content
        /// </summary>
        public IList<T> Veri { get; set; }

    }
}
