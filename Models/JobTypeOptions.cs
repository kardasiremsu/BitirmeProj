using System.Collections.Generic;

namespace BitirmeProj.Utilities
{
    public static class JobApplicationOptions
    {
        public static Dictionary<int, string> WorkPlaceTypeOptions = new Dictionary<int, string>
        {
            { 1, "Hybrid" },
            { 2, "Onsite" },
            { 3, "Online" }
        };

        public static Dictionary<int, string> ExperienceLevelOptions = new Dictionary<int, string>
        {
            { 1, "Internship" },
            { 2, "Beginner" },
            { 3, "Expert" },
            { 4, "Mid-Expert Manager" },
            { 5, "Director" },
            { 6, "Expert Manager" },
            { 7, "Research Assistant" },
            { 8, "Teaching Staff" }
        };

        public static Dictionary<int, string> SalaryCurrencyOptions = new Dictionary<int, string>
        {
            { 1, "TL" },
            { 2, "Euro" },
            { 3, "Dolar" },
            { 4, "Pound" }
        };

       public static Dictionary<string, string> JobLocationOptions = new Dictionary<string, string>
        {
            { "adana", "Adana" },
            { "adiyaman", "Adıyaman" },
            { "afyonkarahisar", "Afyonkarahisar" },
            { "agri", "Ağrı" },
            { "amasya", "Amasya" },
            { "ankara", "Ankara" },
            { "antalya", "Antalya" },
            { "artvin", "Artvin" },
            { "aydin", "Aydın" },
            { "balikesir", "Balıkesir" },
            { "bilecik", "Bilecik" },
            { "bingol", "Bingöl" },
            { "bitlis", "Bitlis" },
            { "bolu", "Bolu" },
            { "burdur", "Burdur" },
            { "bursa", "Bursa" },
            { "canakkale", "Çanakkale" },
            { "cankiri", "Çankırı" },
            { "corum", "Çorum" },
            { "denizli", "Denizli" },
            { "diyarbakir", "Diyarbakır" },
            { "duzce", "Düzce" },
            { "edirne", "Edirne" },
            { "elazig", "Elazığ" },
            { "erzincan", "Erzincan" },
            { "erzurum", "Erzurum" },
            { "eskisehir", "Eskişehir" },
            { "gaziantep", "Gaziantep" },
            { "giresun", "Giresun" },
            { "gumushane", "Gümüşhane" },
            { "hakkari", "Hakkâri" },
            { "hatay", "Hatay" },
            { "igdir", "Iğdır" },
            { "isparta", "Isparta" },
            { "istanbul", "İstanbul" },
            { "izmir", "İzmir" },
            { "kars", "Kars" },
            { "kastamonu", "Kastamonu" },
            { "kayseri", "Kayseri" },
            { "kilis", "Kilis" },
            { "kirikkale", "Kırıkkale" },
            { "kirklareli", "Kırklareli" },
            { "kirsehir", "Kırşehir" },
            { "kocaeli", "Kocaeli" },
            { "konya", "Konya" },
            { "kutahya", "Kütahya" },
            { "malatya", "Malatya" },
            { "manisa", "Manisa" },
            { "mardin", "Mardin" },
            { "mersin", "Mersin" },
            { "mugla", "Muğla" },
            { "mus", "Muş" },
            { "nevsehir", "Nevşehir" },
            { "nigde", "Niğde" },
            { "ordu", "Ordu" },
            { "osmaniye", "Osmaniye" },
            { "rize", "Rize" },
            { "sakarya", "Sakarya" },
            { "samsun", "Samsun" },
            { "siirt", "Siirt" },
            { "sinop", "Sinop" },
            { "sivas", "Sivas" },
            { "sanliurfa", "Şanlıurfa" },
            { "sirnak", "Şırnak" },
            { "tekirdag", "Tekirdağ" },
            { "tokat", "Tokat" },
            { "trabzon", "Trabzon" },
            { "tunceli", "Tunceli" },
            { "usak", "Uşak" },
            { "van", "Van" },
            { "yalova", "Yalova" },
            { "yozgat", "Yozgat" },
            { "zonguldak", "Zonguldak" },
            { "foreigncountry", "Foreign Country" }
        };

        public static Dictionary<int, string> JobTypeOptions = new Dictionary<int, string>
        {
            { 1, "Full Time" },
            { 2, "Part Time" },
            { 3, "Intern" }
        };
    }
}
