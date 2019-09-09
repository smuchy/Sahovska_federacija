using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sahovska_Federacija
{
    #region TurnirPregled
    public class TurnirPregled
    {
        public int turnirId { get; set; }
        public string naziv { get; set; }
        public string grad { get; set; }
        public string zemlja { get; set; }
        public int god_odrzavanja { get; set; }
        public string tip { get; set; }
        public int brojPartija { get; set; }

        public TurnirPregled(int id, string Naziv, string Grad, string Zemlja, int God_odrzavanja, string Tip, int brPartija)
        {
            this.turnirId = id;
            this.naziv = Naziv;
            this.grad = Grad;
            this.zemlja = Zemlja;
            this.god_odrzavanja = God_odrzavanja;
            this.tip = Tip;
            this.brojPartija = brPartija;
        }


    }
    #endregion

    #region OrganizatorPregled
    public class OrganizatorPregled
    {
        public int maticni_broj { get; set; }
        public string ime { get; set; }
        public string prezime { get; set; }
        public string adresa { get; set; }

        public OrganizatorPregled(int Mat_br, string Ime, string Prezime, string Adresa)
        {
            this.maticni_broj = Mat_br;
            this.ime = Ime;
            this.prezime = Prezime;
            this.adresa = Adresa;
        }
    }
    #endregion

    #region TurnirBasic
    public class TurnirBasic
    {
        public int turnirId { get; set; }
        public string naziv { get; set; }
        public string zemlja { get; set; }
        public string grad { get; set; }
        public int god_odrzavanja { get; set; }

        public TurnirBasic(int id, string Naziv, string Zemlja, string Grad, int odrzavanje)
        {
            this.turnirId = id;
            this.naziv = Naziv;
            this.zemlja = Zemlja;
            this.grad = Grad;
            this.god_odrzavanja = odrzavanje;
        }

        public TurnirBasic() { }
    }
    #endregion

    #region SahistaBasic

    public class SahistaBasic
    {
        public int registracioni_broj { get; set; }
        public string ime { get; set; }
        public string prezime { get; set; }
        public string ulica { get; set; }
        public string tip { get; set; }

        public SahistaBasic(int reg_broj, string Ime, string Prezime, string Ulica, string Tip)
        {
            this.registracioni_broj = reg_broj;
            this.ime = Ime;
            this.prezime = Prezime;
            this.ulica = Ulica;
            this.tip = Tip;
        }
        public SahistaBasic() { }
    }

    #endregion

    #region SahistiPregled
    public class SahistiPregled
    {
        public int registracioni_broj_sahiste { get; set; }
        public string ime { get; set; }
        public string prezime { get; set; }
        public string zemlja_porekla { get; set; }
        public string tip { get; set; }

        public SahistiPregled(int reg_broj, string Ime, string Prezime, string zemlja, string Tip)
        {
            this.registracioni_broj_sahiste = reg_broj;
            this.ime = Ime;
            this.prezime = Prezime;
            this.zemlja_porekla = zemlja;
            this.tip = Tip;
        }
    }
    #endregion

    #region SponzoriPregled
    public class SponzoriPregled
    {
        public int reg_broj { get; set; }
        public string ime { get; set; }
        public string opis { get; set; }
        public string logo { get; set; }

        public SponzoriPregled(int reg_broj, string ime, string opis, string logo)
        {
            this.reg_broj = reg_broj;
            this.ime = ime;
            this.opis = opis;
            this.logo = logo;
        }

    }
    #endregion

    #region SponzorBasic

    public class SponzorBasic
    {
        public int reg_broj { get; set; }
        public string ime { get; set; }
        public string opis { get; set; }
        public string logo { get; set; }
        public SponzorBasic(int reg_broj, string ime, string opis, string logo)
        {
            this.reg_broj = reg_broj;
            this.ime = ime;
            this.opis = opis;
            this.logo = logo;
        }
        public SponzorBasic() { }
    }

    #endregion

    #region PartijePregled
    public class PartijePregled
    {
        public int id_partije { get; set; }
        public string bele_figure { get; set; }
        public string crne_figure { get; set; }
        public string kad_se_igra { get; set; }
        public int trajanje { get; set; }
        public string ishod { get; set; }
        

        public PartijePregled(int id, string Bele, string Crne, string kad, int Trajanje, string Ishod)
        {
            this.id_partije = id;
            this.bele_figure = Bele;
            this.crne_figure = Crne;
            this.kad_se_igra = kad;
            this.trajanje = Trajanje;
            this.ishod = Ishod;
        }
    }

    #endregion

    #region PartijePregldBezIshoda
    public class PartijePregledBezIshoda
    {
        public int id_partije { get; set; }
        public string bele_figure { get; set; }
        public string crne_figure { get; set; }
        public string kad_se_igra { get; set; }
        public int trajanje { get; set; }

        public PartijePregledBezIshoda(int id, string Bele, string Crne, string kad, int Trajanje)
        {
            this.id_partije = id;
            this.bele_figure = Bele;
            this.crne_figure = Crne;
            this.kad_se_igra = kad;
            this.trajanje = Trajanje;
        }
    }
    #endregion


    #region PoteziPregled

    public class PoteziPregled
    {
        public int redni_br { get; set; }
        public string opis { get; set; }
        public string kad_odigran { get; set; }

        public PoteziPregled(int redni, string Opis, string Kad)
        {
            this.redni_br = redni;
            this.opis = Opis;
            this.kad_odigran = Kad;
        }
    }
    #endregion

    #region OrganizatoriHomePregled
    public class OrganizatoriHomePregled
    {
        public int maticni_broj { get; set; }
        public string ime { get; set; }
        public string prezime { get; set; }
        public string adresa { get; set; }

        public OrganizatoriHomePregled(int maticni_broj, string ime, string prezime, string adresa)
        {
            this.maticni_broj = maticni_broj;
            this.ime = ime;
            this.prezime = prezime;
            this.adresa = adresa;
        }

    }
    #endregion

    #region OrganizatoriHomeBasic

    public class OrganizatoriHomeBasic
    {
        public int maticni_broj { get; set; }
        public string ime { get; set; }
        public string prezime { get; set; }
        public string adresa { get; set; }
        public OrganizatoriHomeBasic(int maticni_broj, string ime, string prezime, string adresa)
        {
            this.maticni_broj = maticni_broj;
            this.ime = ime;
            this.prezime = prezime;
            this.adresa = adresa;
        }

        public OrganizatoriHomeBasic()
        {
        }
    }
    #endregion
}