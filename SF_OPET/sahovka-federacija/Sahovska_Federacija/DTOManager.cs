using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate.Linq;
using Sahovska_Federacija.Entiteti;
using System.Windows.Forms;

namespace Sahovska_Federacija
{
    public class DTOManager
    {
        #region getInfos_TurnirPregled
        public static List<TurnirPregled> getInfos(int federacijaBroj)
        {
            List<TurnirPregled> odInfos = new List<TurnirPregled>();
            try
            {
                ISession s = DataLayer.GetSession();
                IEnumerable<Sahovski_turnir> turniri = from t in s.Query<Sahovski_turnir>()
                                                       where t.Je_pokrovitelj.registracioni_broj == federacijaBroj
                                                       where t.odigran == "Y"
                                                       select t;
                foreach (Sahovski_turnir t in turniri)
                {
                    odInfos.Add(new TurnirPregled(t.id_turnira, t.naziv, t.grad, t.zemlja, t.god_odrzavanja, t.tip, t.Partije.Count));
                }
                s.Close();
            } catch (Exception ex)
            {
                //exception
            }
            return odInfos;
        }
        #endregion

        #region getInfosTurniriHome
        public static List<TurnirPregled> getInfosTurniriHome(int federacijaBroj)
        {
            List<TurnirPregled> odInfos = new List<TurnirPregled>();
            try
            {
                ISession s = DataLayer.GetSession();
                IEnumerable<Sahovski_turnir> turniri = from t in s.Query<Sahovski_turnir>()
                                                       where t.Je_pokrovitelj.registracioni_broj == federacijaBroj
                                                       where t.odigran == "N"
                                                       select t;
                foreach (Sahovski_turnir t in turniri)
                {
                    odInfos.Add(new TurnirPregled(t.id_turnira, t.naziv, t.grad, t.zemlja, t.god_odrzavanja, t.tip, t.Partije.Count));
                }
                s.Close();
            }
            catch (Exception ex)
            {
                //exception
            }
            return odInfos;
        }
        #endregion

        public static List<OrganizatorPregled> getInfosOrganizator(int turnirId)
        {
            List<OrganizatorPregled> odInfos = new List<OrganizatorPregled>();
            try
            {
                ISession s = DataLayer.GetSession();
                IEnumerable<Organizator> organizatori = from o in s.Query<Organizator>()
                                                        where o.OrganizujeTurnir.id_turnira == turnirId
                                                        select o;
                foreach(Organizator o in organizatori)
                {
                    odInfos.Add(new OrganizatorPregled(o.maticni_broj, o.ime, o.prezime, o.adresa));
                }
                s.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return odInfos;
        }

        public static List<OrganizatoriHomePregled> getInfosOrganizatoriHome()
        {
            List<OrganizatoriHomePregled> odInfos = new List<OrganizatoriHomePregled>();
            try
            {
                ISession s = DataLayer.GetSession();
                IEnumerable<Organizator> organizatori = from o in s.Query<Organizator>()
                                                        select o;
                foreach (Organizator o in organizatori)
                {
                    odInfos.Add(new OrganizatoriHomePregled(o.maticni_broj, o.ime, o.prezime, o.adresa));
                }
                s.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return odInfos;
        }

        public static List<SahistiPregled> getInfosSahisti(int federacijaBroj)
        {
            List<SahistiPregled> odInfos = new List<SahistiPregled>();
            try
            {
                ISession s = DataLayer.GetSession();
                IEnumerable<Sahista> sahisti = from sh in s.Query<Sahista>()
                                               where sh.ClanFederacije.registracioni_broj == federacijaBroj
                                               select sh;
                foreach(Sahista sh in sahisti)
                {
                    odInfos.Add(new SahistiPregled(sh.registracioni_broj, sh.ime, sh.prezime, sh.zemlja_porekla, sh.tip));
                }
                s.Close();
            } catch(Exception ex)
            {

            }
            return odInfos;
        }

        public static List<SponzoriPregled> getInfosSponzori()
        {
            List<SponzoriPregled> odInfos = new List<SponzoriPregled>();
            try
            {
                ISession s = DataLayer.GetSession();
                IEnumerable<Sponzor> sponzori = from sh in s.Query<Sponzor>()
                                                select sh;

                foreach (Sponzor sh in sponzori)
                {
                    odInfos.Add(new SponzoriPregled(sh.reg_broj, sh.ime, sh.opis,sh.logo));
                }
                s.Close();
            }
            catch (Exception ex)
            {

            }
            return odInfos;
        }

        public static List<PartijePregled> getInfosPartije(int id)
        {
            List<PartijePregled> odInfos = new List<PartijePregled>();
            try
            {
                ISession s = DataLayer.GetSession();
                IEnumerable<Partija> partije = from p in s.Query<Partija>()
                                               where p.IgraSe.id_turnira == id
                                               select p;
                foreach(Partija p in partije)
                {
                    if(p.mat=="Y")
                        odInfos.Add(new PartijePregled(p.id_partije, p.bele, p.crne, p.kad_se_igra, p.trajanje, "Mat"));
                    if(p.pat=="Y")
                        odInfos.Add(new PartijePregled(p.id_partije, p.bele, p.crne, p.kad_se_igra, p.trajanje, "Pat"));
                    if(p.rem=="Y")
                        odInfos.Add(new PartijePregled(p.id_partije, p.bele, p.crne, p.kad_se_igra, p.trajanje, "Rem"));
                }
                s.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return odInfos;
        }

        public static List<PartijePregledBezIshoda> getInfosPartijeBezIshoda(int id)
        {
            List<PartijePregledBezIshoda> odInfos = new List<PartijePregledBezIshoda>();
            try
            {
                ISession s = DataLayer.GetSession();
                IEnumerable<Partija> partije = from p in s.Query<Partija>()
                                               where p.IgraSe.id_turnira == id
                                               select p;
                foreach (Partija p in partije)
                {
                        odInfos.Add(new PartijePregledBezIshoda(p.id_partije, p.bele, p.crne, p.kad_se_igra, p.trajanje));
                }
                s.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return odInfos;
        }

        public static List<SponzoriPregled> getInfosSponzoriTurnir(int turnirId)
        {
            List<SponzoriPregled> odInfos = new List<SponzoriPregled>();
            try
            {
                ISession s = DataLayer.GetSession();
                IEnumerable<int> je_sponzor = from js in s.Query<Je_sponzor>()
                                              where js.SponzoriseTurnir.id_turnira == turnirId
                                              select js.SponzorJe.reg_broj;

                foreach (int js in je_sponzor)
                {
                    IEnumerable<Sponzor> sponzori = from sp in s.Query<Sponzor>()
                                                    where sp.reg_broj == js
                                                    select sp;
                    foreach(Sponzor sp in sponzori)
                    {
                        odInfos.Add(new SponzoriPregled(sp.reg_broj, sp.ime, sp.opis, sp.logo));
                    }
                }
                s.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return odInfos;
        }

        public static List<PoteziPregled> getInfosPoteziTurnir(int partijaId)
        {
            List<PoteziPregled> odInfos = new List<PoteziPregled>();
            try
            {
                ISession s = DataLayer.GetSession();
                IEnumerable<Potez> potezi = from p in s.Query<Potez>()
                                            where p.je_odigran.id_partije == partijaId
                                            select p;
                foreach(Potez p in potezi)
                {
                    odInfos.Add(new PoteziPregled(p.redni_br, p.opis, p.kad_odigran));
                }
                s.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return odInfos;
        }

        public static TurnirBasic GetTurnirBasic(int odId)
        {
            TurnirBasic tb = new TurnirBasic();
            try
            {
                ISession s = DataLayer.GetSession();

                Sahovski_turnir t = s.Load<Sahovski_turnir>(odId);
                tb = new TurnirBasic(t.id_turnira, t.naziv, t.zemlja, t.grad, t.god_odrzavanja);
                s.Close();
            } catch (Exception ex)
            {
                //exception
            }

            return tb;
        }

        public static TurnirBasic UpdateTurnirBasic(TurnirBasic tb)
        {
            try
            {
                ISession s = DataLayer.GetSession();

                Sahovski_turnir t = s.Load<Sahovski_turnir>(tb.turnirId);
                t.naziv = tb.naziv;
                t.grad = tb.grad;
                t.zemlja = tb.zemlja;
                t.god_odrzavanja = tb.god_odrzavanja;

                s.Update(t);
                s.Flush();
                s.Close();
            } catch (Exception ex)
            {
                //Exception
            }
            return tb;
        }

        public static SahistaBasic GetSahistaBasic(int odId)
        {
            SahistaBasic sb = new SahistaBasic();
            try
            {
                ISession s = DataLayer.GetSession();

                Sahista sh = s.Load<Sahista>(odId);
                sb = new SahistaBasic(sh.registracioni_broj, sh.ime, sh.prezime, sh.ulica, sh.tip);
                s.Close();
            } catch
            {

            }
            return sb;
        }

        public static SahistaBasic UpdateSahistaBasic(SahistaBasic sb)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                Sahista sh = s.Load<Sahista>(sb.registracioni_broj);
                sh.ime = sb.ime;
                sh.prezime = sb.prezime;
                sh.ulica = sb.ulica;
                sh.tip = sb.tip;
                s.Update(sh);
                s.Flush();
                s.Close();
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return sb;
        }

        public static SponzorBasic GetSponzorBasic(int odId)
        {
            SponzorBasic sb = new SponzorBasic();
            try
            {
                ISession s = DataLayer.GetSession();

                Sponzor sh = s.Load<Sponzor>(odId);
                sb = new SponzorBasic(sh.reg_broj, sh.ime, sh.opis, sh.logo);
                s.Close();
            }
            catch
            {

            }
            return sb;
        }

        public static SponzorBasic UpdateSponzorBasic(SponzorBasic sb)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                Sponzor sh = s.Load<Sponzor>(sb.reg_broj);
                sh.ime = sb.ime;
                sh.opis = sb.opis;
                sh.logo = sb.logo;

                s.Update(sh);
                s.Flush();
                s.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return sb;
        }

        public static OrganizatoriHomeBasic GetOrganizatoriHomeBasic(int odId)
        {
            OrganizatoriHomeBasic sb = new OrganizatoriHomeBasic();
            try
            {
                ISession s = DataLayer.GetSession();

                Organizator sh = s.Load<Organizator>(odId);
                sb = new OrganizatoriHomeBasic(sh.maticni_broj, sh.ime, sh.prezime, sh.adresa);
                s.Close();
            }
            catch
            {

            }
            return sb;
        }

        public static OrganizatoriHomeBasic UpdateOrganizatoriHomeBasic(OrganizatoriHomeBasic sb)
        {
            try
            {
                ISession s = DataLayer.GetSession();
                Organizator sh = s.Load<Organizator>(sb.maticni_broj);
                sh.ime = sb.ime;
                sh.prezime = sb.prezime;
                sh.adresa = sb.adresa;

                s.Update(sh);
                s.Flush();
                s.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return sb;
        }



    }
}
