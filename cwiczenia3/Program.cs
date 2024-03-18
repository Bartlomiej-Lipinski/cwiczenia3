using System.Collections;

namespace Kontenery
{
    class Kontener
    {
        private double masa;
        private int wysokosc;
        private double WagaWlasna;
        private int glebokosc;
        private string numerSeryjny;
        private double maxLadownosc;
        private bool isHazardous;

        public Kontener(int masa, int wysokosc, int WagaWlasna, int glebokosc , int maxLadownosc)
        {
            this.masa = masa;
            this.wysokosc = wysokosc;
            this.WagaWlasna = WagaWlasna;
            this.glebokosc = glebokosc;
            numerSeryjny = "KON-"+establishConType()+"-"+generateSN();
            this.maxLadownosc = maxLadownosc;
        }
        public void OprozniLadunek()
        {
            masa = WagaWlasna;
        }
        public void zaladujLadunek(int masaLadunku)
        {
            if (masaLadunku + masa <= maxLadownosc)
            {
                masa += masaLadunku;
            }
            else
            {
                throw new OverFilledException();
            }
        }

        private int generateSN()
        {
             return new Random().Next(1, 9999);
        }

        private string establishConType()
        {
            if (this is KontenerNaPlyny)
            {
                 return "L";
            }
            if (this is KontenernaGaz)
            {
                return "G";
            }
            if (this is KontenerChlodniczy)
            {
                return "C";
            }
            return "N";
        }
    }

    internal class OverFilledException : Exception
    {
        public OverFilledException()
        {
            Console.WriteLine("Kontener jest przeciażony!");
        }
    }

    class KontenerNaPlyny : Kontener,IHazardNotifier
    {
        public KontenerNaPlyny(int masa, int wysokosc, int WagaWlasna, int glebokosc, int maxLadownosc) : base(masa, wysokosc, WagaWlasna, glebokosc,maxLadownosc)
        {
            
        }
        
    }

    class KontenernaGaz : Kontener,IHazardNotifier
    {
        int Pressure { get; set; }
        
        public KontenernaGaz(int masa, int wysokosc, int WagaWlasna, int glebokosc, int maxLadownosc) : base(masa, wysokosc, WagaWlasna, glebokosc, maxLadownosc)
        {
            
        }
    }
    class KontenerChlodniczy : Kontener
    {
        private int temp { get;set; }
        private TypProduktu typProdukt { get; set; }
        
        public KontenerChlodniczy(int masa, int wysokosc, int WagaWlasna, int glebokosc, int maxLadownosc,TypProduktu typProduktu) : base(masa, wysokosc, WagaWlasna, glebokosc, maxLadownosc)
        {
            this.typProdukt = typProduktu;
        }
    }

    public interface IHazardNotifier
    {
        private void NotifyHazard(String sn)
        {
            Console.WriteLine("Uwaga! Kontener zawiera materiały niebezpieczne!"+" Numer seryjny: "+sn);
        }
    }

    enum TypProduktu
    {
        Bananas,Chocolate,Fish,Meat,IceCream,FrozenPizza,Cheese,Sausages,Butter,Eggs
    }
    
}

namespace kontenerowce
{
    class Kontenerowiec
    {
        private ArrayList kontenery = new ArrayList();
        private int predkosc;
        private int maxiloscKontenerow;
        private int maxLadownosc;
    }
}
